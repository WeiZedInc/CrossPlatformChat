using CrossPlatformChat.MVVM.Models;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class ChatsVM
    {
        public bool NoChats { get; set; } = true;
        public string Test { get; set; } = "Test";
        public ICommand TestCMD { get; set; }

        public readonly ISQLiteCRUD _dbservice;

        public ChatsVM(ISQLiteCRUD dbservice)
        {
            _dbservice = dbservice;
            TestCMD = new Command(async () =>
            {
                await _dbservice.AddAsync(new TestExternalUsers
                {
                    AvatarSource = "hui",
                    LastLoginTime = DateTime.Now,
                    IsOnline = false,
                    Username = "Test"
                });
                App.Current.MainPage.DisplayAlert("ok", _dbservice.GetListAsync().Result.Count.ToString(), "ok").GetAwaiter();
            });
        }

        public string GetLastMessageInChat(ChatInfoModel chat)
        {
            // get last users message in chat by chat id
            return string.Empty;
        }
    }
}
