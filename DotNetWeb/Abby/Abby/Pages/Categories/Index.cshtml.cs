using Abby.Data;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Abby.Pages.Categories
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        public IEnumerable<Category> Categories { get; set; }  
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Categories = _context.Category;
        }
    }
}
