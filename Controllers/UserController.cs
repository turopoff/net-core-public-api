using Microsoft.AspNetCore.Mvc;
using PrepTeach.Models;
using PrepTeach.Services;
using PrepTeach.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace PrepTeach.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Foydalanuvchilar Bilan Ishlash")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly MyDbContext db;

        public UserController(IUserService _userService, MyDbContext db)
        {
            userService = _userService;
            this.db = db;
        }

        [HttpPost("Login")]
        [SwaggerOperation("Tizimga Kirish (Token Olish)")]
        public async Task<ResponseView<UserTokenView?>> AuthAsync([FromBody] LoginView model)
        {
            ResponseView<UserTokenView?> response = new();
            UserTokenView? user = await userService.AuthenticateAsync(model.Login, model.Password);
            bool isExist = user != null;
            response.Data = user;
            response.Message = isExist ? null : "User Is Not Found";
            response.Status = isExist ? 0 : 1;
            return response;
        }

        [HttpPost("Register")]
        [SwaggerOperation("Ro'yxatdan O'tish")]
        public async Task<ResponseView<UserView>> RegisterAsync([FromBody] RegisterView payload)
        {
            bool IsExist = db.Users.Any(x => x.Login == payload.Login);
            ResponseView<UserView> response = new();
            if (!IsExist)
            {
                User user = new()
                {
                    Login = payload.Login,
                    Password = payload.Password,
                    FirstName = payload.FirstName,
                    LastName = payload.LastName,
                    FatherName = payload.FatherName,
                };

                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
                response.Data = new UserView(user);
                response.Status = 1;
            }
            else
            {
                response.Message = "User already exists";
            }

            return response;
        }
    }
}
