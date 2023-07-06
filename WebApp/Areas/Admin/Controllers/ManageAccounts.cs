using Business.RoleServices;
using DataTransferObject.Roles;
using Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = UserRoles.Role_Admin)]
    public class ManageAccounts : Controller
    {
        private readonly IRoleServices roleServices;

        public ManageAccounts(IRoleServices roleServices)
        {
            this.roleServices = roleServices;   
        }
        public async Task<IActionResult> Index()
        {
            return View(roleServices.RoleList());
        }

        public IActionResult CreateRole()
        {
            return PartialView("_CreateRole");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RolesDto rolesDto)
        {
            if (ModelState.IsValid)
            {
                if (await roleServices.CreateRole(rolesDto))
                {
                    return Json(new {success = true});
                }
            }
            var partialView = PartialView("_CreateRole", rolesDto);
            //return Json(new ResponseDTO() { IsSuccessful = false, ViewResult = partialView});
            return partialView;
        }

        public IActionResult UpdateRole(string Id)
        {
            return PartialView("_UpdateRole",roleServices.GetRole(Id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(RolesDto rolesDto)
        {
            if (ModelState.IsValid)
            {
                if (await roleServices.UpdateRole(rolesDto))
                {
                    return Json(new { success = true });
                }
            }
            return PartialView("_UpdateRole",rolesDto);
        }

        public IActionResult DeleteRole(string Id)
        {
            return PartialView("_DeleteRole",roleServices.GetRole(Id));
        }

        [HttpPost,ActionName("DeleteRole")]
        public async Task<IActionResult> Delete(string Id)
        {
            if (await roleServices.DeleteRole(Id))
            {
                return Json(new { success = true });
            }
            return PartialView("_DeleteRole");
        }



        public async Task<IActionResult> RolesList()
        {
            return PartialView("_RoleTable",roleServices.RoleList());
        }
    }
}
