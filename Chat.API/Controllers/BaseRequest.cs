namespace Chat.API.Controlls
{
    public class BaseRequest
    {
        public string Login { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;
        public string? FriendsJSON { get; set; }
    }
}
