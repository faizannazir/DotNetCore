using CodeFIrstCRUD.Data;
using CodeFIrstCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CodeFIrstCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentContext _context;
        public HomeController(ILogger<HomeController> logger,StudentContext context )
        {
            _logger = logger;
            _context = context;
        }
        

        public ActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student) 
        {
            if(ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound("Student Not found");
            }
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound("Student Not found");
            }
            return View(student);
        }

        //private bool StudentExists(int id)
        //{
        //    return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (student == null || _context.Students == null )
            {
                return NotFound("Student Not found");
            }
            if (ModelState.IsValid)
            {
                try
                {
                        _context.Students.Update(student);
                        _context.SaveChanges();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(_context.Students?.Any(e => e.Id == student.Id)).GetValueOrDefault())
                    {
                        return NotFound("Student Not found");
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }
            return View(student);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound("Student not found");
            }
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound("Student Not found");
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(Student student)
        {
            if (student == null || _context.Students == null)
            {
                return NotFound("No data Found ");
            }
            _context.Students.Remove(student);
             
            int a  = _context.SaveChanges();
            if (a > 0)
                {
                    return RedirectToAction("Index");
                }

            return View("Delete",student);
        }

        public IActionResult Detail(int? id)
        {
            if (_context.Students == null || id == null)
            {
                return NotFound("Student Not found");
            }
            var student =  _context.Students.Find(id);
            if (student == null)
            {
                return NotFound("Student Not found");
            }
            return View(student);
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