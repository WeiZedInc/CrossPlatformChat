using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatsCollectionVM : ChatsCollectionModel
    {
        public ICommand NewChatCMD { get; set; }

        public ChatsCollectionVM()
        {
            NewChatCMD = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new ChatCreationView());
            });
        }
    }
}