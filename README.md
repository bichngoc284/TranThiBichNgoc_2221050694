Buổi học số 3: 
2. Cấu trúc thư mục của dự án .Net MVC:

Dự án .NET MVC = một website
Gõ link → web hiện ra
Có giao diện
Có xử lý phía server

MVC: Model - Views - Controller
Tạo project MVC: dotnet new mvc

📄 DemoController.cs là gì?
Ví dụ:

public class DemoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

👉 Nghĩa là:
Có 1 controller tên Demo
Có 1 hành động (action) tên Index

Buổi học số 4:

1. Tìm hiểu về ViewBag trong MVC

ViewBag giúp Controller “nói chuyện” với View

📦 Bạn bỏ dữ liệu vào ViewBag ở Controller
👀 View mở túi ViewBag ra để lấy dữ liệu hiển thị

Trong Controller (bỏ dữ liệu vào túi)
//public IActionResult Index()
{
    ViewBag.Message = "Xin chào các bạn!";
    ViewBag.Year = 2026;
    return View();
}//

Trong View (lấy dữ liệu ra dùng)
//<h1>@ViewBag.Message</h1>
<p>Năm hiện tại: @ViewBag.Year</p>//

Lưu ý: 
- Gõ đúng tên + đúng chính tả: vd ViewBag.Age = 22 (csharp)   -> <p> Tuổi: @ViewBag.Age<p> 


2. Ví dụ sử dụng ViewBag để gửi dự liệu từ Controller về View

Ví dụ: ViewBagController.cs
3. Tìm hiểu về gửi nhận dữ liệu giữa View và Controller thông qua Submit form

FORM LÀ GÌ? 

👉 Form là một cái “phiếu” để người dùng nhập dữ liệu rồi gửi đi

Ví dụ ngoài đời:

Bạn điền tên + tuổi vào tờ giấy

Đưa cho nhân viên xử lý

👉 Trong MVC:

View = tờ giấy (form)

Controller = nhân viên xử lý

4.Lấy ví dụ: nhập họ tên trên view gửi dữ liệu lên controller, controller xử lý và gửi thông báo "Xin chào " + Họ tên về hiển thị lên view

 HelloController.cs


5. Tìm hiểu về Models, tạo class Student có 2 thuộc tính: StudentCode, FullName

1️⃣ Model là gì? (hiểu đơn giản)

👉 Model là nơi chứa dữ liệu và định nghĩa cấu trúc dữ liệu
Nó KHÔNG hiển thị, KHÔNG xử lý giao diện, chỉ giữ thông tin

Ví dụ ngoài đời:

Sinh viên → có Mã SV, Họ tên
Trong code → ta tạo Model Student

2️⃣ Tạo thư mục Models

Trong project MVC của bạn (DemoMVC):

📁 DemoMVC

📁 Controllers

📁 Views

📁 Models 👈 (nếu chưa có thì tạo mới)

👉 Chuột phải project → Add → New Folder → đặt tên: Models

3️⃣ Tạo class Student

Trong thư mục Models:

👉 Chuột phải Models → Add → Class
👉 Đặt tên: Student.cs
