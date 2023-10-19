﻿using Business.ProductServices;
using DataTransferObject;
using DataTransferObject.Product;
using Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = UserRoles.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        // Index Page Show All Products 
        public IActionResult Index()
        {
            return View(_productServices.GetAllProducts());
        }

        // AddUpdate Perform get method to retrive create and update form 
        public IActionResult AddUpdate(Guid Id)
        {
            return View(_productServices.GetById(Id));
        }


        // Post data for create and Update

        [HttpPost]
        public async Task<ResponseDTO> AddUpdate(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                return await _productServices.AddUpdate(productDto);
            }
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return new ResponseDTO() { IsSuccessful = false, Message = "Failed to submit", Response = errors, Url = "" };
        }



        //public async Task<IActionResult> Delete(Guid Id)
        //{
        //        return View(model: _categoryServices.GetById);
        //}


        //[HttpPost,ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            if (await _productServices.Delete(Id))
            {
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}