using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AjaxNetCore.Models
{
    public class Students
    {
        [Key]
        public int Id 
        { 
            get; 
            set; 
        }
            [Required]
        [StringLength(20,MinimumLength =3,ErrorMessage ="Please Provide your name min length 3 max 20")]
            public string Name
            {
                get;
                set;
            }
            [Required]
            public string Email
            {
                get;
                set;
            }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "+92-***-*******",ApplyFormatInEditMode =true)]
        public string Phone
        {
            get;
            set;
        }
        [Display(Name = "Image")]
        [ValidateNever]
        public string ImageUrl {get; set;}
    }
}
