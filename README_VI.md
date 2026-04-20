# 📦 Hệ Thống Quản Lý Đơn Hàng - DemoMVC

**Phiên bản**: 1.0.0  
**Ngôn ngữ**: C# .NET 6+  
**Database**: SQLite  
**Framework**: ASP.NET Core MVC  

---

## 📖 Giới Thiệu

Hệ thống này là một ứng dụng web ASP.NET Core MVC hoàn chỉnh để quản lý **khách hàng**, **sản phẩm**, **đơn hàng** và **chi tiết đơn hàng**. 

### ✨ Tính Năng Chính

- **👥 Quản Lý Khách Hàng**: Thêm, sửa, xóa, xem chi tiết khách hàng
- **📦 Quản Lý Sản Phẩm**: Quản lý sản phẩm với giá và tồn kho
- **🛒 Quản Lý Đơn Hàng**: Tạo, theo dõi đơn hàng
- **📋 Chi Tiết Đơn Hàng**: Xem sản phẩm trong từng đơn hàng
- **🔍 Xem Lịch Sử Đơn Hàng**: Hiển thị tất cả đơn hàng của một khách hàng (NEW!)

---

## 🗂️ Cấu Trúc Thư Mục

```
DemoMVC/
├── Controllers/          # Controllers xử lý logic
│   ├── CustomerController.cs
│   ├── OrderController.cs
│   ├── OrderDetailController.cs
│   ├── ProductController.cs
│   └── ...
├── Models/              # Data models
│   ├── Customer.cs
│   ├── Order.cs
│   ├── OrderDetail.cs
│   ├── Product.cs
│   └── ...
├── Views/               # Razor views (giao diện)
│   ├── Customer/
│   ├── Order/
│   ├── Product/
│   └── ...
├── Data/                # Database context
│   └── ApplicationDbContext.cs
├── wwwroot/             # Static files (CSS, JS, images)
├── Program.cs           # Cấu hình ứng dụng
└── DemoMVC.csproj       # Project file
```

---

## 🚀 Bắt Đầu

### Yêu Cầu
- .NET 6.0 trở lên
- Visual Studio 2022 (hoặc VS Code)
- SQLite

### Cài Đặt

1. **Clone/Open Project**
```bash
cd DemoMVC
```

2. **Restore NuGet packages**
```bash
dotnet restore
```

3. **Build project**
```bash
dotnet build
```

4. **Run migrations (nếu cần)**
```bash
dotnet ef database update
```

5. **Start application**
```bash
dotnet run
```

6. **Open browser**
```
https://localhost:5001
```

---

## 📚 Các URLs Chính

### Quản Lý Khách Hàng
| URL | Mô Tả |
|-----|-------|
| `/Customer/Index` | Danh sách khách hàng |
| `/Customer/Details/{id}` | Chi tiết + đơn hàng gần đây |
| `/Customer/OrderHistory/{id}` | Lịch sử đơn hàng (NEW!) |
| `/Customer/Create` | Thêm khách hàng mới |
| `/Customer/Edit/{id}` | Chỉnh sửa khách hàng |
| `/Customer/Delete/{id}` | Xóa khách hàng |

### Quản Lý Đơn Hàng
| URL | Mô Tả |
|-----|-------|
| `/Order/Index` | Danh sách đơn hàng |
| `/Order/Details/{id}` | Chi tiết đơn hàng |
| `/Order/Create` | Tạo đơn hàng mới |
| `/Order/Edit/{id}` | Chỉnh sửa đơn hàng |
| `/Order/Delete/{id}` | Xóa đơn hàng |

### Quản Lý Sản Phẩm
| URL | Mô Tả |
|-----|-------|
| `/Product/Index` | Danh sách sản phẩm |
| `/Product/Details/{id}` | Chi tiết sản phẩm |
| `/Product/Create` | Thêm sản phẩm mới |
| `/Product/Edit/{id}` | Chỉnh sửa sản phẩm |
| `/Product/Delete/{id}` | Xóa sản phẩm |

### Quản Lý Chi Tiết Đơn Hàng
| URL | Mô Tả |
|-----|-------|
| `/OrderDetail/Index` | Danh sách chi tiết |
| `/OrderDetail/Details/{id}` | Chi tiết |
| `/OrderDetail/Create` | Thêm mới |
| `/OrderDetail/Edit/{id}` | Chỉnh sửa |
| `/OrderDetail/Delete/{id}` | Xóa |

