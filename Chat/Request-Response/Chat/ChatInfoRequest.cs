using CrossPlatformChat.Services.Base;

namespace CrossPlatformChat.Utils.Request_Response.Chat
{
    public class ChatInfoRequest
    {
        public string AvatarSource { get; set; } = "avatar.png";
        public string GeneralUsersID_JSON { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
