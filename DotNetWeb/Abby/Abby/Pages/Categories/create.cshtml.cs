using Abby.Data;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Abby.Pages.Categories
{
    public class createModel : PageModel
    {
        private ApplicationDbContext _context;
        public Category Category { get; set; }
        public createModel(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public void OnGet()
        {
        }
         
        public async Task<IActionResult> OnPost(Category category)
        {
            if(ModelState.IsValid)
            {
                await _context.Category.AddAsync(category);

                await _context.SaveChangesAsync();
                TempData["done"] = "Category Created";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
