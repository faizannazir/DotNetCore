using Abby.Data;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Abby.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private ApplicationDbContext _context;
        public Category Category { get; set; }
        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public void OnGet( int id )
        {
            Category = _context.Category.Find(id);
        }
         
        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                _context.Category.Update(Category);

                await _context.SaveChangesAsync();
                TempData["done"] = "Category Updated";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
