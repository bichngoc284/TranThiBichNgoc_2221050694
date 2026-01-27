using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class DemoControllerSubmit : Controller
    {
        // Hiển thị form (GET)
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Nhận dữ liệu từ form (POST)
        [HttpPost]
        public IActionResult Index(string hoTen)
        {
            ViewBag.Message = "Xin chào " + hoTen;
            return View();
        }
    }
}
