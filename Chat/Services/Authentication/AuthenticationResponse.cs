namespace CrossPlatformChat.Services.Authentication
{
    internal class AuthenticationResponse
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
