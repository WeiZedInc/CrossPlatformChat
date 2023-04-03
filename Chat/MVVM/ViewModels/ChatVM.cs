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
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce eget malesuada nisl, vel congue risus. Morbi fringilla viverra libero, eu dictum sem rhoncus vitae. Mauris purus est, molestie vitae scelerisque nec, cursus quis augue. In eget pharetra mauris. Etiam aliquet nisi in arcu vehicula eleifend. Suspendisse id metus eu arcu efficitur euismod. Fusce nisi ipsum, congue sit amet dolor non, porttitor tristique lorem. Cras at diam a neque dignissim pharetra. Donec venenatis risus ex. Suspendisse varius pharetra enim nec congue. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Maecenas at dapibus ipsum, sed bibendum dolor. Phasellus vel urna id est varius venenatis. Nam tristique lacinia condimentum."
                });
                await App.Current.MainPage.DisplayAlert("Message sent", result.ToString(), "ok");
            });
        }
    }
}
