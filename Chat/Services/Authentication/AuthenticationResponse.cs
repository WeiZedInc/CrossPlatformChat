namespace CrossPlatformChat.Services.Authentication
{
    internal class AuthenticationResponse : BaseResponse
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
