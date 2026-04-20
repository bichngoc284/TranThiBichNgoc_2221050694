using Microsoft.AspNetCore.Mvc;
using DemoMVC.Data;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class DemoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DemoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Name = "Trần Thị Bích Ngọc";
            ViewBag.StudentId = "2221050694";
            ViewBag.Year = 2026;

            return View();
        }
    }
}
