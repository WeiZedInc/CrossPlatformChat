using Chat.API.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Chat.API.Functions.User
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
        User? Authenticate(string login, string password);
        (User?, RegistrationStatus) Register(string login, string password);
        User? GetUserByID(int ID);
    }

    public class UserManager : IUserManager
    {
        private readonly ChatAppContext context;
        public UserManager(ChatAppContext context) => this.context = context;
        readonly string securityKey = "1234567890123456";
        readonly DateTime tokenExpireTime = DateTime.Now.AddDays(1);

        public User? Authenticate(string login, string password)
        {
            try
            {
                var entity = context.Users.SingleOrDefault(x => x.Login == login);
                if (entity == null) return null;

                var isPasswordsMatches = VerifyPassword(password, entity.StoreSalt, entity.Password);
                if (isPasswordsMatches == false) return null;

                var token = GenerateJWTToken(entity);

                return new User
                {
                    ID = entity.ID,
                    Username = entity.Username,
                    Token = token,
                };

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public (User?, RegistrationStatus) Register(string login, string password)
        {
            try
            {
                var entity = context.Users.SingleOrDefault(x => x.Login == login);
                if (entity != null) 
                    return (null, RegistrationStatus.LoginOccupied);

                //add logics for checking pass and login to be correct

                var passwordTuple = CreateHashedPassword(password);
                context.Users.Add(new Users 
                { 
                    Login = login, 
                    IsOnline = true,
                    Username = login,
                    LastLoginTime = DateTime.Now,
                    StoreSalt = passwordTuple.Salt, 
                    Password = passwordTuple.HashedPassword 
                }); // remade for async
                context.SaveChanges();

                return (new User
                {
                    ID = entity.ID,
                    Username = entity.Username,
                    Password = password,
                    Token = GenerateJWTToken(entity),
                    AvatarSource = entity.AvatarSource,
                    IsOnline = true,
                    LastLoginTime = DateTime.Now
                }, RegistrationStatus.Success);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User? GetUserByID(int ID)
        {
            try
            {
                var entity = context.Users.SingleOrDefault(x => x.ID == ID);
                if (entity == null) return null;

                return Convert.ChangeType(entity, typeof(User)) as User;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        (string HashedPassword, byte[] Salt) CreateHashedPassword(string password)
        {
            byte[] salt = new byte[128 / 8]; 
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                10000,
                256 / 8));

            return (hashedPassword, salt);
        }
        bool VerifyPassword(string enteredPassword, byte[] storedSlat, string storedPassword)
        {
            string encryptedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(enteredPassword, storedSlat, KeyDerivationPrf.HMACSHA1, 10000, 256/8));
            return encryptedPassword.Equals(storedPassword);
        }
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