---

## 💾 Models Định Nghĩa

### Customer (Khách Hàng)
```csharp
public class Customer
{
    public int Id { get; set; }
    [Required] [StringLength(100)]
    public string Name { get; set; }
    
    [Required] [EmailAddress] [StringLength(150)]
    public string Email { get; set; }
    
    [Required] [Phone] [StringLength(20)]
    public string Phone { get; set; }
    
    // Navigation
    public List<Order> Orders { get; set; }
}
```

### Order (Đơn Hàng)
```csharp
public class Order
{
    public int Id { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    
    [Required]
    public int CustomerId { get; set; }
    
    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }
    
    // Navigation
    public List<OrderDetail> OrderDetails { get; set; }
    
    // Calculated properties
    [NotMapped]
    public int TotalQuantity => OrderDetails?.Sum(x => x.Quantity) ?? 0;
    [NotMapped]
    public decimal TotalAmount => OrderDetails?.Sum(x => x.Quantity * x.Price) ?? 0;
}
```

### OrderDetail (Chi Tiết Đơn Hàng)
```csharp
public class OrderDetail
{
    public int Id { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int ProductId { get; set; }
    
    [Range(1, 1000)]
    public int Quantity { get; set; }
    [Range(0.01, 100000000)]
    public decimal Price { get; set; }
    
    [StringLength(150)]
    public string ProductName { get; set; }
    [StringLength(50)]
    public string ProductCode { get; set; }
    
    // Foreign keys
    public Order Order { get; set; }
    public Product Product { get; set; }
}
```

### Product (Sản Phẩm)
```csharp
public class Product
{
    public int Id { get; set; }
    [Required] [StringLength(150)]
    public string Name { get; set; }
    
    [Range(0.01, 100000000)]
    public decimal Price { get; set; }
    
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
    
    // Navigation
    public List<OrderDetail> OrderDetails { get; set; }
}
```

---

## 🔗 Mối Quan Hệ Dữ Liệu

```
┌─────────────────────────────────┐
│         Customer (1)            │
│ - Id (PK)                       │
│ - Name                          │
│ - Email                         │
│ - Phone                         │
└───────────────┬─────────────────┘
                │ (1:*)
                │ CustomerId (FK)
                │
┌───────────────▼─────────────────┐
│         Order (*)               │
│ - Id (PK)                       │
│ - OrderDate                     │
│ - CustomerId (FK)               │
└───────────────┬─────────────────┘
                │ (1:*)
                │ OrderId (FK)
                │
┌───────────────▼─────────────────┐
│      OrderDetail (*)            │
│ - Id (PK)                       │
│ - OrderId (FK) ───┐             │
│ - ProductId (FK) ─┼─┐           │
│ - Quantity        │ │           │
│ - Price           │ │           │
└───────────────────┘ │           │
                      │           │
         ┌────────────┘           │
         │                        │
         │    ┌────────────────────┘
         │    │
┌────────▼────▼──────────────────┐
│       Product (*)              │
│ - Id (PK)                      │
│ - Name                         │
│ - Price                        │
│ - Stock                        │
└────────────────────────────────┘
```

---

## 🎯 Luồng Sử Dụng Chính

### 1. Tạo Đơn Hàng
```
1. Đi tới /Order/Create
2. Chọn Khách hàng
3. Nhập Ngày đặt hàng
4. Click Tạo
```

### 2. Thêm Sản Phẩm Vào Đơn
```
1. Đi tới /OrderDetail/Create
2. Chọn Đơn hàng
3. Chọn Sản phẩm
4. Nhập Số lượng & Giá
5. Click Thêm
```

### 3. Xem Lịch Sử Đơn Hàng (NEW!)
```
1. Đi tới /Customer/Index
2. Click "Chi tiết" trên khách hàng
3. Click "Xem chi tiết lịch sử →"
4. Xem tất cả đơn hàng với sản phẩm
```

---

## 🔐 Validation Rules

### Customer
- ✓ Name: Bắt buộc, Max 100 ký tự
- ✓ Email: Bắt buộc, Format hợp lệ, Max 150 ký tự
- ✓ Phone: Bắt buộc, Format hợp lệ, Max 20 ký tự

