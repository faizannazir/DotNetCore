using Abby.Data;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Abby.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private ApplicationDbContext _context;
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(int id)
        {
            Category = _context.Category.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var category = _context.Category.FirstOrDefault(x=> x.Id == Category.Id);
            if (category != null)
            {
                 _context.Category.Remove(category);

                await _context.SaveChangesAsync();
                TempData["done"] = "Category Deleted";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
