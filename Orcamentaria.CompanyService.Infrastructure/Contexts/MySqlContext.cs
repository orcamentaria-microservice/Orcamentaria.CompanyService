using Microsoft.EntityFrameworkCore;
using Orcamentaria.CompanyService.Domain.Models;
using Orcamentaria.CompanyService.Infrastructure.Configurations;

namespace Orcamentaria.CompanyService.Infrastructure.Contexts
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<DbContext> options)
        : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        }
    }
}
