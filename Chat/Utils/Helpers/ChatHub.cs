using Microsoft.AspNetCore.SignalR.Client;

namespace CrossPlatformChat.Utils.Helpers
{
    public class ChatHub : INotifyPropertyChanged
    {
        HubConnection _hubConnection;
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
                string accessToken = ClientManager.Instance.Local.Token;
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl($"{APIManager.Instance.devSsl.DevServerRootUrl}/ChatHub?access_token={accessToken}") // for emulator
                    .Build();

                _hubConnection.ServerTimeout = new TimeSpan(0, 0, 5);
                _hubConnection.Closed += async (error) =>
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "Hub connection closed", "ok");
                    IsConnected = false;
                    await Task.Delay(3000);
                    await Connect();
                };

                _hubConnection.On<int, string>("Receive", async (userID, message) =>
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
