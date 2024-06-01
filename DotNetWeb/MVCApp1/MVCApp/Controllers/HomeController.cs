using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCApp.Data;
using MVCApp.Models;
using System.Diagnostics;

namespace MVCApp.Controllers
{
    [BindProperties]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly DbContextClass _dbContextClass;

        public HomeController(ILogger<HomeController> logger, DbContextClass dbContextClass, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _dbContextClass = dbContextClass;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_dbContextClass.Category, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product p, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string rootpath = _webHostEnvironment.WebRootPath;
                if(file!=null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    
                    string path = Path.Combine(rootpath, "\\images\\products");

                    using (var filestream = new FileStream(Path.Combine(path,filename),FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    p.ImageUrl = @"\images\products\" + filename;
                }
                _dbContextClass.Add(p);
                var a = _dbContextClass.SaveChanges();
                if (a > 1)
                {
                    return NotFound("Unable to create product");
                }
                    return RedirectToAction("Index", "Products");
               
            }

            ViewData["CategoryId"] = new SelectList(_dbContextClass.Category, "Id", "Name");
            return View();
        }

        public IActionResult CreateCategory()
        {
            //ViewData["CategoryId"] = new SelectList(_dbContextClass.Category, "Id", "Name");
            return View();
        }

        [HttpPost]
        public  IActionResult CreateCategory(Category c, IFormFile file)
        {

            if (ModelState.IsValid)
            {  
                _dbContextClass.Add(c);
               var a = _dbContextClass.SaveChanges();
                if(a>1)
                {
                    return RedirectToAction("Index","Products");
                }
                return NotFound("Unable to create productCaregory");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}