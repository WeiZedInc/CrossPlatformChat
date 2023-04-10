namespace Chat.API.Entities
{
    public class Chats
    {
        public int ID { get; set; }
        public string AvatarSource { get; set; } = "avatar.png";
        public string GeneralUsersID_JSON { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
