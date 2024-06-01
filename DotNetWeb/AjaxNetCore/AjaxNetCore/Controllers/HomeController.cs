using AjaxNetCore.Data;
using AjaxNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AjaxNetCore.Controllers
{
    [BindProperties]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentContext _context;
        private readonly IWebHostEnvironment _web;

        public HomeController(ILogger<HomeController> logger, StudentContext context, IWebHostEnvironment web)
        {
            _logger = logger;
            _context = context;
            _web = web;
        }

        public IActionResult Index()
        {
            var s = _context.Students;
            return View(s);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create(string? s)
        {
            var students = _context.Students;
            //Json(students);
            return View();
        }  
        //IFormFile img
            //if (img != null)
            //{  
            //    string root = Path.GetFullPath(_web.WebRootPath).TrimEnd('\\');
            //    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
            //    string filePath = Path.Combine(root, @"images\", fileName);

            //    using (var file = new FileStream(filePath, FileMode.Create))
            //    {
            //        img.CopyTo(file);
            //    }
            //    student.ImageUrl = @"~\images\" + fileName;
            //}
        [HttpPost]
        public async Task<IActionResult> Create(Students student)
        {
            if (!ModelState.IsValid) return BadRequest("Enter required fields");

            _context.Add(student);
            _context.SaveChanges();
            // Clear the model state
            ModelState.Clear();

            return Students();
            //var students = _context.Students;
            ////return this.Ok($"Form Data received! {student} created");
            //return Json(students);
        }

        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Upload(IFormFile img)
        {
            string root = Path.GetFullPath(_web.WebRootPath).TrimEnd('\\');
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
            string filePath = Path.Combine(root, @"images\", fileName);

            using (var file = new FileStream(filePath, FileMode.Create))
            {
                img.CopyTo(file);
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
public IActionResult Students()
{
    var students = _context.Students;
    //Json(students);
    return Json(students);
}
    }
}




