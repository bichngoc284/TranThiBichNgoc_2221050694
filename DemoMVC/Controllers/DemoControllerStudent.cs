using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class DemoControllerStudent : Controller
    {
        public IActionResult StudentInfo()
        {
            Student st = new Student
            {
                StudentCode = "2221050694",
                FullName = "Trần Thị Bích Ngọc"
            };

            return View(st);
        }
    }
}
