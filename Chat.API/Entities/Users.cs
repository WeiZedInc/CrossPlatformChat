namespace Chat.API.Entities
{
    public class Users
    {
        public int ID { get; set; }
        public string Login { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string AvatarSource { get; set; } = null!;
        public bool IsOnline { get; set; }
        public DateTime LastLoginTime { get; set; }
        public byte[] StoreSalt { get; set; } = null!;
    }
}
