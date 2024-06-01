using Microsoft.AspNetCore.Mvc;

namespace AjaxNetCore.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
