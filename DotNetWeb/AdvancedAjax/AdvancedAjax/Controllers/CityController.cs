using AdvancedAjax.Data;
using AdvancedAjax.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdvancedAjax.Controllers
{
    public class CityController : Controller
    {
        private readonly AppDbContext _context;

        public CityController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var city = _context.City;
            city.Include(c => c.Country).ToList();
            ViewData["CountryId"] = new SelectList(_context.Country, "id", "Name");
            return View(city);
        }

        public IActionResult Details(int id)
        {
            var city = _context.City;
            city.Include(c => c.Country).ToList();
            var detail = city.Find(id);

            if (detail != null)
            {
                return View(detail);
            }
            return NotFound("Error No record Against this id ");

        }

        //var countriesList =country.Select(c => new SelectListItem()
        //{
        //    Value = c.id.ToString(),
        //    Text = c.Name
        //}).ToList();
        //ViewBag.CountryId = countriesList;
        //city.Include(city => city.Country);
        //var countries = city.Include(c => c.Country);
        public IActionResult Create()
        {
            var city = _context.City;
            var countries = _context.Country;
            ViewData["CountryId"] = new SelectList(_context.Country, "id", "Name");
            //    countries.Select(c => new SelectListItem
            //{
            //    Text = c.Name,
            //    Value = c.id.ToString()
            //});
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(City city)
        {
            if (ModelState.IsValid)
            {
                _context.City.Add(city);
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

        public IActionResult Edit(int id)
        {
            var city = _context.City.Find(id);
            ViewData["CountryId"] = new SelectList(_context.Country, "id", "Name");
            return View(city);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(City city)
        {
            if (ModelState.IsValid)
            {
                _context.City.Update(city);
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
            var cities = _context.City;
            cities.Include(c => c.CountryId).ToList();
            var city = cities.Find(id);

            return View(city);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(City city)
        {
            _context.City.Remove(city);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }


            return RedirectToAction("Index");
        }
    }
}
