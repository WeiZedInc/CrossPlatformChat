using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat.MVVM.ViewModels
{
    internal class RegisterVM
    {
        public BaseUserModel UserModel { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand GoToLoginViewCommand { get; set; }
        readonly string _registrationPath;
        public RegisterVM()
        {
            UserModel = new BaseUserModel();
            _registrationPath = "/Registration/Register";
            RegisterCommand = new Command(() =>
            {
                if (UserModel.IsProcessing) return;
                if (string.IsNullOrWhiteSpace(UserModel.LoginInput) || string.IsNullOrWhiteSpace(UserModel.PasswordInput)) return;

                UserModel.IsProcessing = true;
                TryRegisterAsync().GetAwaiter().OnCompleted(() => UserModel.IsProcessing = false);  // can return registration completion
            });
            GoToLoginViewCommand = new Command(async () => await App.Current.MainPage.Navigation.PopAsync());
        }

        async Task<bool> TryRegisterAsync()
        {
            try
            {
                var request = new AuthenticationRequest
                {
                    Login = UserModel.LoginInput,
                    Password = UserModel.PasswordInput
                };
                var response = await ServiceProvider.Instance.Authenticate(request, _registrationPath);
                if (response.StatusCode == 200)
                {
                    UserModel.Test = $"Registration successful!\nUsername: {response.UserName}\nToken:{response.Token}";
                    return true;
                }
                else
                {
                    UserModel.Test = response.StatusMessage;
                    return false;
                }
            }
            catch (Exception ex)
            {
                UserModel.Test = ex.Message;
                return false;
            }
        }
    }
}
