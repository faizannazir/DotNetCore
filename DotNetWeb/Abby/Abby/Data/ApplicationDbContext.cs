using Abby.Models;
using Microsoft.EntityFrameworkCore;

namespace Abby.Data
{
    public class ApplicationDbContext :DbContext
    {
        // required to establish db connection
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        // Category will be name of table in  database 
        public DbSet<Category> Category { get; set; }
    }
}