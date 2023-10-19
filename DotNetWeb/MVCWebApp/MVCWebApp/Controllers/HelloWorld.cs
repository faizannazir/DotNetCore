using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers
{
    public class HelloWorld : Controller
    {
        //Session["name"] = "John Doe";

        
        public IActionResult Index()
        {
            TempData["message"] = "Welcome";
            ViewBag.Name = "Faizan";
            ViewData["id"] = "5157";

            return View();
        }

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }

        public IActionResult AddEmployee()
        {
            return View();
        }

       
        //
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddEmployee([Bind("Name,Number")]Employee emp)
        [HttpPost]
        [HttpPost]
        public IActionResult AddEmployee(Employee emp)
        {
            if (string.IsNullOrEmpty(emp.Name))
            {
                ModelState.AddModelError(nameof(emp.Name), "Name is required");
            }
            if (string.IsNullOrEmpty(emp.Number))
            {
                ModelState.AddModelError(nameof(emp.Number), "Number is required");
            }

            if (ModelState.IsValid)
            {
                
                ViewData["emp"] = emp;
                ModelState.Clear();
                return View("EmployeeView", emp);
            }

            // If the model state is not valid, re-render the view with error messages
            return View(emp);
        }


        public IActionResult EmployeeView() 
        {
            Employee emp = new Employee();
            emp.Id = 5157;
            emp.Name = "Faizan Nazir";
            emp.Number = "03109161773";
            ViewData["emp"] = emp;
            return View(emp);
        }
        
    }
}
