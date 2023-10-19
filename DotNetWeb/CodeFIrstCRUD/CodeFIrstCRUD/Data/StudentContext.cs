using CodeFIrstCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFIrstCRUD.Data
{
    public class StudentContext: DbContext
    {

        public StudentContext(DbContextOptions<StudentContext> options): base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Courses> Courses { get; set; }

    }
}
