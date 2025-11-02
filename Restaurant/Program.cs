using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer;
using Restaurant.Controllers;
using Restaurant.DAL;
using Restaurant.DAL.Interfaces;
using Restaurant.DAL.Repositories;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Options;
using Restaurant.Services.Implementations;
using Restaurant.Services.Interfaces;
using System.Security.Claims;
using System.Text;
namespace Restaurant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("MSSQL");
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });
            builder.Services.AddScoped<IBaseRepository<Delivery>, DeliveryRepository>();
            builder.Services.AddScoped<IBaseRepository<ReservationsQuery>, ReservationRepository>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IBaseRepository<Position>, PositionsRepository>();
            builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();
            builder.Services.AddScoped<IPositionService, PositionService>();
            builder.Services.AddScoped<IAuthService,AuthService>();
            builder.Services.AddScoped<IUserService,AuthService>();
            builder.Services.AddScoped<IDeliveryService, DeliveryService>();
            builder.Services.AddSingleton<IEncryptionService, EncryptionService>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddCors();
            builder.Services.AddDbContext<AppDbContext>(options => 
            {
                options.UseSqlServer(connectionString);
            });
            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseCors(per => per.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.MapControllers();
            app.Run();


        }
    }
}
