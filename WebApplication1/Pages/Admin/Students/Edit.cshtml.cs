using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.Admin.Students
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;
        [BindProperty]
        public StudentDto StudentDto { get; set; } = new StudentDto();
        
        public Student Student { get; set; } = new Student();

        public string errorMessage = "";
        public string successMessage = "";

        public EditModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            if(id == null)
            {
                Response.Redirect("/Admin/Students/Index");
                return;
            }

            var student = context.Students.Find(id);
            if(student == null)
            {
                Response.Redirect("/Admin/Students/Index");
                return;
            }

            StudentDto.firstname = student.firstname;
            StudentDto.lastname = student.lastname;
            StudentDto.middlename = student.middlename;
            StudentDto.guardian = student.guardian;
            StudentDto.phoneno = student.phoneno;
            StudentDto.birthdate = student.birthdate;

            Student = student;
        }

        public void OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Students/Index");
                return;
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all required fields";
                return;
            }

            var student = context.Students.Find(id);
            if (student == null)
            {
                Response.Redirect("/Admin/Students/Index");
                return;
            }


            string newFile = student.img;
            if(StudentDto.src != null)
            {
                newFile = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFile += Path.GetExtension(StudentDto?.src?.FileName);

                string imgPath = environment.WebRootPath + "/students/" + newFile;
                using (var stream = System.IO.File.Create(imgPath))
                {
                    StudentDto?.src?.CopyTo(stream);
                }

                string oldPath = environment.WebRootPath + "/students/" + student.img;
                System.IO.File.Delete(oldPath);
            }


            student.firstname = StudentDto.firstname;
            student.lastname = StudentDto.lastname;
            student.middlename = StudentDto.middlename;
            student.guardian = StudentDto.guardian;
            student.phoneno = StudentDto.phoneno;
            student.birthdate = StudentDto.birthdate;
            student.img = newFile;

            context.SaveChanges();

            Student = student;

            successMessage = "Student updated successfully";

            Response.Redirect("/Admin/Students/Index");
        }
    }
}
