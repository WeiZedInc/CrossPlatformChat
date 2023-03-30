using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.Utils.Helpers;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatVM : ChatsCollectionModel
    {
        public ChatHub ChatHub { get; set; } = new ChatHub();
    }
}
