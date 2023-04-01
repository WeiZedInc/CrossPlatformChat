using Microsoft.AspNetCore.SignalR.Client;

namespace CrossPlatformChat.Utils.Helpers
{
    public class ChatHub : INotifyPropertyChanged
    {
        HubConnection _hubConnection;
        bool _isBusy, _isConnected;
        readonly string connectionPath = "https://10.0.2.2:7233/ChatHub";

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

        public ChatHub()
        {
            IsConnected = false;
            IsBusy = false;
        }

        public async Task Connect()
        {
            if (IsConnected)
                return;
            try
            {
                string accessToken = ServiceHelper.Get<ClientHandler>().LocalClient.Token;
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(connectionPath)
                    .Build();

                _hubConnection.ServerTimeout = new TimeSpan(0, 0, 5);
                _hubConnection.Closed += async (error) =>
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "Hub connection closed", "ok");
                    IsConnected = false;
                    await Task.Delay(3000);
                    await Connect();
                };

                _hubConnection.On<int, string>("ReceiveMessage", async (chatID, message) =>
                {
                    await App.Current.MainPage.DisplayAlert("Received msg", message, "ok");
                });
                await _hubConnection.StartAsync();
                await _hubConnection.SendAsync("GetConnectionFromServer", "Connected");

                await App.Current.MainPage.DisplayAlert("Success", "You have entered the chat", "ok");
                IsConnected = true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error at Connect", ex.Message, "ok");
            }
        }

        public async Task Disconnect()
        {
            if (!IsConnected)
                return;

            await _hubConnection.StopAsync();
            IsConnected = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
