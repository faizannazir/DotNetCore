using Business.CategoryServices;
using DataTransferObject;
using DataTransferObject.Category;
using Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Common;

namespace WebApp.Areas.Admin.Controllers
{
    [BindProperties]
    [Area("Admin"), Authorize(Roles = UserRoles.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public IActionResult Index()
        {
            return View(_categoryServices.GetAllCategories());
        }

        //Add Update
        public IActionResult AddUpdate(Guid Id)
        {
            return View(_categoryServices.GetById(Id));
        }

        [HttpPost]
        public async Task<ResponseDTO> AddUpdate(CategoryDto categorydto)
        {
            if (ModelState.IsValid)
            {
                return await _categoryServices.AddUpdate(categorydto);
            }
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return new ResponseDTO() { IsSuccessful = false, Message = "Failed to submit", Response = errors, Url = "" };
        }


        //public async Task<IActionResult> Delete(Guid Id)
        //{
        //    return View(_categoryServices.GetById(Id));
        //}

        //[HttpDelete,ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            if (await _categoryServices.Delete(Id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }




        public async Task<IActionResult> GetPaginatedCategory()
        {
            try
            {
                var pagination =  PaginationParameter.Pagination(HttpContext);
                //var test = HttpContext.Request.Query["draw"];

                int recordsTotal = 0;
                var customerData = _categoryServices.GetAllCategoriesDatatable();
                if (!(string.IsNullOrEmpty(pagination.sortColumn) && string.IsNullOrEmpty(pagination.sortColumnDirection)))
                {
                    customerData = customerData.OrderBy(pagination.sortColumn + " " + pagination.sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(pagination.searchValue))
                {
                    customerData = customerData.Where(m => m.Name.Contains(pagination.searchValue));
                }
                recordsTotal = customerData.Count();
                var data = customerData.Skip(pagination.skip).Take(pagination.take).ToList();
                var jsonData = new { pagination.draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetAllCategoriesForView()
        {
            var paginationParameter = PaginationParameter.Pagination(HttpContext);
            var data = await _categoryServices.GetAllCategoriesServerSideGrid(paginationParameter);
            return Json(new { paginationParameter.draw, recordsFiltered = data.Key, recordsTotal = data.Key, data = data.Value });
        }
    }
}
