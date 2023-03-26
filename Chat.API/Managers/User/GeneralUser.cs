namespace Chat.API.Managers.User
{
    public class GeneralUser
    {
        public string Username { get; set; }
        public string AvatarSource { get; set; } = "avatar.png";
        public bool IsOnline { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
