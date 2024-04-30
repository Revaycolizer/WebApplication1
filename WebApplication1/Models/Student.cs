using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Student
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string? firstname { get; set; }
        [MaxLength(100)]
        public string? middlename { get; set; }
        [MaxLength(100)]
        public string? lastname { get; set; }

        public DateTime birthdate { get; set; }

        public string? guardian { get; set; }

        public string? img { get; set; }

        public long? phoneno { get; set; }

        public DateTime createdAt { get; set; }


    }
}
