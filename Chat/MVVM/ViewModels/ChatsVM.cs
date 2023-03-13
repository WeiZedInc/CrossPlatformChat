using CrossPlatformChat.MVVM.Models;
using System.Collections.ObjectModel;

namespace CrossPlatformChat.MVVM.ViewModels
{
    internal class ChatsVM
    {
        public bool NoChats { get; set; } = true;
        ObservableCollection<ChatHandler> AllChats { get; set; }

        public string GetLastMessageInChat(ChatHandler chat)
        {
            // get last users message in chat by chat id
            return string.Empty;
        }
    }
}
