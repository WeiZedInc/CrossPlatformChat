using CrossPlatformChat.MVVM.Models;

namespace CrossPlatformChat.MVVM.ViewModels
{
    internal class ChatsVM
    {
        public bool NoChats { get; set; } = true;
        public string Test { get; set; } = "Test";
        public ICommand TestCMD { get; set; }

        public ChatsVM()
        {
            TestCMD = new Command(() =>
            {
                App.Instance.SaveTestAsync(new DBTestModel
                {
                    AvatarSource = "hui",
                    LastLoginTime = DateTime.Now,
                    IsOnline = false,
                    Username = "Test"
                });
                Test = App.Instance.GetInfo("testtbl").ToString();
            });
        }

        public string GetLastMessageInChat(ChatInfoModel chat)
        {
            // get last users message in chat by chat id
            return string.Empty;
        }
    }
}
