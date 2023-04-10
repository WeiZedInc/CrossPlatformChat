using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.Helpers;
using CrossPlatformChat.MVVM.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace CrossPlatformChat.Utils.Helpers
{
    public class ChatHub : INotifyPropertyChanged
    {
        HubConnection _hubConnection;
        bool _isBusy = false, _isConnected = false;
        readonly string _connectionPath;
        ChatsCollectionModel _Model;

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
            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                _connectionPath = "http://10.0.2.2:5066/ChatHub";
            else
                _connectionPath = "http://localhost:5066/ChatHub";

            Application.Current.Dispatcher.Dispatch(async () => await Connect());

            _Model = ServiceHelper.Get<ChatsCollectionModel>();
        }

        public async Task Connect()
        {
            if (IsConnected || IsBusy)
                return;
            IsBusy = true;
            try
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_connectionPath)
                    .Build();

                _hubConnection.Closed += async (error) =>
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "Hub connection closed", "ok");
                    IsConnected = false;
                    await Connect();
                };

                _hubConnection.On<int, MessageEntity>("MessageFromServer", OnMessageRecieved);

                await _hubConnection.StartAsync();
                IsConnected = true;
                IsBusy = false;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error at Hub Connect", ex.Message, "ok");
            }
        }
        public async Task Disconnect()
        {
            if (!IsConnected || _hubConnection is null)
                return;
            try
            {
                await _hubConnection.StopAsync();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error at Hub Disconnect", ex.Message, "ok");
            }
        }

        void OnMessageRecieved(int chatID, MessageEntity messageEntity)
        {
            try
            {
                if (_Model.ChatsAndMessagessDict == null) return;

                lock (_Model.ChatsAndMessagessDict)
                {
                    var kvp = _Model.ChatsAndMessagessDict.Where(x => x.Key.ID == chatID).FirstOrDefault();
                    if (kvp.Key == null) return;

                    messageEntity.Message = CryptoManager.DecryptMessage(messageEntity.EncryptedMessage, kvp.Key.StoredSalt, messageEntity.InitialVector);

                    kvp.Value.Add(messageEntity);
                    ServiceHelper.Get<ISQLiteService>().InsertAsync(messageEntity).Wait();
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error at OnMessageRecieved", ex.Message, "ok").Wait();
            }
        }

        public async Task<bool> SendMessageToServer(int chatID, MessageEntity messageEntity)
        {
            if (IsConnected && messageEntity != null)
            {
                var chat = _Model.ChatsAndMessagessDict.Keys.Where(x => x.ID == chatID).FirstOrDefault();
                if (chat == null) return false;

                var (encryptedMessage, initialVector) = CryptoManager.EncryptMessage(chat.StoredSalt, messageEntity.Message);

                messageEntity.Message = null;
                messageEntity.EncryptedMessage = encryptedMessage;
                messageEntity.InitialVector = initialVector;

                await _hubConnection.InvokeCoreAsync("MessageFromClient", new object[] { chatID, messageEntity});
                return true;
            }
            else
                return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
