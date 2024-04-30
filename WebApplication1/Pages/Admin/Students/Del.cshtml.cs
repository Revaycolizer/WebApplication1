using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;

namespace WebApplication1.Pages.Admin.Students
{
    public class DelModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        public DelModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Students/Index");
                return;
            }

            var student = context.Students.Find(id);

            if (student == null)
            {
                Response.Redirect("/Admin/Students/Index");
                return;
            }
            string imgPath = environment.WebRootPath + "/students" + student.img;
            System.IO.File.Delete(imgPath);

            context.Students.Remove(student);
            context.SaveChanges();

            Response.Redirect("/Admin/Students/Index");
        }
    }
}

