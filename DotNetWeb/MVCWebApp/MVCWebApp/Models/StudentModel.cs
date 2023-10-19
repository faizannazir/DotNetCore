using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCWebApp.Models
{
    public class StudentModel
    {
        [Key]
        [DisplayName("ID")]
        [Required (ErrorMessage ="Please provide your id")]
        public int StudentId { get; set; }
        
        [Required(ErrorMessage = "Please provide your name")]
        [StringLength (20,MinimumLength = 3, ErrorMessage ="Please Enter valid name ")]
        public string Name { get; set; }
        
        [Required (ErrorMessage =" Please provide your email")]
        [EmailAddress (ErrorMessage ="Please Enter Valid Email Address")]
        [RegularExpression ("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Please enter valid email address")]
        public string Email{ get; set; }
        [Required (ErrorMessage = "Please provide your Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+-={}|;:'\"",.<>?/]).{8,}$", ErrorMessage = "Please enter strong password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "confirm password is required")]
        [Compare("Password",ErrorMessage = "Password and ConfirmPassword not matched")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+-={}|;:'\"",.<>?/]).{8,}$", ErrorMessage = "Please enter strong password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please provide your age")]
        [Range (18, 60,ErrorMessage ="Please enter valid age")]
        public string Age { get; set; }
        [Required(ErrorMessage = "please provide your city")]
        public string City { get; set; }
    }
}
