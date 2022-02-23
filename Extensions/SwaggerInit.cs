using Microsoft.OpenApi.Models;

namespace PrepTeach.Extensions
{
    public static class SwaggerInit
    {
        public static IServiceCollection AddMySwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PrepTeach Server",
                });
                c.EnableAnnotations();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Token qo'yish tartibi (Bearer <token>)",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
            });
            });
            //services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }


        public static IApplicationBuilder UseMySwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
                c.DisplayRequestDuration();
                //c.DocExpansion(DocExpansion.None);
                c.EnableValidator();
            });

            return app;
        }
    }
}
