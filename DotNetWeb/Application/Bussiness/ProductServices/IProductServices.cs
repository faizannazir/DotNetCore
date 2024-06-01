using DataTransferObject;
using DataTransferObject.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Business.ProductServices
{
    public interface IProductServices
    {
        public Task<ResponseDTO> AddUpdate(ProductDto product);
        public IEnumerable<ProductDto> GetAllProducts();
        public ProductDto GetById(Guid Id);

        public Task<bool> Delete(Guid Id);
        public IEnumerable<SelectListItem> GetAllCategories(ProductDto? p);
        //public IEnumerable<SelectListItem> UpdateCategory();
    }
}
