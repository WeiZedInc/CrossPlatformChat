using CrossPlatformChat.Services.Base;

namespace CrossPlatformChat.Services.Chat.Friends
{
    internal class FriendsResponse : BaseResponse
    {
        public string FriendsJSON { get; set; }
        public int FriendsCount { get; set; }
    }
}
