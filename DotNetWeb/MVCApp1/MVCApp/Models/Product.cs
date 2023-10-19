using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage= "This Field is required")]
        [RegularExpression("^[a-z A-Z]{3,20}$",ErrorMessage = "Product name length must be in range of (3-20) only alphabet")]
        public string Name { get; set; }

        [Required(ErrorMessage ="This Field is Required")]
        [RegularExpression("^[0-9]{3,}",ErrorMessage ="should be at least 3 digits")]
        public double Price { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]

        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
