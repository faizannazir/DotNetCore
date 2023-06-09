using DataAccess.AppDbContext;
using DataAccess.Entities;
using DataAccess.GenericRepository;

namespace DataAccess.Repositories.ProductRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
