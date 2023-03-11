namespace Chat.API.Controlls.Authenticate
{
    public class AuthenticateRequest
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
