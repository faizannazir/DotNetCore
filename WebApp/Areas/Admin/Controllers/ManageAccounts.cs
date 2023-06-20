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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RolesDto rolesDto)
        { 
            if(ModelState.IsValid)
            {
            if(await roleServices.CreateRole(rolesDto))
            {
                return RedirectToAction("Index", "ManageAccounts", new { areas="Admin" });
            }
            }
            return View();
        }

        public IActionResult UpdateRole(string Id)
        {
            return View(roleServices.GetRole(Id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(RolesDto rolesDto)
        {
            if (ModelState.IsValid)
            {
                if (await roleServices.UpdateRole(rolesDto))
                {
                   return RedirectToAction("Index","ManageAccounts",new { areas = "Admin" });
                }
            }
            return View();
        }

        public IActionResult DeleteRole(string Id)
        {
            return View(roleServices.GetRole(Id));
        }

        [HttpPost,ActionName("DeleteRole")]
        public async Task<IActionResult> Delete(string Id)
        {
            if (await roleServices.DeleteRole(Id))
            {
                return RedirectToAction("Index", "ManageAccounts", new { areas = "Admin" });
            }
            return View();
        }
    }
}
