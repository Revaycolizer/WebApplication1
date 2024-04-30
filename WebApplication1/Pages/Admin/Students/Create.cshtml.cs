using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.Admin.Students
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public StudentDto StudentDto { get; set; } = new StudentDto();

        public CreateModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet()
        {
        }

        public string errorMessage = "";
        public string successMessage = "";
        public void OnPost() {
            if(StudentDto.src == null)
            {
                ModelState.AddModelError("StudentDto.src", "The image file is required");
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;
            }

            string newFile = DateTime.Now.ToString("yyyyMMddHHmmssff");
            newFile += Path.GetExtension(StudentDto.src!.FileName);
            string imgPath = environment.WebRootPath + "/students/" + newFile;

            using (var stream = System.IO.File.Create(imgPath))
            {
                StudentDto.src.CopyTo(stream);
            }

            Student student = new Student()
            {
                firstname = StudentDto.firstname,
                lastname = StudentDto.lastname,
                middlename = StudentDto.middlename,
                guardian = StudentDto.guardian,
                phoneno = StudentDto.phoneno,
                birthdate = StudentDto.birthdate,
                img = newFile,
            createdAt = DateTime.Now
            };

            context.Students.Add(student);
            context.SaveChanges();

            StudentDto.firstname = "";
            StudentDto.lastname = "";
            StudentDto.middlename = "";
            StudentDto.phoneno = null;
            StudentDto.guardian = "";
            StudentDto.src = null;

            ModelState.Clear();

            successMessage = "Student added successfully";

            Response.Redirect("/Admin/Students/Index");

        }
    }
}
