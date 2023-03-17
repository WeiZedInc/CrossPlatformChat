namespace CrossPlatformChat.Services.Authentication
{
    internal class AuthenticationRequest
    {
        public string Login { get; set; }
        public string HashedPassword { get; set; }
    }
}
