using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhonebookSystem.Core.Domain.Contracts.Persistence;

namespace PhonebookSystem.Infrastructure.Persistence.Identity
{
    internal class ApplicationIdentityDbInitializer(UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager, ApplicationIdentityDbContext dbContext) 
        : IApplicationIdentityDbInitializer
    {
        public async Task InitializeAsync()
        {
            var Migrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (Migrations.Any())
                await dbContext.Database.MigrateAsync();
        }

        public async Task SeedAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                IdentityRole[] roles = [
                    new IdentityRole(){ Name = "Admin" },
                    new IdentityRole(){ Name = "User" },
                ];

                foreach (var role in roles)
                {
                    await _roleManager.CreateAsync(role);
                }
            }
            
            if (!_userManager.Users.Any())
            {
                IdentityUser[] users = 
                [
                    new IdentityUser()
                    {
                        UserName = "Ahmed",
                        Email = "Ahmed@gmail.com",
                        PhoneNumber = "01122334455"
                    },
                    new IdentityUser()
                    {
                        UserName = "Hazem",
                        Email = "Hazem@gmail.com",
                        PhoneNumber = "01122334455"
                    }
                 ];

                foreach (var user in users)
                {
                    user.EmailConfirmed = true;
                    user.PhoneNumberConfirmed = true;
                }
                await _userManager.CreateAsync(users[0], "P@ssw0rd");
                await _userManager.CreateAsync(users[1], "Hazemahmed011#");
                await _userManager.AddToRoleAsync(users[0], "Admin");
                await _userManager.AddToRoleAsync(users[1], "User");

            }
        }
    }
}
