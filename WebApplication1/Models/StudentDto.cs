using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class StudentDto
    {
        public string? firstname { get; set; }
        [Required,MaxLength(100)]
        public string? middlename { get; set; }
        [Required,MaxLength(100)]
        public string? lastname { get; set; }
        [Required]
        public DateTime birthdate { get; set; }
        [Required]
        public string? guardian { get; set; }

        public IFormFile? src { get; set; }
        [Required]
        public long? phoneno { get; set; }

    }
}
