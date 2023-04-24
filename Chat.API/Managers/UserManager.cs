using Chat.API.Entities;
using Chat.API.Managers.User.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chat.API.Managers
{
    public enum RegistrationStatus
    {
        Success = 0,
        LoginOccupied = 1,
        InvalidLogin = 2,
        InvalidPassword = 3
    }
    public interface IUserManager
    {
        ClientResponse? Authenticate(string login, string password);
        (ClientResponse?, RegistrationStatus) Register(string login, string password);
        GeneralUserResponse? GetUserByID(int ID);
        public GeneralUserResponse? GetGeneralUserByUsername(string username);
    }

    public class UserManager : IUserManager
    {
        private readonly ChatAppContext db;
        public UserManager(ChatAppContext context) => db = context ?? throw new ArgumentNullException(nameof(context));
        readonly string securityKey = "1234567890123456";
        readonly DateTime tokenExpireTime = DateTime.Now.AddDays(1);

        public ClientResponse? Authenticate(string login, string password)
        {
            try
            {
                var entity = db.Users.SingleOrDefault(x => x.Login == login);
                if (entity == null) return null;

                var isPasswordsMatches = VerifyPassword(password, entity.HashedPassword);
                if (isPasswordsMatches == false) return null;

                return new ClientResponse
                {
                    ID = entity.ID,
                    Username = entity.Username,
                    Token = GenerateJWTToken(entity)
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GeneralUserResponse? GetGeneralUserByUsername(string username)
        {
            try
            {
                Users? entity = db.Users.SingleOrDefault(x => x.Username == username);
                if (entity == null) return null;

                return new GeneralUserResponse
                {
                    ID = entity.ID,
                    Username = entity.Username,
                    IsOnline = entity.IsOnline,
                    LastLoginTime = entity.LastLoginTime,
                    AvatarSource = entity.AvatarSource
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public (ClientResponse?, RegistrationStatus) Register(string login, string password)
        {
            try
            {
                var entity = db.Users.SingleOrDefault(x => x.Login == login);
                if (entity != null)
                    return (null, RegistrationStatus.LoginOccupied);

                if (login.Length < 3)
                    return (null, RegistrationStatus.InvalidLogin);

                if (password.Length < 4)
                    return (null, RegistrationStatus.InvalidPassword);

                var newUserToDB = new Users
                {
                    Login = login,
                    Username = login.ToUpper(),
                    HashedPassword = password,
                    RegistrationTime = DateTime.UtcNow,
                };

                var entry = db.Users.Add(newUserToDB);// remade for async
                db.SaveChanges();

                var Token = GenerateJWTToken(newUserToDB);
                return (new ClientResponse
                {
                    ID = entry.Entity.ID,
                    Login = login,
                    Username = login.ToUpper(),
                    HashedPassword = password,
                    Token = Token,
                }, RegistrationStatus.Success);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public GeneralUserResponse? GetUserByID(int ID)
        {
            try
            {
                var entity = db.Users.SingleOrDefault(x => x.ID == ID);
                if (entity == null) return null;

                return Convert.ChangeType(entity, typeof(GeneralUserResponse)) as GeneralUserResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        bool VerifyPassword(string enteredPassword, string storedPassword) => enteredPassword.Equals(storedPassword);
        string GenerateJWTToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(securityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()) }),
                Expires = tokenExpireTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
