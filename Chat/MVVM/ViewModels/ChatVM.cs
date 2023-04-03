using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.Utils.Helpers;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatVM
    {
        public ChatHub ChatHub { get; set; } = new();
        public ChatsCollectionModel Model { get; set; }

        public ICommand SendMsgCMD { get; set; }

        public ChatVM()
        {
            Model = ServiceHelper.Get<ChatsCollectionModel>();
            SendMsgCMD = new Command(async () =>
            {
                bool result = await ChatHub.SendMessageToServer(0, new()
                {
                    ChatID = 0,
                    Message = "test"
                });
                await App.Current.MainPage.DisplayAlert("Received", result.ToString(), "ok");
            });
        }
    }
}
