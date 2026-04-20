using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Mã Sinh viên")]
        public required string StudentCode { get; set; }

        [Required]
        [Display(Name = "Họ và Tên")]
        public required string FullName { get; set; }

        [Display(Name = "Khoa")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn khoa")]
        public int FacultyId { get; set; }
    }
}
