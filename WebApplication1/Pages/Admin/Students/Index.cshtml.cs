using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.Admin.Students
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public List<Student> Students { get; set; } = new List<Student>();
        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void OnGet()
        {
            Students = context.Students.OrderByDescending(s => s.Id).ToList();
        }
    }
}
