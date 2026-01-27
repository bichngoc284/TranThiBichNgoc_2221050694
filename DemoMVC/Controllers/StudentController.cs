using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class StudentController : Controller
    {
        // Hiển thị form nhập dữ liệu
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Nhận dữ liệu Student từ View
        [HttpPost]
        public IActionResult Create(Student student)
        {
            // Xử lý dữ liệu
            ViewBag.Message = "Xin chào " + student.FullName +
                              " (Mã SV: " + student.StudentCode + ")";

            // Gửi lại dữ liệu về View
            return View(student);
        }
    }
}
