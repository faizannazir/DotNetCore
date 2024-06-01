using DataAccess.AppDbContext;
using DataAccess.Entities;
using DataAccess.GenericRepository;



namespace DataAccess.Repositories.CategoryRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
