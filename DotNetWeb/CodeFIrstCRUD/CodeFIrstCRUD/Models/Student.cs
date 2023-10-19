using System.ComponentModel.DataAnnotations;

namespace CodeFIrstCRUD.Models
{
    public class Student
    {
        [Key]
        [Required (ErrorMessage ="Please Provide id")]
        public int Id { get; set; }
        [Required (ErrorMessage ="Please provide Student Name")]
        [StringLength (20,MinimumLength =3)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide Student Degree Name")]
        [StringLength(10, MinimumLength = 4, ErrorMessage ="Please Provide valid minimum 4 character maximum 10")]
        public string DegreeName { get; set; }

        [Required(ErrorMessage = "Please provide Student Age")]
        [Range (minimum:18,maximum:70, ErrorMessage = "You are not eligible")]
        public int Age { get; set; } 


    }
}
