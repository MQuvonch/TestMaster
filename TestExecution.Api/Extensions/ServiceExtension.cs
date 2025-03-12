using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using TestExecution.Data.IRepositories;
using TestExecution.Data.Repositories;
using TestExecution.Service.Interfaces;
using TestExecution.Service.Mapping;
using TestExecution.Service.Services;

namespace TestExecution.Api.Extensions
{
    public static class ServiceExtension
    {
        public static void AddCustomExtension(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<IAuthService, AuthService>();    
            Services.AddScoped<ITestService, TestService>();
            Services.AddScoped<IQuestionService, QuestionService>();
            Services.AddScoped<IOptionService, OptionService>();    
            Services.AddScoped<IUserAttemptService, UserAttemptService>();
            Services.AddScoped<IUserAnswerService, UserAnswerService>();
            Services.AddScoped<IEmailService, EmailService>();
            Services.AddMemoryCache();

            Services.AddHttpContextAccessor();
            Services.AddHttpClient();
            Services.AddAutoMapper(typeof(Mapping));
        }

        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestExecution.Api", Version = "v1" });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[]{ }
            }
        });
            });
        }

        public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            
            
        }
    }
}
