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
        ChatEntity _chat;
        public ChatHub ChatHub { get; set; }
        public GeneralUserEntity User { get; set; }
        public ObservableCollection<MessageEntity> Messages { get; set; }
        public string MessageToEncrypt { get; set; }
        public bool IsRefreshing { get; set; }
        ISQLiteService _db;

        public ICommand SendMsgCMD { get; set; }
        public ICommand RefreshCMD { get; set; }

        public ChatRoomVM()
        {
            _db = ServiceHelper.Get<ISQLiteService>();

            SendMsgCMD = SendMsg;

            RefreshCMD = new Command(() =>
            {
                // todo
            });
        }

        async Command SendMsg()
        {
            MessageEntity message = new();
            message.ChatID = _chat.ID; // _chat is null
            message.Message = MessageToEncrypt;
            message.SentDate = DateTime.Now;
            message.IsSent = true;

            Messages.Add(message); // Messages is null
            _db.InsertAsync(message).Wait();

            await ChatHub.SendMessageToServer(message);

            return Task.CompletedTask;
        }

        public void InitChat(int id)
        {
            var model = ServiceHelper.Get<ChatsCollectionModel>();
            var kvp = model.ChatsAndMessagessDict.Where(x => x.Key.ID == id).FirstOrDefault();
            if (kvp.Key == null)
                throw new Exception("Err at chatVM cotr");

            _chat = kvp.Key;
            Messages = kvp.Value;

            ChatHub = new(_chat, model);

            InitChatUsersData();
        }

        void InitChatUsersData()
        {
            int[] usersID = JsonConvert.DeserializeObject<int[]>(_chat.GeneralUsersID_JSON);
            if (usersID == null && usersID.Length == 0) return;

            if (usersID.Length == 1)
                User = _db.FindByIdAsync<GeneralUserEntity>(usersID[0]).Result;
            else
            {
                // todo
            }
        }
    }
}
