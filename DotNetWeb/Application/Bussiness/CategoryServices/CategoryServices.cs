using DataAccess.Entities;
using DataAccess.Repositories.CategoryRepository;
using DataTransferObject;
using DataTransferObject.Category;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Business.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryServices(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }


        //              AddUpdate 
        public async Task<ResponseDTO> AddUpdate(CategoryDto category)
        {
            if (category.Id != Guid.Empty && category.Id != null)
            {
                var dbCategory = await _categoryRepository.Get(category.Id);
                if (dbCategory != null)
                {
                    dbCategory.Name = category.Name;
                    dbCategory.UpdateDate = DateTime.Now;
                    dbCategory.TotalOrders = category.TotalOrders;
                    await _categoryRepository.Change(dbCategory);
                    return new ResponseDTO() { IsSuccessful = true, Message = "Category Updated", Url = "Category/Index" }; 
                }
                else
                {
                    return new ResponseDTO() { IsSuccessful = false, Message = "Category Not Exist", Url = "" };  
                }
            }
            else
            {
                Category category1 = new Category();
                category1.Id = Guid.NewGuid();
                category1.Name = category.Name;
                category1.TotalOrders = category.TotalOrders;
                category1.CreatedDate = DateTime.Now;
                await _categoryRepository.Add(category1);
                return new ResponseDTO() { IsSuccessful = true, Message = "Category Created", Url = "Category/Index" };
            }
        }



        // Delete By Id
        public async Task<bool> Delete(Guid Id)
        {
            if (Id != Guid.Empty)
            {
             await _categoryRepository.Delete(Id);
            return true;   
            }
            return false; 
        }



        // Get All enteries
        public IEnumerable<CategoryDto> GetAllCategories()
        {
            IEnumerable<CategoryDto> categoryDto = _categoryRepository.GetReadOnlyList().Where(u => u.IsDeleted != true).Select(x => new CategoryDto()
            {
                Id = x.Id,
                Name = x.Name,
                TotalOrders = x.TotalOrders
            });
            return categoryDto;

        }

        public IQueryable<Category> GetAllCategoriesDatatable()
        {
            return _categoryRepository.GetAll();
            //    .Select(c => new Category
            //    {
            //    Name = c.Name,
            //    TotalOrders = c.TotalOrders
            //});
        }


        //Get Single By Id 

        public CategoryDto GetById(Guid Id)
        {
            CategoryDto categoryDto = new CategoryDto();
            var category = _categoryRepository.GetAll().Where(u => u.Id == Id).FirstOrDefault();
            if (category == null)
            {
                return categoryDto;
            }
            categoryDto.Name = category.Name;
            categoryDto.TotalOrders = category.TotalOrders;
            return categoryDto;
        }


        // Pagination key value pair
        public async Task<KeyValuePair<int, List<CategoryDto>>> GetAllCategoriesServerSideGrid(Pagination pagination)
        {
            var query = _categoryRepository.GetReadOnlyList().Where(x => x.IsDeleted != true)
                .Include(x => x.Products).OrderByDescending(x => x.Id).AsQueryable();
            pagination.searchValue = string.IsNullOrEmpty(pagination.searchValue) ? null : pagination.searchValue;
            if (!string.IsNullOrEmpty(pagination.searchValue))
            {
                query = query.Where(x => x.Name.Contains(pagination.searchValue.Trim().ToLower()));
            }
            var dataQuery = query.Select(x => new CategoryDto
            {
                Name = x.Name,
                TotalOrders = x.TotalOrders,
                Id = x.Id
            });

            dataQuery = dataQuery.OrderBy($"{pagination.sortColumn} {pagination.sortColumnDirection}");
            var totalRecord = dataQuery.Count();
            if (pagination.take > 0)
            {
                var PendingList = await dataQuery.Skip(pagination.skip).Take(pagination.take).ToListAsync();
                return new KeyValuePair<int, List<CategoryDto>>(totalRecord, PendingList);
            }
            else
            {
                var PendingList = await dataQuery.Take(totalRecord).ToListAsync();
                return new KeyValuePair<int, List<CategoryDto>>(totalRecord, PendingList);
            }
        }

    }
}
