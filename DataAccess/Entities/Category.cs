using DataAccess.Entities.BaseEntity;

namespace DataAccess.Entities
{
    public class Category : BaseEntity.BaseEntity
    {
        public Category()
        {
                Products = new HashSet<Product>();  
        }
        public string Name { get; set; }

        public int TotalOrders { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
