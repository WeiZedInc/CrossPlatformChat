namespace Chat.API.Managers.User
{
    public class BaseUser
    {
        public string Username { get; set; }
        public string AvatarSource { get; set; } 
        public bool IsOnline { get; set; }
        public DateTime LastLoginTime { get; set; }

        public BaseUser(string Username, bool isOnline, DateTime LastLoginTime, string AvatarSource = "avatar.png")
        {
            this.Username = Username;
            this.AvatarSource = AvatarSource;
            this.IsOnline = isOnline;
            this.LastLoginTime = LastLoginTime;
        }
    }
}
