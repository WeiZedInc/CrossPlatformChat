using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.Utils.Helpers;
using System.Collections.ObjectModel;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatVM
    {
        public ChatHub ChatHub { get; set; } = new();
        public ChatEntity Chat { get; set; }
        public ObservableCollection<MessageEntity> Messages { get; set; }

        public ICommand SendMsgCMD { get; set; }

        public ChatVM()
        {
            var kvp = ServiceHelper.Get<ChatsCollectionModel>().ChatsAndMessagessDict.Where(x => x.Key.ID == 0).FirstOrDefault();
            if (kvp.Key == null)
                throw new Exception("Err at chatVM cotr");

            Chat = kvp.Key;
            Messages = kvp.Value;


            SendMsgCMD = new Command(async () =>
            {
                bool result = await ChatHub.SendMessageToServer(0, new()
                {
                    ChatID = 0,
                    Message = "Maecenas at dapibus ipsum, sed bibendum dolor. Phasellus vel urna id est varius venenatis. Nam tristique lacinia condimentum."
                });
            });
        }
    }
}
