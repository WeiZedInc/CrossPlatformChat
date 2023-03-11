namespace Chat.API.Functions.User
{
    public interface IUserFunction
    {
        User? Authenticate(string login, string password);
        User GetUserByID(int ID);
    }
}
