using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.Helpers;
using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.Utils.Helpers;
using Newtonsoft.Json;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatRoomVM : ChatRoomModel
    {
        public ChatRoomVM(int id)
        {
            _db = ServiceHelper.Get<ISQLiteService>();

            SendMsgCMD = new Command(SendMsg);

            RefreshCMD = new Command(() =>
            {
                // todo
            });

            InitChat(id);
        }

        async void SendMsg()
        {
            if (_isSending || string.IsNullOrWhiteSpace(MessageToEncrypt))
                return;

            _isSending = true;

            MessageEntity message = new()
            {
                ChatID = Chat.ID,
                Message = MessageToEncrypt,
                SentDate = DateTime.Now,
                IsSent = true,
                SenderID = ClientHandler.LocalClient.ID
            };

            Messages.Add(message);
            await _db.InsertAsync(message);

            if (await ChatHub.SendMessageToServer(message))
                _isSending = false;
        }

        public void InitChat(int id)
        {
            var model = ServiceHelper.Get<ChatsCollectionModel>();
            var kvp = model.ChatsAndMessagessDict.Where(x => x.Key.ID == id).FirstOrDefault();
            if (kvp.Key == null)
                throw new Exception("Err at chatVM cotr");

            Chat = kvp.Key;
            Messages = kvp.Value;

            ChatHub = new(Chat, model);

            InitChatUsersData();
        }

        void InitChatUsersData()
        {
            int[] usersID = JsonConvert.DeserializeObject<int[]>(Chat.GeneralUsersID_JSON);
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
