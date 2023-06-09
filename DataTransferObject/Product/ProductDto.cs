using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObject.Product
{
    public class ProductDto
    {
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        [ValidateNever]
        public string CategoryName { get; set; }
        [Required]
        public Guid CategoryId { get; set; }

        [ValidateNever]
        //public IEnumerable<CategoryDto> Categories { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        [ValidateNever]
        public IFormFile? File { get; set; }
    }
}
