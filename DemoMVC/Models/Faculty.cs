using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Faculty
    {
        [Required(ErrorMessage = "Id khoa không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Id phải lớn hơn 0")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên khoa không được để trống")]
        [StringLength(100, ErrorMessage = "Tên khoa tối đa 100 ký tự")]
        public string Name { get; set; } = string.Empty;

        // Quan hệ 1 - nhiều với Student
        public List<Student> Students { get; set; } = new List<Student>();
    }
}