using DataAccess.Entities;
using DataTransferObject;
using DataTransferObject.Category;
using Microsoft.AspNetCore.Mvc;

namespace Business.CategoryServices
{
    public interface ICategoryServices
    {
        public Task<ResponseDTO> AddUpdate(CategoryDto category);
        public IEnumerable<CategoryDto> GetAllCategories();
        public IQueryable<Category> GetAllCategoriesDatatable();
        public CategoryDto GetById(Guid Id);

        public Task<bool> Delete(Guid Id);

        public Task<KeyValuePair<int, List<CategoryDto>>> GetAllCategoriesServerSideGrid(Pagination pagination);
    }
}
