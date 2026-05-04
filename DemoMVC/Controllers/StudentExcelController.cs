using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models;
using OfficeOpenXml;

namespace DemoMVC.Controllers
{
    public class StudentExcelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentExcelController(ApplicationDbContext context)
        {
            _context = context;
            // Đặt License cho EPPlus 8+
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        // GET: StudentExcel/Upload
        public IActionResult Upload()
        {
            return View();
        }

        // POST: StudentExcel/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["Error"] = "Vui lòng chọn file Excel để upload.";
                return View();
            }

            // Kiểm tra định dạng file
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (extension != ".xlsx" && extension != ".xls")
            {
                TempData["Error"] = "Chỉ chấp nhận file Excel (.xlsx, .xls).";
                return View();
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        // Danh sách để lưu sinh viên
                        var students = new List<Student>();
                        var errors = new List<string>();

                        // Bắt đầu từ dòng 2 (dòng 1 là header)
                        for (int row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                var studentCode = worksheet.Cells[row, 1].Value?.ToString()?.Trim();
                                var fullName = worksheet.Cells[row, 2].Value?.ToString()?.Trim();
                                var facultyName = worksheet.Cells[row, 3].Value?.ToString()?.Trim();

                                // Kiểm tra dữ liệu trống
                                if (string.IsNullOrEmpty(studentCode) && string.IsNullOrEmpty(fullName))
                                {
                                    continue; // Bỏ qua dòng trống
                                }

                                // Validate required fields
                                if (string.IsNullOrEmpty(studentCode))
                                {
                                    errors.Add($"Dòng {row}: Mã sinh viên không được trống.");
                                    continue;
                                }

                                if (string.IsNullOrEmpty(fullName))
                                {
                                    errors.Add($"Dòng {row}: Họ và tên không được trống.");
                                    continue;
                                }

                                if (string.IsNullOrEmpty(facultyName))
                                {
                                    errors.Add($"Dòng {row}: Tên khoa không được trống.");
                                    continue;
                                }

                                // Tìm FacultyId từ tên khoa
                                var faculty = await _context.Faculties
                                    .FirstOrDefaultAsync(f => f.Name.ToLower() == facultyName.ToLower());

                                if (faculty == null)
                                {
                                    errors.Add($"Dòng {row}: Không tìm thấy khoa có tên '{facultyName}'.");
                                    continue;
                                }

                                // Kiểm tra trùng mã sinh viên
                                var existingStudent = await _context.Students
                                    .AnyAsync(s => s.StudentCode == studentCode);

                                if (existingStudent)
                                {
                                    errors.Add($"Dòng {row}: Mã sinh viên '{studentCode}' đã tồn tại.");
                                    continue;
                                }

                                students.Add(new Student
                                {
                                    StudentCode = studentCode,
                                    FullName = fullName,
                                    FacultyId = faculty.Id
                                });
                            }
                            catch (Exception ex)
                            {
                                errors.Add($"Dòng {row}: Lỗi xử lý - {ex.Message}");
                            }
                        }

                        // Lưu vào CSDL nếu có sinh viên hợp lệ
                        if (students.Any())
                        {
                            _context.Students.AddRange(students);
                            await _context.SaveChangesAsync();
                        }

                        // Truyền kết quả cho View
                        ViewBag.ImportedCount = students.Count;
                        ViewBag.ErrorCount = errors.Count;
                        ViewBag.Errors = errors;
                        ViewBag.Success = errors.Count == 0 ? "Tất cả sinh viên đã được import thành công!" : null;
                    }
                }

                TempData["Success"] = $"Đã import thành công {ViewBag.ImportedCount} sinh viên.";
                return View("UploadResult");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Lỗi khi xử lý file: {ex.Message}";
                return View();
            }
        }

        // GET: StudentExcel/DownloadTemplate
        public IActionResult DownloadTemplate()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SinhVien");

                // Header style
                worksheet.Cells["A1:C1"].Style.Font.Bold = true;
                worksheet.Cells["A1:C1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A1:C1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                // Set headers
                worksheet.Cells[1, 1].Value = "Mã Sinh Viên";
                worksheet.Cells[1, 2].Value = "Họ và Tên";
                worksheet.Cells[1, 3].Value = "Tên Khoa";

                // Ví dụ dữ liệu - lấy tên khoa thực tế
                worksheet.Cells[2, 1].Value = "SV001";
                worksheet.Cells[2, 2].Value = "Nguyễn Văn A";
                worksheet.Cells[2, 3].Value = "Khoa Công Nghệ Thông Tin";

                worksheet.Cells[3, 1].Value = "SV002";
                worksheet.Cells[3, 2].Value = "Trần Thị B";
                worksheet.Cells[3, 3].Value = "Khoa Kinh Tế";

                // Auto fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = $"Mau_Import_SinhVien_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        // GET: StudentExcel/ListFaculties - Danh sách khoa để tham khảo
        public async Task<IActionResult> ListFaculties()
        {
            var faculties = await _context.Faculties.OrderBy(f => f.Id).ToListAsync();
            return View(faculties);
        }
    }
}
