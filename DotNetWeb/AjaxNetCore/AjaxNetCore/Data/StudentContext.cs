using AjaxNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace AjaxNetCore.Data
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions options): base(options) { }

        public DbSet<Students> Students { get; set; }  
    }
}
