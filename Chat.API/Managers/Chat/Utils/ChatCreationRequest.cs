namespace Chat.API.Managers.Chat.Utils
{
    public class ChatCreationRequest
    {
        public string AvatarSource { get; set; } = "avatar.png";
        public string GeneralUsersID_JSON { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
