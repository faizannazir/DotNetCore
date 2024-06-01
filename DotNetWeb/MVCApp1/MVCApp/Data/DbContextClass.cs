using Microsoft.EntityFrameworkCore;
using MVCApp.Models;

namespace MVCApp.Data
{
    public class DbContextClass :DbContext
    {
        public DbSet<Product>Product{ get; set; }
       public DbSet<Category>Category{ get; set; }
        public DbContextClass(DbContextOptions<DbContextClass> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Category>().ToTable("Categories");
        }
    }
}
