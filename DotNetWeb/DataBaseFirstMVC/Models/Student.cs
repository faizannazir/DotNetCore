using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataBaseFirstMVC.Models;

public partial class Student
{
    [Key]
    public int Id { get; set; }
    [Required (ErrorMessage = "Please Provide your name")]
    [StringLength (12, MinimumLength = 3,ErrorMessage ="Please provide correct student ")]
    public string Name { get; set; } = null!;
    [Required (ErrorMessage = "Please provide gender")]
    [RegularExpression("^(male|female|Male|Female|MALE|FEMALE)$", ErrorMessage = "Please Provide correct gender information about student")]
    public string Gender { get; set; } = null!;

    [Required (ErrorMessage = "Please provide student age.")]
    [Range (5, 70, ErrorMessage = "Please provide age between 5 and 70")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Please provide student standard .")]
    [Range(5, 70, ErrorMessage = "Please provide age between 1 and 12")]
    public int Standard { get; set; }

    [Required(ErrorMessage = "Please provide student city.")]
    [StringLength(12, MinimumLength = 3, ErrorMessage = "Please provide correct student city")]
    public string city { get; set; } = null!;
}
