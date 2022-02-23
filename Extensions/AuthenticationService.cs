using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PrepTeach.Extensions
{
    public static class AuthenticationService
    {
        public static void AddMyAuthentication(this IServiceCollection services, IConfiguration confi)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var str = confi["JwtToken:SecretKey"];
            var key = Encoding.ASCII.GetBytes(str);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                //x.Events = new JwtBearerEvents
                //{
                //    OnMessageReceived = context =>
                //    {
                //        context.Token = context.Request.Headers["Authorization"];
                //        return Task.CompletedTask;
                //    },

                //};
            });
        }
    }
}
