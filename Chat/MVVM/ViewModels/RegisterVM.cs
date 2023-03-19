using CrossPlatformChat.MVVM.Models.Users;
using CrossPlatformChat.MVVM.Views;
using CrossPlatformChat.Services.Database;
using CrossPlatformChat.Utils;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class RegisterVM : ClientModel
    {
        public ICommand RegisterCommand { get; set; }
        public ICommand GoToLoginViewCommand { get; set; }

        ISQLiteService db;
        public RegisterVM()
        {
            db = ServiceHelper.GetService<ISQLiteService>();

            RegisterCommand = new Command(async () =>
            {
                if (IsProcessing) return;
                if (string.IsNullOrWhiteSpace(LoginInput) || string.IsNullOrWhiteSpace(PasswordInput)) return; // remake checks to be optimal

                IsProcessing = true;
                if (await TryRegisterAsync())
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ChatsView());
                }
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
                var response = await APIManager.Instance.HttpRequest<AuthenticationResponse>(request, RequestPath.Register);
                if (response.StatusCode == 200)
                {
                    await db.InsertAsync(new ClientData()
                    {
                        HashedPassword = passTuple.HashedPassword,
                        KeyWord = KeyWordInput,
                        StoredSalt = passTuple.Salt,
                        IsOnline = true,
                        LastLoginTime = DateTime.UtcNow,
                        RegistrationTime = DateTime.UtcNow,
                        Password = PasswordInput,
                        Login = LoginInput,
                        Username = LoginInput,
                        AvatarSource = "default.png",
                        Token = response.Token
                    });

                    Test = $"Registration successful!\nToken:{response.Token}" +
                        $"\nUsername:{response.UserName}\nID:{response.ID}";
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
            finally 
            { 
                IsProcessing = false;
            }
        }
    }
}
