using DataTransferObject;
using DataTransferObject.Category;

namespace Bussiness.CategoryServices
{
    public interface ICategoryServices
    {
        Task<ResponseDTO> AddUpdate(CategoryDto category);
        IEnumerable<CategoryDto> GetAllCategories();
        public CategoryDto GetById(Guid Id);

        public Task<bool> Delete(Guid Id);
    }
}
