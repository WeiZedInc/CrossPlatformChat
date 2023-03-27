using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatsCollectionVM : ChatModel
    {
        public ChatsCollectionVM()
        {
            if (AllChats.Count == 0)
                NoChats = true;

            NewChatCMD = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new ChatCreationView());
            });
        }

        public string GetLastMessageInChat(ChatModel chat)
        {
            // get last users message in chat by chat id
            return string.Empty;
        }
    }
}