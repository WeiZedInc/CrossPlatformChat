using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.Utils.Helpers;
using System.Collections.ObjectModel;

namespace CrossPlatformChat.MVVM.Models
{
    public class ChatModel
    {
        public readonly ISQLiteService _dbservice;
        public static ObservableDictionary<ChatEntity, ObservableCollection<MessageEntity>> Chats { get; set; }

        public ChatModel()
        {
            _dbservice = ServiceHelper.GetService<ISQLiteService>();

            InitChats();
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
        }
    }
}
