<<<<<<< HEAD:WebApp/Areas/Customer/Controllers/HomeController.cs
﻿using Bussiness.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
=======
﻿using Microsoft.AspNetCore.Mvc;
using MvcCore.Models;
>>>>>>> 3c9dcd0c32628e54684c52a3cd12ad4c0e7c57df:DotNetWeb/MvcCore/MvcCore/Controllers/HomeController.cs
using System.Diagnostics;

namespace MvcCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IProductServices productServices)
        {
            _logger = logger;
            _productServices = productServices;
        }

        public IActionResult Index()
        {
            return View(_productServices.GetAllProducts());
        }


        [Authorize]
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