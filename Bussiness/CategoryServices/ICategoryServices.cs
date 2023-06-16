using DataTransferObject;
using DataTransferObject.Category;

namespace Bussiness.CategoryServices
{
    public interface ICategoryServices
    {
        public Task<ResponseDTO> AddUpdate(CategoryDto category);
        public IEnumerable<CategoryDto> GetAllCategories();
        public CategoryDto GetById(Guid Id);

        public Task<bool> Delete(Guid Id);
    }
}
