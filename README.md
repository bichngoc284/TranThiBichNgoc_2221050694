1. tìm hiểu về cấu trúc dự án .Net MVC 
*Mô hình kiến trúc phần mêm tách biệt:
- Controlles : chứa các Controller xử lý request từ trình duyệt. Nhận request 
-view : giap diện người dùng .
-Model : Xử lý dữ liệu và logic nghiệp vụ .
* Cấu trúc thư mục chính :
MyAspNetMvcApp/
│
├── Controllers/
├── Models/
├── Views/
│ ├── Shared/
│ └── Home/
│
├── wwwroot/
│ ├── css/
│ ├── js/
│ └── images/
│
├── Properties/
├── appsettings.json
├── Program.cs
├── Startup.cs (với .NET Core < 6)
└── MyAspNetMvcApp.csproj
Controllers/

Chứa các Controller.

Controller nhận request từ người dùng, xử lý logic và trả về View hoặc dữ liệu.

Ví dụ: HomeController.cs, AccountController.cs.

📂 Models/

Chứa các Model đại diện cho dữ liệu và nghiệp vụ.

Thường là các class ánh xạ với database (Entity) hoặc ViewModel.

Ví dụ: User.cs, Product.cs.

📂 Views/

Chứa các file giao diện .cshtml (Razor View).

Mỗi Controller thường có một thư mục View tương ứng.

📂 Views/Home/

Chứa các View của HomeController.

Ví dụ: Index.cshtml, About.cshtml.

📂 Views/Shared/

Chứa các View dùng chung cho toàn bộ ứng dụng.

Ví dụ:

_Layout.cshtml (layout chính)

_PartialView.cshtml

_ValidationScriptsPartial.cshtml

📂 wwwroot/

Chứa các tài nguyên tĩnh (static files).

📂 wwwroot/css/

File CSS (Bootstrap, site.css, ...)

📂 wwwroot/js/

File JavaScript

📂 wwwroot/images/

Hình ảnh của website

📂 Properties/

Chứa cấu hình cho project.

Thường bao gồm file launchSettings.json (cấu hình môi trường chạy).
