using System.ComponentModel.DataAnnotations;

namespace AdvancedAjax.Models
{
    public class Country
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Country Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string CurrencyName { get; set; }
        [Required]
        [StringLength(5)]
        [Display(Name = "Country Code ")]
        public string Code { get; set; }
    }

}
