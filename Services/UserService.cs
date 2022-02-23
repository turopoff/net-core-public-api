using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PrepTeach.ViewModels;
using PrepTeach.Models;

namespace PrepTeach.Services
{
    public interface IUserService
    {
        Task<UserTokenView?> AuthenticateAsync(string login, string password);
    }

    public class UserService : IUserService
    {
        private readonly MyDbContext db;
        private readonly IConfiguration config;

        public UserService(MyDbContext _db, IConfiguration _config)
        {
            db = _db;
            config = _config;
        }

        public async Task<UserTokenView?> AuthenticateAsync(string login, string password)
        {
            User? user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
            if (user == null) return null;
            ClaimsIdentity claims = new(new Claim[]
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Login ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user.Login ?? "")
            });

            var str = config["JwtToken:SecretKey"];
            var key = Encoding.ASCII.GetBytes(str);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "PrepTeach",
                Audience = "PrepTeach",
                Subject = claims,
                Expires = DateTime.Now.AddYears(1),
                NotBefore = DateTime.Now.AddMinutes(-10),
                IssuedAt = DateTime.Now.AddMinutes(-10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            UserTokenView tokenView = new();
            tokenView.User = new UserView(user);
            tokenView.Token = tokenHandler.WriteToken(token);
            return tokenView;
        }
    }
}