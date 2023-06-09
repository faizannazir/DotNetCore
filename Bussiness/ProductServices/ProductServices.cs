using DataAccess.Entities;
using DataAccess.Repositories.CategoryRepository;
using DataAccess.Repositories.ProductRepository;
using DataTransferObject;
using DataTransferObject.Product;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bussiness.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHostingEnvironment _env;
        public ProductServices(IProductRepository productRepository, ICategoryRepository categoryRepository, IHostingEnvironment env)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _env = env;
        }


        //              AddUpdate 
        public async Task<ResponseDTO> AddUpdate(ProductDto product)
        {
            if (product.File != null || !string.IsNullOrEmpty(product.ImageUrl))
            {
                string webRootPath = _env.ContentRootPath + Path.DirectorySeparatorChar.ToString() + "wwwroot";
                string productImagePath = @"images\Products";

                if (product.File != null)
                {
                    string extension = Path.GetExtension(product.File.FileName);
                    string fileName = Guid.NewGuid().ToString() + extension;
                    string productPath = Path.Combine(webRootPath, productImagePath, fileName).TrimEnd('\\');
                    product.ImageUrl = @"\images\products\" + fileName;
                    if (extension != null && (extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase)
                        || extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase)
                        || extension.Equals(".png", StringComparison.OrdinalIgnoreCase)
                        || extension.Equals(".gif", StringComparison.OrdinalIgnoreCase)))
                    {
                        using (var filestream = new FileStream(productPath, FileMode.Create))
                        {
                            await product.File.CopyToAsync(filestream);
                        }
                    }
                    else
                    {
                        return new ResponseDTO() { IsSuccessful = false, Message = "Please Upload Image type File ", Url = "" };
                    }
                }

            }
            if (product.Id != Guid.Empty && product.Id != null)
            {

                var dbProduct = await _productRepository.Get(product.Id);
                if (dbProduct != null)
                {
                    dbProduct.Name = product.Name;
                    dbProduct.UpdateDate = DateTime.Now;
                    dbProduct.Description = product.Description;
                    dbProduct.Price = product.Price;
                    dbProduct.ImageUrl = product.ImageUrl;
                    dbProduct.CategoryId = product.CategoryId;
                    await _productRepository.Change(dbProduct);
                    return new ResponseDTO() { IsSuccessful = true, Message = "Product Updated", Url = "Product/Index" };
                }
                else
                {
                    return new ResponseDTO() { IsSuccessful = false, Message = "Product not found", Url = "" };

                }

            }
            else
            {
                Product product1 = new Product();
                product1.Id = Guid.NewGuid();
                product1.Name = product.Name;
                product1.Description = product.Description;
                product1.Price = product.Price;
                product1.ImageUrl = product.ImageUrl;
                product1.CategoryId = product.CategoryId;
                product1.CreatedDate = DateTime.Now;
                await _productRepository.Add(product1);
                return new ResponseDTO() { IsSuccessful = true, Message = "Product Created", Url = "Product/Index" };
            }

        }





        // Delete By Id
        public async Task<bool> Delete(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return false;
            }
            await _productRepository.Delete(Id);
            return true;
        }



        // Get All enteries
        public IEnumerable<ProductDto> GetAllProducts()
        {
            var productDto = _productRepository.GetAll().Include(cattegory => cattegory.Category).Where(u => u.IsDeleted != true).Select(x => new ProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                CategoryName = x.Category.Name,
                CategoryId = x.CategoryId
            });
            return productDto;

        }


        //Get Single By Id 

        public ProductDto GetById(Guid Id)
        {
            ProductDto productDto = new ProductDto();
            if (Id != Guid.Empty)
            {
                var product = _productRepository.GetAll().Where(u => u.Id == Id).FirstOrDefault();
                if (product != null)
                {
                    productDto.Name = product.Name;
                    productDto.Description = product.Description;
                    productDto.Price = product.Price;
                    productDto.ImageUrl = product.ImageUrl;
                    productDto.CategoryId = product.CategoryId;
                    productDto.Categories = GetAllCategories(productDto);
                    return productDto;
                }
            }
            productDto.Categories = GetAllCategories(productDto);
            return productDto;
        }




        // Reterive all Categories from Category table 
        public IEnumerable<SelectListItem> GetAllCategories(ProductDto? p)
        {
            // Retrieve the categories from the repository or data source
            var categories = _categoryRepository.GetAll().Where(u => u.IsDeleted != true).Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name,
                Selected = p.CategoryId == u.Id
            });

            return categories.ToList();
        }

        //public IEnumerable<SelectListItem> UpdateCategory()
        //{
        //    var categories = _productRepository.GetAll()
        //        .Where(u => u.IsDeleted != true)
        //            .Include(x => x.Category)
        //                .Select(u => new SelectListItem
        //                {
        //                    Value = u.Category.Id.ToString(),
        //                    Text = u.Category.Name,
        //                    Selected = u.Category.Id == u.CategoryId
        //                }
        //              );
        //    return categories.ToList();
        //}

    }
}


