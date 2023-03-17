using CrossPlatformChat.MVVM.Models.Users;
using CrossPlatformChat.Services.Database;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class RegisterVM : ClientModel
    {
        public ICommand RegisterCommand { get; set; }
        public ICommand TestCMD { get; set; }
        public ICommand GoToLoginViewCommand { get; set; }
        ISQLiteService db;
        readonly string _registrationPath;
        public RegisterVM(ISQLiteService db)
        {
            this.db = db;
            _registrationPath = "/Registration/Register";
            RegisterCommand = new Command(() =>
            {
                if (IsProcessing) return;
                if (string.IsNullOrWhiteSpace(LoginInput) || string.IsNullOrWhiteSpace(PasswordInput)) return;

                IsProcessing = true;
                TryRegisterAsync().GetAwaiter().OnCompleted(() => IsProcessing = false);  // can return registration completion
            });
            TestCMD = new Command(() =>
            {
                App.Current.MainPage.DisplayAlert("ok", db.FirstOrDefault<ClientData>().Result.HashedPassword, "ok").GetAwaiter(); //
            });
            GoToLoginViewCommand = new Command(async () => await App.Current.MainPage.Navigation.PopAsync());
        }

        async Task<bool> TryRegisterAsync()
        {
            try
            {
                var passTuple = CryptoManager.CreateHashedPassword(PasswordInput, KeyWordInput);
                var request = new AuthenticationRequest
                {
                    Login = LoginInput,
                    HashedPassword = passTuple.HashedPassword
                };
                var response = await APIManager.Instance.Authenticate(request, _registrationPath);
                if (response.StatusCode == 200)
                {
                    Test = $"Registration successful!\nUsername: {response.UserName}\nToken:{response.Token}" +
                        $"\nHashedPassword:{passTuple.HashedPassword}\nKeyWord:{KeyWordInput}\nSalt:{passTuple.Salt}";
                    db.DeleteAllInTableAsync<ClientData>().Wait(); // for testing only
                    await db.InsertAsync(new ClientData()
                    { // todo: need to be completed with all data
                        HashedPassword = passTuple.HashedPassword,
                        KeyWord = KeyWordInput,
                        StoredSalt = passTuple.Salt
                    });
                    return true;
                }
                else
                {
                    Test = response.StatusMessage;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Test = ex.Message;
                return false;
            }
        }
    }
}
