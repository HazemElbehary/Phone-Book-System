using Microsoft.EntityFrameworkCore;
using PhonebookSystem.Core.Domain.Contracts.Persistence;
using PhonebookSystem.Core.Domain.Entities;

namespace PhonebookSystem.Infrastructure.Persistence.Data
{
    internal class DbInitializer(ApplicationDbContext dbContext) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            var Migrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (Migrations.Any())
                await dbContext.Database.MigrateAsync();
        }

        public async Task SeedAsync()
        {
            if (dbContext.Contacts.Count() == 0)
            {
                await dbContext.Contacts.AddRangeAsync([
                    new Contact
                    {
                        Name = "Hazem",
                        PhoneNumber = "01234567891",
                        Email = "H@gmail.com"
                    },
                    new Contact
                    {
                        Name = "Ahmed",
                        PhoneNumber = "01112223344",
                        Email = "Ahmed@gmail.com"
                    },
                    new Contact
                    {
                        Name = "Mona",
                        PhoneNumber = "01098765432",
                        Email = "Mona@gmail.com"
                    },
                    new Contact
                    {
                        Name = "Yara",
                        PhoneNumber = "01555555555",
                        Email = "Yara@gmail.com"
                    },
                    new Contact
                    {
                        Name = "Ali",
                        PhoneNumber = "01222222222",
                        Email = "Ali@gmail.com"
                    }
                ]);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
