using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;
using CrossPlatformChat.Utils.Helpers;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatsCollectionVM : ChatsCollectionModel
    {
        public ICommand NewChatCMD { get; set; }

        public ChatsCollectionVM()
        {
            NewChatCMD = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(ServiceHelper.Get<ChatCreationView>());
            });

            ChatHub chatHub = new ChatHub();
            chatHub.Connect().Wait();
        }
    }
}