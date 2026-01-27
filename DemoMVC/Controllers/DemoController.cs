using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Name = "Trần Thị Bích Ngọc";
            ViewBag.StudentId = "2221050694";
            ViewBag.Year = 2026;

            return View();
        
    }
}
}
