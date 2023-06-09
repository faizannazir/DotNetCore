using DataTransferObject;
using DataTransferObject.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bussiness.ProductServices
{
    public interface IProductServices
    {
        Task<ResponseDTO> AddUpdate(ProductDto product);
        IEnumerable<ProductDto> GetAllProducts();
        public ProductDto GetById(Guid Id);

        public Task<bool> Delete(Guid Id);
        public IEnumerable<SelectListItem> GetAllCategories(ProductDto? p);
        //public IEnumerable<SelectListItem> UpdateCategory();
    }
}
