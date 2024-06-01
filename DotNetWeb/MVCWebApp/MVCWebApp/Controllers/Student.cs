using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers
{
    public class Student : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(StudentModel student)
        {
            if(ModelState.IsValid)
            {
                ViewData["Success"] = "<script>alert(\"data submited\") </script> ";
                ModelState.Clear();
            }

            return View();
        }
    }
}
