using Microsoft.EntityFrameworkCore;
using PhonebookSystem.Core.Domain.Entities;

namespace PhonebookSystem.Infrastructure.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
