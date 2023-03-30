using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatsCollectionVM : ChatModel
    {
        public bool NoChats { get; set; } = false;
        public ICommand NewChatCMD { get; set; }

        public ChatsCollectionVM()
        {
            if (Chats.Count == 0)
                NoChats = true;

            NewChatCMD = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new ChatCreationView());
            });
        }
    }
}