using Chat.API.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chat.API.Functions.User
{
    public class UserFunction : IUserFunction
    {
        private readonly ChatAppContext context;
        public UserFunction(ChatAppContext context) => this.context = context;
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

        public User GetUserByID(int ID)
        {
            throw new NotImplementedException();
        }

        bool VerifyPassword(string enterePassword, byte[] storedSlat, string storedPassword)
        {
            string encryptedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(enterePassword, storedSlat, KeyDerivationPrf.HMACSHA1, 10000, 256/8));
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
