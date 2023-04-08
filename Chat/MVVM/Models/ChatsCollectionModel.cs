using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.Utils.Helpers;
using System.Collections.ObjectModel;

namespace CrossPlatformChat.MVVM.Models
{
    public class ChatsCollectionModel
    {
        public readonly ISQLiteService _dbservice;
        public bool NoChats { get; set; } = false;
        public ObservableDictionary<ChatEntity, ObservableCollection<MessageEntity>> ChatsAndMessagessDict { get; set; } = new();
        static bool _isInitialized = false;

        public ChatsCollectionModel()
        {
            _dbservice = ServiceHelper.Get<ISQLiteService>();

            #region Testing
            Test();

            ChatsAndMessagessDict.Add
                (
                new ChatEntity() { ID = 0, Name = "testChat", StoredSalt = CryptoManager.CreateSalt("testKey"), GeneralUsersID_JSON = "[0]", CreatedDate = DateTime.Now },
                new()
                );
            #endregion

            if (!_isInitialized)
                InitChats();
        }

        void Test()
        {
            _dbservice.DeleteAllInTableAsync<ChatEntity>();
            _dbservice.DeleteAllInTableAsync<MessageEntity>();
            _dbservice.DeleteAllInTableAsync<GeneralUserEntity>();
        }


        void InitChats()
        {
            //receiveing chats and msgs from bd
            List<ChatEntity> chatsTable = _dbservice.TableToListAsync<ChatEntity>().Result;
            List<MessageEntity> msgTable = _dbservice.TableToListAsync<MessageEntity>().Result;

            if (chatsTable != null && msgTable != null)
            {
                //messages for inserting to general collection
                ObservableCollection<MessageEntity> msgCollection;

                lock (ChatsAndMessagessDict)
                {
                    foreach (var chat in chatsTable)
                    {
                        //reciveing msgs for a specific chat
                        msgCollection = new(msgTable.Where(x => x.ChatID == chat.ID));

                        //adding to the general collection (chat, msgs)
                        ChatsAndMessagessDict.Add(chat, msgCollection);
                    }

                    if (ChatsAndMessagessDict == null || ChatsAndMessagessDict.Count == 0)
                        NoChats = true;
                }
            }

            _isInitialized = true;
        }
    }
}