### Order
- ✓ OrderDate: Bắt buộc, DateTime
- ✓ CustomerId: Bắt buộc, Foreign Key

### OrderDetail
- ✓ OrderId: Bắt buộc, Foreign Key
- ✓ ProductId: Bắt buộc, Foreign Key
- ✓ Quantity: 1-1000
- ✓ Price: 0.01-100,000,000

### Product
- ✓ Name: Bắt buộc, Max 150 ký tự
- ✓ Price: 0.01-100,000,000
- ✓ Stock: >= 0

---

## 🎨 Giao Diện

- **Framework CSS**: Bootstrap 5
- **Responsive**: Mobile, Tablet, Desktop
- **Icons**: Sử dụng text + Bootstrap utilities
- **Colors**: 
  - Primary (Blue): Actions chính
  - Warning (Yellow): Edit
  - Info (Light Blue): Details
  - Danger (Red): Delete

---

## 📊 Database

- **Type**: SQLite
- **Location**: Local file (DB được tạo tự động)
- **Migrations**: Đã có sẵn trong `Migrations/` folder
- **Update DB**: `dotnet ef database update`

---

## 🛠️ Công Nghệ Sử Dụng

| Công Nghệ | Phiên Bản | Mục Đích |
|-----------|----------|---------|
| .NET | 6.0+ | Runtime |
| ASP.NET Core | 6.0+ | Web Framework |
| Entity Framework Core | 6.0+ | ORM |
| SQLite | Latest | Database |
| Bootstrap | 5.x | CSS Framework |
| Razor | 6.0 | View Engine |

---

## 📝 Tệp Tài Liệu

| Tệp | Mô Tả |
|-----|-------|
| `HUONG_DAN_XEM_DON_HANG.md` | Hướng dẫn chi tiết sử dụng |
| `QUICK_START.md` | Hướng dẫn nhanh |
| `CHANGELOG.md` | Danh sách thay đổi |
| `IMPLEMENTATION_COMPLETE.md` | Xác nhận hoàn thành |
| `README.md` | Tệp này |

---

## 🚀 Deployment

### Local Development
```bash
dotnet run
```
→ Truy cập: `https://localhost:5001`

### Production
```bash
dotnet publish -c Release
# Deploy contents of bin/Release/net6.0/publish/
```

---

## 📞 Troubleshooting

### Issue: Database not found
```
Solution: Run dotnet ef database update
```

### Issue: Port already in use
```
Solution: Change port in launchSettings.json or use different port
```

### Issue: Bootstrap not loading
```
Solution: Ensure wwwroot folder is properly configured in Program.cs
```

---

## ✅ Features Checklist

### Data Management
- [x] CRUD cho Customer
- [x] CRUD cho Order
- [x] CRUD cho OrderDetail
- [x] CRUD cho Product

### User Interface
- [x] Responsive Design
- [x] Bootstrap 5 Styling
- [x] Navigation Links
- [x] Form Validation

### Business Logic
- [x] Automatic Total Calculation
- [x] Eager Loading (Optimize Queries)
- [x] Data Validation
- [x] Customer Order History

### Documentation
- [x] User Guide
- [x] Quick Start
- [x] Code Documentation
- [x] README

---

## 🎓 Learning Points

Bạn sẽ học được:
- ASP.NET Core MVC Architecture
- Entity Framework Core
- CRUD Operations
- Database Relationships
- Razor View Engine
- Bootstrap Responsive Design
- Data Validation
- Forms & Input Handling

---

## 📩 Support

Để được hỗ trợ:
1. Kiểm tra các tệp tài liệu trước
2. Xem CHANGELOG để hiểu thay đổi
3. Tham khảo QUICK_START cho hướng dẫn nhanh

---

## 📄 License

Dự án này được tạo cho mục đích giáo dục.

---

**Phiên bản**: 1.0.0  
**Cập nhật**: 20/04/2026  
**Trạng thái**: ✅ Production Ready

---

## 🎉 Kết Luận

Hệ thống này cung cấp một nền tảng hoàn chỉnh và chuyên nghiệp để quản lý khách hàng và đơn hàng. Tất cả các CRUD operations đã được triển khai, validation đã được thêm, và UI đã được tối ưu cho trải nghiệm người dùng tốt nhất.

**Ready to use! 🚀**
