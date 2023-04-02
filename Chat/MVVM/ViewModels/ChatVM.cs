using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.Utils.Helpers;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatVM
    {
        public ChatHub ChatHub { get; set; } = new ChatHub();
        public ChatsCollectionModel Model { get; set; }

        public ChatVM()
        {
            Model = ServiceHelper.Get<ChatsCollectionModel>();
        }
    }
}
