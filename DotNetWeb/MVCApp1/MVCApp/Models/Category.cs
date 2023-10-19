using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MVCApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[a-z A-Z]{3,12}$")]
        [StringLength(20,MinimumLength =3)]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[0-9]{1,}$",ErrorMessage = "Minimum 1 digit is required")]
        [Range(minimum:0,maximum:100,ErrorMessage = "Orders must be between 0-100")]
        public int Orders { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Creation Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }
        [ValidateNever]
        public byte[] Image { get; set; }
    }
}
