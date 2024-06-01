using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.RegisterDto
{
    public class RegisterDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password not matched")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }
        public string? City { get; set; }
        public string? StreetAddress { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

    }
}
