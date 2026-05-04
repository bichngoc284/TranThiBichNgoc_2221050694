using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Models;
using DemoMVC.Data;
using DemoMVC.ViewModels;
using DemoMVC.Models.ViewModels;
using OfficeOpenXml;

namespace DemoMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        private void PopulateDropDownLists(object? selectedFaculty = null)
        {
            var facultiesQuery = _context.Faculties
                .OrderBy(f => f.Name)
                .Select(f => new { f.Id, f.Name });

            ViewBag.FacultyId = new SelectList(facultiesQuery, "Id", "Name", selectedFaculty);
        }

        private List<StudentVM> GetStudentsWithFacultyByLinq(string? searchString, int? facultyId)
        {
            var students =
                from s in _context.Students
                join f in _context.Faculties on s.FacultyId equals f.Id
                where (string.IsNullOrEmpty(searchString) || s.FullName.Contains(searchString))
                      && (!facultyId.HasValue || facultyId.Value == 0 || s.FacultyId == facultyId.Value)
                orderby s.FullName
                select new StudentVM
                {
                    Id = s.Id,
                    StudentCode = s.StudentCode,
                    FullName = s.FullName,
                    FacultyName = f.Name
                };

            return students.ToList();
        }

        private async Task<StudentVM?> GetStudentWithFacultyByLinqAsync(int id)
        {
            var student = await (from s in _context.Students
                                 join f in _context.Faculties on s.FacultyId equals f.Id
                                 where s.Id == id
                                 select new StudentVM
                                 {
                                     Id = s.Id,
                                     StudentCode = s.StudentCode,
                                     FullName = s.FullName,
                                     FacultyName = f.Name
                                 })
                                .FirstOrDefaultAsync();

            return student;
        }

        // Hiển thị danh sách Students
        public IActionResult Index(string? searchString, int? facultyId)
        {
            PopulateDropDownLists(facultyId);
            ViewBag.SearchString = searchString;
            ViewBag.SelectedFacultyId = facultyId;

            var students = GetStudentsWithFacultyByLinq(searchString, facultyId);
            return View(students);
        }
// IMPORT EXCEL
[HttpPost]
public async Task<IActionResult> ImportExcel(IFormFile file)
{
    if (file == null || file.Length == 0)
        return BadRequest("File không hợp lệ");

    using (var stream = new MemoryStream())
    {
        await file.CopyToAsync(stream);

        using (var package = new OfficeOpenXml.ExcelPackage(stream))
        {
            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                string studentCode = worksheet.Cells[row, 1].Text;
                string fullName = worksheet.Cells[row, 2].Text;
                string facultyText = worksheet.Cells[row, 3].Text;

                if (string.IsNullOrEmpty(studentCode) || string.IsNullOrEmpty(fullName))
                    continue;

                if (!int.TryParse(facultyText, out int facultyId))
                    continue;

                // kiểm tra Faculty tồn tại
                var facultyExists = _context.Faculties.Any(f => f.Id == facultyId);
                if (!facultyExists)
                    continue;

                // kiểm tra trùng mã SV
                var exists = _context.Students.Any(s => s.StudentCode == studentCode);
                if (exists)
                    continue;

                var student = new Student
                {
                    StudentCode = studentCode,
                    FullName = fullName,
                    FacultyId = facultyId
                };

                _context.Students.Add(student);
            }

            await _context.SaveChangesAsync();
        }
    }

    return RedirectToAction(nameof(Index));
}
        // Hiển thị form nhập dữ liệu
        [HttpGet]
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // Nhận dữ liệu Student từ View
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateDropDownLists(student.FacultyId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            PopulateDropDownLists(student.FacultyId);
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            PopulateDropDownLists(student.FacultyId);
            return View(student);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await GetStudentWithFacultyByLinqAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await GetStudentWithFacultyByLinqAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}