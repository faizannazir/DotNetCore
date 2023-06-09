using Bussiness.CategoryServices;
using DataTransferObject;
using DataTransferObject.Category;
using Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers
{
    [BindProperties]
    [Area("Admin"),Authorize(Roles = UserRoles.Role_Admin)]
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


    }
}
