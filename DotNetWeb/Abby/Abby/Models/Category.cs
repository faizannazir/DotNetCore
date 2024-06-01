using System.ComponentModel.DataAnnotations;

namespace Abby.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }    

    }
}
