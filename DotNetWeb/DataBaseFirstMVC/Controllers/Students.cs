using DataBaseFirstMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirstMVC.Controllers
{
    public class Students : Controller
    {
        private readonly DataBaseFirstEfContext _context;
        public Students(DataBaseFirstEfContext context)
        {
            _context = context;
        }
        public IActionResult Index(String name)
        {
            IEnumerable<Student>? students;
            if(string.IsNullOrEmpty(name))
            {
                 students = _context.Students;
            }
            else
            {
                students = _context.Students.Where(s=>s.Name.Contains(name)).ToList();
            }
            
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if(student == null)
            {
                return NotFound("Error Student could not be added");
            }
            if(ModelState.IsValid)
            {
               _context.Students.Add(student);
               _context.SaveChanges();
            }
            return View();
        }

    }
}
