using AdvancedAjax.Data;
using AdvancedAjax.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedAjax.Controllers
{
    public class CountryController : Controller
    {
        private readonly AppDbContext _context;

        public CountryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var countries = _context.Country;
            return View(countries);
        }

        public IActionResult Details(int id)
        {
            var detail = _context.Country.Find(id);
            if (detail != null)
            {
                return View(detail);
            }
            return NotFound("Error No record Against this id ");

        }

        public IActionResult Create()
        {
            //var country = _context.Country;country
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Country.Add(country);
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Update(int id)
        {
            var country = _context.Country.Find(id);
            return View(country);

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Update(Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Country.Update(country);
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }

                return RedirectToAction("Index");
            }

            return View();
        }


        public IActionResult Delete(int id)
        {
            var country = _context.Country.Find(id);

            return View(country);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(Country country)
        {
            _context.Country.Remove(country);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
