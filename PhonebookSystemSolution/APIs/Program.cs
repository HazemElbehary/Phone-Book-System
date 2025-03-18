using Microsoft.EntityFrameworkCore;
using PhonebookSystem.APIs.Extensions;
using PhonebookSystem.Infrastructure.Persistence;
using PhonebookSystem.Infrastructure.Persistence.Data;
using PhonebookSystem.Core.Application;
using Microsoft.AspNetCore.Identity;
using PhonebookSystem.Infrastructure.Persistence.Identity;
using Microsoft.IdentityModel.Tokens;
using PhonebookSystem.Core.Application.Abstraction.DTOs.Auth;
using System.Text;

namespace APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Services
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddPresistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();

            #region Identity Service

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(IdentityOptions =>
            {
                IdentityOptions.User.RequireUniqueEmail = true;

                IdentityOptions.SignIn.RequireConfirmedAccount = true;
                IdentityOptions.SignIn.RequireConfirmedEmail = true;
                IdentityOptions.SignIn.RequireConfirmedPhoneNumber = true;

                IdentityOptions.Lockout.AllowedForNewUsers = true;
                IdentityOptions.Lockout.MaxFailedAccessAttempts = 10;
                IdentityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

            }).AddEntityFrameworkStores<ApplicationIdentityDbContext>();


            builder.Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = "Bearer";
                Options.DefaultChallengeScheme = "Bearer";
            })
                .AddJwtBearer((configurationOptions) =>
                {
                    configurationOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.FromMinutes(0),
                        ValidIssuer = builder.Configuration["JwtSettings:Issure"],
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!))
                    };
                });

            #endregion

            #region Add CORS

            builder.Services.AddCors(CorsOptions =>
            {
                CorsOptions.AddPolicy("PhonebookSystemPolicy", PolicyBuilder =>
                {
                    PolicyBuilder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });

            #endregion

            #endregion

            var app = builder.Build();

            #region DataBase Initialization

            // Run migrations at startup
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var identityDbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
                dbContext.Database.Migrate();
                identityDbContext.Database.Migrate();
            }

            // Initialize DB
            await app.InitializeApplication();
            #endregion


            #region Piplines

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("PhonebookSystemPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}