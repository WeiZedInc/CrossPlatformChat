﻿using CrossPlatformChat.Database;
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
        ChatEntity _currentChat;

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

        public ChatHub(ChatEntity currentChat, ChatsCollectionModel model)
        {
            _Model = model;
            _currentChat = currentChat;

            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                _connectionPath = "http://10.0.2.2:5066/ChatHub";
            else
                _connectionPath = "http://localhost:5066/ChatHub";

            Application.Current.Dispatcher.Dispatch(async () => await Connect());
        }

        public async Task Connect()
        {
            if (IsConnected || IsBusy)
                return;
            try
            {
                IsBusy = true;

                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_connectionPath)
                    .Build();

                _hubConnection.Closed += async (error) =>
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "Hub connection closed", "ok");
                    IsConnected = false;
                    await Connect();
                };

                _hubConnection.On<MessageEntity>("ReceiveMessage", ReceiveMessage);

                await _hubConnection.StartAsync();
                await _hubConnection.InvokeAsync("AddToGroup", _currentChat.ID.ToString());
                IsConnected = true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error at Hub Connect", ex.Message, "ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task Disconnect()
        {
            if (IsConnected && _hubConnection != null)
            {
                await _hubConnection.InvokeAsync("RemoveFromGroup", _currentChat.ID.ToString());
                await _hubConnection.StopAsync();
            }
        }

        void ReceiveMessage(MessageEntity messageEntity)
        {
            try
            {
                if (_Model == null || _currentChat == null)
                    throw new("Model || Chat == null");

                lock (_Model.ChatsAndMessagessDict)
                {
                    messageEntity.Message = CryptoManager.DecryptMessage(messageEntity.EncryptedMessage, _currentChat.StoredSalt, messageEntity.InitialVector);
                    messageEntity.IsSent = false;

                    _Model.ChatsAndMessagessDict[_currentChat].Add(messageEntity);
                    ServiceHelper.Get<ISQLiteService>().InsertAsync(messageEntity).Wait();
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error at ReceiveMessage", ex.Message, "ok").Wait();
            }
        }

        public async Task<bool> SendMessageToServer(MessageEntity messageEntity)
        {
            try
            {
                if (_Model == null || _currentChat == null)
                    throw new("Model || Chat == null");

                if (IsConnected && messageEntity != null)
                {
                    var (encryptedMessage, initialVector) = CryptoManager.EncryptMessage(_currentChat.StoredSalt, messageEntity.Message);

                    MessageEntity msgToBeSent = new()
                    {
                        ID = messageEntity.ID,
                        ChatID = messageEntity.ChatID,
                        EncryptedMessage = encryptedMessage,
                        InitialVector = initialVector,
                        IsSent = messageEntity.IsSent,
                        Message = "Encrypted",
                        SenderID = messageEntity.SenderID,
                        SentDate = DateTime.Now,
                    };
                    await _hubConnection.InvokeAsync("SendMessageToGroup", _currentChat.ID.ToString(), msgToBeSent);
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

        ~ChatHub()
        {
            Disconnect().Wait();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
