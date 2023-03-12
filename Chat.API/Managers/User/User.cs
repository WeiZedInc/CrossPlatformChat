namespace Chat.API.Functions.User
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string AvatarSource { get; set; } = "avatar.png";
        public bool IsOnline { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string Token { get; set; } = null!;
    }
}
