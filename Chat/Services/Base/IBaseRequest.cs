namespace CrossPlatformChat.Services.Base
{
    internal interface IBaseRequest
    {
        public string Login { get; set; }
        public string HashedPassword { get; set; }
    }
}
