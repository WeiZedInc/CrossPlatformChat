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
        public ObservableDictionary<ChatEntity, ObservableCollection<MessageEntity>> ChatsAndMessagessDict { get; set; }
        static bool _isInitialized = false;

        public ChatsCollectionModel()
        {
            _dbservice = ServiceHelper.Get<ISQLiteService>();
            ChatsAndMessagessDict = new();


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
            //получаем чаты и сообщения с бд
            List<ChatEntity> chatsTable = _dbservice.TableToListAsync<ChatEntity>().Result;
            List<MessageEntity> msgTable = _dbservice.TableToListAsync<MessageEntity>().Result;

            //чаты для коллекции
            ObservableCollection<MessageEntity> msgCollection;
            lock (ChatsAndMessagessDict)
            {
                foreach (var chat in chatsTable)
                {
                    //получаем сообщения определенного чата и добавляем 
                    msgCollection = new(msgTable.Where(x => x.ChatID == chat.ID));

                    //добавляем в словарь (чат, сообщения)

                    ChatsAndMessagessDict.Add(chat, msgCollection);
                }

                if (ChatsAndMessagessDict == null || ChatsAndMessagessDict.Count == 0)
                    NoChats = true;
            }

            _isInitialized = true;
        }
    }
}
