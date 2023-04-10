using CrossPlatformChat.Helpers;
using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatsCollectionVM
    {
        public ICommand NewChatCMD { get; set; }
        public ChatsCollectionModel Model { get; set; }

        public ChatsCollectionVM()
        {
            Model = ServiceHelper.Get<ChatsCollectionModel>();
            NewChatCMD = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new ChatCreationView());
            });
        }
    }
}