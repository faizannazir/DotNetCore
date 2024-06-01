using AdvancedAjax.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvancedAjax.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}
