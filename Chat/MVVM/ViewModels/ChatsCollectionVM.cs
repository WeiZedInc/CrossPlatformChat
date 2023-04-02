using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;
using CrossPlatformChat.Utils.Helpers;

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
                await App.Current.MainPage.Navigation.PushAsync(ServiceHelper.Get<ChatCreationView>());
            });
        }
    }
}