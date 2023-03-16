using CrossPlatformChat.MVVM.Models;
using CrossPlatformChat.MVVM.Views;

namespace CrossPlatformChat.MVVM.ViewModels
{
    internal class RegisterVM : ClientModel
    {
        public ICommand RegisterCommand { get; set; }
        public ICommand GoToLoginViewCommand { get; set; }
        readonly string _registrationPath;
        public RegisterVM()
        {
            _registrationPath = "/Registration/Register";
            RegisterCommand = new Command(() =>
            {
                if (IsProcessing) return;
                if (string.IsNullOrWhiteSpace(LoginInput) || string.IsNullOrWhiteSpace(PasswordInput)) return;

                IsProcessing = true;
                TryRegisterAsync().GetAwaiter().OnCompleted(() => IsProcessing = false);  // can return registration completion
            });
            GoToLoginViewCommand = new Command(async () => await App.Current.MainPage.Navigation.PopAsync());
        }

        async Task<bool> TryRegisterAsync()
        {
            try
            {
                var request = new AuthenticationRequest
                {
                    Login = LoginInput,
                    Password = PasswordInput
                };
                var response = await ServerProvider.Instance.Authenticate(request, _registrationPath);
                if (response.StatusCode == 200)
                {
                    Test = $"Registration successful!\nUsername: {response.UserName}\nToken:{response.Token}";
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
