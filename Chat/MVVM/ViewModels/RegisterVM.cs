using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;
using CrossPlatformChat.Services.Base;
using CrossPlatformChat.Utils.Helpers;

namespace CrossPlatformChat.MVVM.ViewModels
{
    public class RegisterVM : ClientModel
    {
        public ICommand RegisterCommand { get; set; }
        public ICommand GoToLoginViewCommand { get; set; }

        ISQLiteService db;
        public RegisterVM()
        {
            db = ServiceHelper.Get<ISQLiteService>();

            RegisterCommand = new Command(async () =>
            {
                if (IsProcessing) return;
                if (string.IsNullOrWhiteSpace(LoginInput) || string.IsNullOrWhiteSpace(PasswordInput)) return; // remake checks to be optimal

                IsProcessing = true;
                if (await TryRegisterAsync())
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ChatsCollectionView());
                }
            });

            GoToLoginViewCommand = new Command(async () => await App.Current.MainPage.Navigation.PopAsync());
        }

        async Task<bool> TryRegisterAsync()
        {
            try
            {
                var (Hash, Salt) = CryptoManager.CreateHash(PasswordInput);
                var request = new BaseRequest
                {
                    Login = LoginInput,
                    HashedPassword = Hash
                };
                var response = await ServiceHelper.Get<APIManager>().HttpRequest<AuthenticationResponse>(request, RequestPath.Register, HttpMethod.Post);
                if (response.StatusCode == 200)
                {
                    await db.InsertAsync(new ClientEntity()
                    {
                        HashedPassword = Hash,
                        StoredSalt = Salt,
                        IsOnline = true,
                        LastLoginTime = DateTime.UtcNow,
                        RegistrationTime = DateTime.UtcNow,
                        Login = LoginInput,
                        Username = LoginInput,
                        AvatarSource = "default.png",
                        ID = response.ID,
                        Token = response.Token
                    });
                    return true;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", response.StatusMessage, "ok");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "ok");
                return false;
            }
            finally
            {
                IsProcessing = false;
            }
        }
    }
}
