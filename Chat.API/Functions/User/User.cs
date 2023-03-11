namespace Chat.API.Functions.User
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string AvatarSource { get; set; } = null!;
        public bool IsOnline { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
