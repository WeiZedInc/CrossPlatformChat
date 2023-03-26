using CrossPlatformChat.Services.Base;

namespace CrossPlatformChat.Utils.Request_Response.Chat
{
    internal class UserEntityResponse : BaseResponse
    {
        public int ID { get; set; }
        public string Username { get; set; } = null!;
        public string AvatarSource { get; set; } = "avatar.png";
        public bool IsOnline { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
