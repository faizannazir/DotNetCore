using AdvancedAjax.Data;
using AdvancedAjax.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdvancedAjax.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var customer = _context.Customer;
            customer.Include(c=>c.City).ToList();
            return View(customer);
        }

        public IActionResult Details(int id)
        {
            var customer = _context.Customer;
            customer.Include(c => c.City).ToList();
            var detail = customer.Find(id);

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
            ViewData["CountryId"] = new SelectList(_context.Country, "id", "Name");
            ViewData["CityId"] = new SelectList(_context.City, "id", "Name");
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customer.Add(customer);
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
            var customers = _context.Customer;
            customers.Include(c => c.City).ToList();
            var customer = customers.Find(id);

            ViewData["CountryId"] = new SelectList(_context.Country, "id", "Name");
            ViewData["CityId"] = customer.City;
            return View(customer);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customer.Update(customer);
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
        {  var customers = _context.Customer;
            customers.Include(c => c.City);
            var customer = customers.Find(id);

            return View(customer);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            _context.Customer.Remove(customer);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        // Json Functions 

        [HttpGet]
        public IActionResult getCountries()
        {

            return Json("");
        }

        [HttpGet]
        public IActionResult getCities(int countryId)
        {
            List<SelectListItem> cities = new List<SelectListItem>();
            cities = _context.City
                .Where(c => c.CountryId == countryId)
                .OrderBy(n => n.Name)
                .Select(s => new SelectListItem
                {
                    Value = s.id.ToString(),
                    Text = s.Name
                }).ToList();
            return Json(cities);
        }


        //[HttpGet]
        //public IActionResult getCity(int countryId)
        //{
        //    List<SelectListItem> cities = new List<SelectListItem>();
        //    cities = _context.City
        //        .Where(c => c.CountryId == countryId)
        //        .OrderBy(n => n.Name)
        //        .Select(s => new SelectListItem
        //        {
        //            Value = s.id.ToString(),
        //            Text = s.Name
        //        }).ToList();
        //    return Json(cities);
        //}


        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            if (customer == null)
            {
                return View("Error");
            }
            _context.Customer.Add(customer);
            _context.SaveChanges();
            return Json("User created");

        }
  



    [HttpPut]
    public IActionResult UpdateCustomer(Customer customer)
    {
        if (customer == null)
        {
            return View("Error");
        }
        _context.Customer.Update(customer);
        _context.SaveChanges();
        return Json("User created");

    }
}
}