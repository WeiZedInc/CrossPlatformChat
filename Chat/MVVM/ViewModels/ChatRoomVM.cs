using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.Helpers;
using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.Utils.Helpers;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatRoomVM
    {
        int _chatID = 0;
        public ChatHub ChatHub { get; set; } = new();
        public ChatEntity Chat { get; set; }
        public GeneralUserEntity User { get; set; }
        public ClientEntity Client { get; set; }
        public ObservableCollection<MessageEntity> Messages { get; set; }
        public string MessageToEncrypt { get; set; }
        public bool IsRefreshing { get; set; }
        ISQLiteService _db;

        public ICommand SendMsgCMD { get; set; }
        public ICommand RefreshCMD { get; set; }

        public ChatRoomVM()
        {
            _db = ServiceHelper.Get<ISQLiteService>();

            InitChat();
            InitChatUsersData();

            SendMsgCMD = new Command(async () =>
            {
                bool result = await ChatHub.SendMessageToServer(0, new()
                {
                    ChatID = 0,
                    Message = MessageToEncrypt,
                    SentDate = DateTime.Now,
                    IsSent = true
                });
            });

            RefreshCMD = new Command(() =>
            {
                // todo
            });
        }

        void InitChatUsersData()
        {
            #region Testing
            _db.InsertAsync(new GeneralUserEntity()
            {
                ID = 0,
                AvatarSource = "dotnet_bot.svg",
                IsOnline = true,
                LastLoginTime = DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0)),
                Username = "TestUsername..."
            });
            #endregion
            Client = ClientHandler.LocalClient;
            int[] usersID = JsonConvert.DeserializeObject<int[]>(Chat.GeneralUsersID_JSON);
            if (usersID == null && usersID.Length == 0) return;

            if (usersID.Length == 1)
                User = _db.FindByIdAsync<GeneralUserEntity>(usersID[0]).Result;
            else 
            {
                // todo
            }
        }

        void InitChat()
        {
            var kvp = ServiceHelper.Get<ChatsCollectionModel>().ChatsAndMessagessDict.Where(x => x.Key.ID == _chatID).FirstOrDefault();
            if (kvp.Key == null)
                throw new Exception("Err at chatVM cotr");

            Chat = kvp.Key;
            Messages = kvp.Value;
        }
    }
}
