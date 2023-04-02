using Microsoft.AspNetCore.SignalR.Client;

namespace CrossPlatformChat.Utils.Helpers
{
    public class ChatHub : INotifyPropertyChanged
    {
        HubConnection _hubConnection;
        bool _isBusy, _isConnected;
        readonly string _connectionPath;

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
            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                _connectionPath = "http://10.0.2.2:5066/ChatHub";
            else
                _connectionPath = "http://localhost:5066/ChatHub";

            Application.Current.Dispatcher.Dispatch(async () =>
            {
                await Connect();
            });
        }

        public async Task Connect()
        {
            if (IsConnected)
                return;
            try
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_connectionPath)
                    .Build();

                _hubConnection.ServerTimeout = new TimeSpan(0, 0, 5);
                _hubConnection.Closed += async (error) =>
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "Hub connection closed", "ok");
                    IsConnected = false;
                    await Task.Delay(3000);
                    await Connect();
                };

                _hubConnection.On<string>("ReceiveMessage", async (message) =>
                {
                    await App.Current.MainPage.DisplayAlert("Received msg", message, "ok");
                });
                await _hubConnection.StartAsync();

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

        public async void SendMessageToServer(string message = "test")
        {
            await _hubConnection.InvokeCoreAsync("SendMessageToAll", args: new[]
            {
                message
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
