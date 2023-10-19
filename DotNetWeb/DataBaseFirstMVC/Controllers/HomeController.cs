using DataBaseFirstMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DataBaseFirstMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataBaseFirstEfContext _context;
        public HomeController(ILogger<HomeController> logger, DataBaseFirstEfContext context)
        {
            _logger = logger;
            _context = context;
        }

       

        public IActionResult Index(String name)
        {
            IEnumerable<Student>? students;
            if (string.IsNullOrEmpty(name))
            {
                students = _context.Students;
            }
            else
            {
                students = _context.Students.Where(s => s.Name.Contains(name)).ToList();
            }

            return View(students);
        }
        public IActionResult Privacy()
        {

            return View();
        }
        public JsonResult PrivacyJson()
        {
            var names = new string[]{ "faizan", "saim" };
            return new JsonResult(Ok(names));
        }
        [HttpPost]
        public JsonResult PrivacyJson(string name)
        {
            return new JsonResult(Ok(name));    
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}