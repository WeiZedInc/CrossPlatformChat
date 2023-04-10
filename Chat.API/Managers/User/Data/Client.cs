namespace Chat.API.Managers.User.Data
{
    public class Client : GeneralUser
    {
        public int ID { get; set; }
        public string Login { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
