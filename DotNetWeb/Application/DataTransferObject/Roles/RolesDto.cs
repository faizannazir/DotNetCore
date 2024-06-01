using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObject.Roles
{
    public class RolesDto
    {
        [ValidateNever]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [ValidateNever]
        public string TotalUsers { get; set; }
    }
}
