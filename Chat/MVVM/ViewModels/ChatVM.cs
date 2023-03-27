using CrossPlatformChat.Database.Entities;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatVM : INotifyPropertyChanged
    {
        HubConnection _hubConnection;
        public string UserName { get; set; }
        public string MessageContent { get; set; }
        public ICommand SendMessageCommand { get; }
        public ObservableCollection<MessageEntity> Messages { get; }

        bool _isBusy, _isConnected;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }
        public bool IsConnected
        {
            get { return _isConnected; }
            set { _isConnected = value; OnPropertyChanged(nameof(_isConnected)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public ChatVM()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(APIManager.Instance.devSsl.DevServerRootUrl + "/ChatHub").Build();  // for emulator

            Messages = new ObservableCollection<MessageEntity>();
            IsConnected = false;
            IsBusy = false;
            SendMessageCommand = new Command(async () => await SendMessage(), () => IsConnected);

            _hubConnection.Closed += async (error) =>
            {
                SendLocalMessage(null, "Hub connection closed");
                IsConnected = false;
                await Task.Delay(3000);
                await Connect();
            };

            _hubConnection.On<ClientEntity, string>("Receive", (user, message) =>
            {
                SendLocalMessage(user, message);
            });
        }

        public async Task Connect()
        {
            if (IsConnected)
                return;
            try
            {
                await _hubConnection.StartAsync();
                SendLocalMessage(null, "You've entered the chat");

                IsConnected = true;
            }
            catch (Exception ex)
            {
                SendLocalMessage(null, $"Connection error: {ex.Message}");
            }
        }

        public async Task Disconnect()
        {
            if (!IsConnected)
                return;

            await _hubConnection.StopAsync();
            IsConnected = false;
            SendLocalMessage(null, "You've leaved the chat");
        }

        async Task SendMessage()
        {
            try
            {
                IsBusy = true;
                await _hubConnection.InvokeAsync("Send", UserName, MessageContent);
            }
            catch (Exception ex)
            {
                SendLocalMessage(null, $"Sending error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void SendLocalMessage(ClientEntity client, string message)
        {
            Messages.Insert(0, new MessageEntity // remake index
            {
                Content = message,
                SenderID = client.ID
            });
        }

    }
}
