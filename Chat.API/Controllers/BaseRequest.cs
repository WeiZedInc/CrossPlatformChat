namespace Chat.API.Controlls
{
    public interface IBaseRequest
    {
        public string Login { get; set; }
        public string HashedPassword { get; set; }
        public string? FriendsJSON { get; set; }
    }
}
