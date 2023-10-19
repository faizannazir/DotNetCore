using System.ComponentModel.DataAnnotations;

namespace DataTransferObject.Category
{
    public class CategoryDto
    {

        public Guid? Id { get; set; }
        [Required(ErrorMessage ="Name Field is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Total Orders Field is required")]
        public int TotalOrders { get; set; }

    }
}
