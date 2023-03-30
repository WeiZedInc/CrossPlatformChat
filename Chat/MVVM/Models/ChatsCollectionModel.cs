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
        public ObservableDictionary<ChatEntity, ObservableCollection<MessageEntity>> Chats { get; set; }

        public ChatsCollectionModel()
        {
            _dbservice = ServiceHelper.GetService<ISQLiteService>();
            Chats = new();


            //Test();
            InitChats();
        }

        void Test()
        {
            _dbservice.DeleteAllInTableAsync<ChatEntity>();
            _dbservice.DeleteAllInTableAsync<MessageEntity>();
        }
            

        void InitChats() 
        {
            //получаем чаты и сообщения с бд
            List<ChatEntity> chatsTable = _dbservice.TableToListAsync<ChatEntity>().Result;
            List<MessageEntity> msgTable = _dbservice.TableToListAsync<MessageEntity>().Result;

            //чаты для коллекции
            ObservableCollection<MessageEntity> msgCollection;

            foreach (var chat in chatsTable)
            {
                //получаем сообщения определенного чата и добавляем 
                msgCollection = new(msgTable.Where(x => x.ChatID == chat.ID));

                //удаляем обработанные с коллекции
                msgTable.RemoveAll(x => x.ChatID == chat.ID);

                //добавляем в словарь (чат, сообщения)
                Chats.Add(chat, msgCollection);
            }

            if (Chats == null || Chats.Count == 0)
                NoChats = true;
        }
    }
}
