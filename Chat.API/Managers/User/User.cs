using Chat.API.Managers.User;

namespace Chat.API.Functions.User
{
    public class User : GeneralUser
    {
        public int ID { get; set; }
        public string Login { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
