using CrossPlatformChat.Helpers;
using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatsCollectionVM
    {
        public ICommand NewChatCMD { get; set; }
        public ICommand EnterChatCMD { get; set; }
        public ChatsCollectionModel Model { get; set; }

        public ChatsCollectionVM()
        {
            Model = ServiceHelper.Get<ChatsCollectionModel>();
            NewChatCMD = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new ChatCreationView());
            });

            EnterChatCMD = new Command<int>(async (id) =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new ChatRoomView(id));
            });
        }
    }
}