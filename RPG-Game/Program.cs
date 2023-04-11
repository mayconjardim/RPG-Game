using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RPG_Game.Data;
using RPG_Game.Services.CharacterService;
using RPG_Game.Services.WeaponService;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace RPG_Game
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer
            (builder.Configuration.GetConnectionString("Database")));
            builder.Services.AddControllers();
            
 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => {
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme, e.g. \"bearer {token} \"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<ICharacterService, CharacterService>();
            builder.Services.AddScoped<IWeaponService, WeaponService>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                options.TokenValidationParameters = new TokenValidationParameters
                 {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                      .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                  ValidateIssuer = false,
                  ValidateAudience = false
                };
             });

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}