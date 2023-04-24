using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.Helpers;
using CrossPlatformChat.MVVM.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace CrossPlatformChat.Services
{
    class ClientHub
    {
        HubConnection _hubConnection;
        bool _IsBusy = false, _IsConnected = false;
        readonly string _connectionPath;
        ChatsCollectionModel _Model;

        public ClientHub()
        {
            _Model = ServiceHelper.Get<ChatsCollectionModel>();

            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                _connectionPath = "http://10.0.2.2:5066/ChatHub";
            else
                _connectionPath = "http://localhost:5066/ChatHub";

            Application.Current.Dispatcher.Dispatch(async () => await Connect());
        }

        public async Task Connect()
        {
            if (_IsConnected || _IsBusy)
                return;
            try
            {
                _IsBusy = true;

                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_connectionPath)
                    .Build();

                _hubConnection.Closed += async (error) =>
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "Hub connection closed", "ok");
                    _IsConnected = false;
                    await Connect();
                };

                _hubConnection.On<ChatEntity>("ReceiveChat", ReceiveChat);

                await _hubConnection.StartAsync();
                await _hubConnection.InvokeAsync("AddToClientsGroup", ClientHandler.LocalClient.ID.ToString());
                _IsConnected = true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error at Hub Connect", ex.Message, "ok");
            }
            finally
            {
                _IsBusy = false;
            }
        }

        public async Task Disconnect()
        {
            if (_IsConnected && _hubConnection != null)
            {
                await _hubConnection.InvokeAsync("RemoveFromClientsGroup", ClientHandler.LocalClient.ID.ToString());
                await _hubConnection.StopAsync();
            }
        }

        void ReceiveChat(ChatEntity chat)
        {
            try
            {
                if (_Model == null || chat == null)
                    throw new("Model || Chat == null");

                lock (_Model.ChatsAndMessagessDict)
                {
                    if (_Model.ChatsAndMessagessDict.ContainsKey(chat)) 
                        return;

                    _Model.ChatsAndMessagessDict.Add(chat, new());
                    ServiceHelper.Get<ISQLiteService>().InsertAsync(chat).Wait();
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error at ReceiveMessage", ex.Message, "ok").Wait();
            }
        }

        public async Task<bool> SendChat(string receiverID, ChatEntity chat)
        {
            try
            {
                if (chat == null)
                    throw new("Chat == null");

                if (_IsConnected)
                {
                    await _hubConnection.InvokeAsync("SendChatToClient", receiverID, chat);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error at SendMessageToServer", ex.Message, "ok");
                return false;
            }
        }

        ~ClientHub()
        {
            Disconnect().Wait();
        }
    }
}
