using CrossPlatformChat.Services.Base;

namespace CrossPlatformChat.Services.Chat
{
    internal class FriendsRequest : IBaseRequest
    {
        public string Login { get; set; }
        public string HashedPassword { get; set; }
    }
}
