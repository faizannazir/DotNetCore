using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedAjax.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(12)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(12)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please provide valid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name="Country")]
        [NotMapped]
        public int Countryld
        {
            get; set;
        }
        [Required]
        [ForeignKey("City")]
        [Display(Name ="City")]
        public int Cityld { get; set; }
        public virtual City City { get; set; }
        [Required(ErrorMessage = "Please choose the Customer Photo url")]
        [StringLength(maximumLength: 500)]
        [Display(Name = "Photo Url")]
        public string Photourl { get; set; }

        [Display(Name = "Profile Photo")]
        //[Required(ErrorMessage = "This field is required")]
        [NotMapped]
        public IFormFile Picture { get; set; }

        [NotMapped]
        public string BriefPhotoName { get; set; }
    }
}
