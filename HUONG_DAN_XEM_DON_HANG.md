# Hướng Dẫn Chức Năng Xem Chi Tiết Đơn Hàng Khách Hàng

## 📋 Tổng Quan

Tính năng này cho phép người dùng xem thông tin chi tiết về tất cả các đơn hàng mà một khách hàng cụ thể đã mua, bao gồm:
- Danh sách tất cả đơn hàng của khách hàng
- Chi tiết từng sản phẩm trong mỗi đơn hàng
- Số lượng, giá, thành tiền
- Tổng cộng cho mỗi đơn

## 🚀 Các Chức Năng Chính

### 1. Danh Sách Khách Hàng (`/Customer/Index`)
- Hiển thị tất cả khách hàng
- Mỗi khách hàng có một badge hiển thị số đơn hàng
- Nút "Chi tiết" để xem thông tin chi tiết

**Cách truy cập:**
```
http://localhost:xxxx/Customer/Index
```

### 2. Chi Tiết Khách Hàng (`/Customer/Details/{id}`)
- **Thông tin khách hàng**: Mã, tên, email, số điện thoại
- **Danh sách đơn hàng**: Bảng hiển thị các đơn hàng gần đây
  - Mã đơn hàng
  - Ngày đặt hàng
  - Số lượng sản phẩm
  - Tổng tiền
  - Chi tiết sản phẩm (lồng nhau)
- **Nút hành động**:
  - "Xem chi tiết lịch sử →": Quay sang OrderHistory
  - "Chỉnh sửa": Chỉnh sửa thông tin khách hàng
  - "Quay lại danh sách": Về danh sách khách hàng

**Cách truy cập:**
```
http://localhost:xxxx/Customer/Details/1
```

### 3. Lịch Sử Đơn Hàng - MyOrders (`/Customer/OrderHistory/{id}`)
**Tính năng mới nhất - Trang chuyên dụng để xem lịch sử đơn hàng**
- **Thông tin khách hàng**: Tên, Email, SĐT, Tổng số đơn
- **Danh sách đơn hàng chi tiết**:
  - Sắp xếp theo ngày mới nhất
  - Mỗi đơn hàng hiển thị trong một card
  - Bảng chi tiết sản phẩm với:
    - Tên sản phẩm & Mã sản phẩm (nếu có)
    - Số lượng (badge)
    - Đơn giá
    - Thành tiền
    - Tổng cộng đơn hàng (highlight)
- **Trở lại**: Nút "Quay lại thông tin khách hàng" ở trên

**Cách truy cập:**
```
http://localhost:xxxx/Customer/OrderHistory/1
```

### 4. Chi Tiết Đơn Hàng (`/Order/Details/{id}`)
- Hiển thị thông tin đơn hàng
- **Liên kết khách hàng**: Click vào tên khách hàng để quay về Customer Details
- Danh sách tất cả sản phẩm trong đơn
- Tổng số lượng và tổng tiền

**Cách truy cập:**
```
http://localhost:xxxx/Order/Details/1
```

## 📊 Mối Quan Hệ Dữ Liệu

```
Customer
├── Orders (1-to-Many)
│   ├── OrderDetail (1-to-Many)
│   │   └── Product (Many-to-One)
│   └── OrderDate
│   └── OrderedItems
```

## 🔧 Công Nghệ & Framework

- **Language**: C# .NET 6+
- **Database**: SQLite
- **ORM**: Entity Framework Core
- **Frontend**: Razor Views + Bootstrap 5
- **Pattern**: MVC (Model-View-Controller)

## 📱 Giao Diện Người Dùng

### Customer Details View
```
┌─────────────────────────────────────────┐
│  Chi tiết khách hàng                   │
├─────────────────────────────────────────┤
│  Thông tin khách hàng                   │
│  - Name: ...                            │
│  - Email: ...                           │
│  - Phone: ...                           │
├─────────────────────────────────────────┤
│  Danh sách đơn hàng (3)  [Chi tiết →]  │
├─────────────────────────────────────────┤
│  #123 | 20/04/2026  | 5 sản phẩm        │
│   └─ Sản phẩm A | 2 | 100K | 200K      │
│   └─ Sản phẩm B | 3 | 150K | 450K      │
├─────────────────────────────────────────┤
│  [Chỉnh sửa] [Quay lại]                │
└─────────────────────────────────────────┘
```

### OrderHistory View
```
┌─────────────────────────────────────────┐
│  Lịch sử đơn hàng                       │
├─────────────────────────────────────────┤
│  Thông tin khách hàng                   │
│  Tổng số đơn hàng: 5                    │
├─────────────────────────────────────────┤
│  Đơn hàng #123 - 20/04/2026             │
│  ┌─────────────────────────────────────┐│
│  │ Sản phẩm | SL | Đơn giá | Thành tiền││
│  │ A        | 2  | 100K    | 200K      ││
│  │ B        | 3  | 150K    | 450K      ││
│  │ Tổng cộng:                    650K  ││
│  └─────────────────────────────────────┘│
│                                         │
│  Đơn hàng #122 - 19/04/2026             │
│  ...                                    │
└─────────────────────────────────────────┘
```

## 💾 Data Validation (Ràng Buộc)

### Customer Model
```csharp
- Name: Required, Max 100 chars
- Email: Required, Valid Email, Max 150 chars
- Phone: Required, Valid Phone, Max 20 chars
```

### Order Model
```csharp
- OrderDate: Required
- CustomerId: Required (Foreign Key)
- TotalQuantity: Calculated property
- TotalAmount: Calculated property
```

### OrderDetail Model
```csharp
- OrderId: Required (Foreign Key)
- ProductId: Required (Foreign Key)
- Quantity: Required, Range(1, 1000)
- Price: Required, Range(0.01, 100,000,000)
- ProductName: Max 150 chars
- ProductCode: Max 50 chars
```

## 🎯 Luồng Sử Dụng Điển Hình

1. **Admin truy cập danh sách khách hàng**
   - URL: `/Customer/Index`
   - Thấy tất cả khách hàng + số đơn hàng

2. **Admin chọn một khách hàng**
   - URL: `/Customer/Details/1`
   - Xem thông tin và danh sách đơn hàng gần đây

3. **Admin muốn xem toàn bộ lịch sử**
   - Click "Xem chi tiết lịch sử →"
   - URL: `/Customer/OrderHistory/1`
   - Xem tất cả đơn hàng với chi tiết

4. **Admin muốn xem chi tiết một đơn**
   - Click "Chi tiết" trên đơn hàng
   - URL: `/Order/Details/1`
   - Xem toàn bộ chi tiết và có thể quay lại Customer

## 📌 Chức Năng CRUD

Ứng dụng hỗ trợ đầy đủ CRUD cho:

### Customer Management
- ✅ **C**reate: Thêm khách hàng mới
- ✅ **R**ead: Xem danh sách, chi tiết
- ✅ **U**pdate: Chỉnh sửa thông tin
- ✅ **D**elete: Xóa khách hàng

### Order Management
- ✅ **C**reate: Tạo đơn hàng mới
- ✅ **R**ead: Xem danh sách, chi tiết theo khách hàng
- ✅ **U**pdate: Chỉnh sửa đơn hàng
- ✅ **D**elete: Xóa đơn hàng

### OrderDetail Management
- ✅ **C**reate: Thêm sản phẩm vào đơn
- ✅ **R**ead: Xem chi tiết sản phẩm
- ✅ **U**pdate: Chỉnh sửa số lượng, giá
- ✅ **D**elete: Xóa sản phẩm khỏi đơn

### Product Management
- ✅ **C**reate: Thêm sản phẩm
- ✅ **R**ead: Xem danh sách, chi tiết
- ✅ **U**pdate: Chỉnh sửa giá, tên
- ✅ **D**elete: Xóa sản phẩm

## 🔗 Navigation Map

```
Customer Index
├── Customer Details/1
│   ├── Order Details/1 (via Chi tiết button)
│   │   └── Back to Customer Details
│   └── OrderHistory/1 (via Chi tiết lịch sử)
│       └── Back to Customer Details
│
Order Index
├── Order Details/1
│   ├── Customer Details (click customer name)
│   │   └── OrderHistory (from there)
│   └── Back to Order List
```

## 📝 Ghi Chú

- All currency values are displayed in **VND (Vietnamese Dong)**
- Dates are formatted in **dd/MM/yyyy HH:mm**
- Tables are responsive with Bootstrap 5
- All models include proper validation attributes

## 🛠️ File Files Được Cập Nhật

1. `Models/OrderDetail.cs` - Enhanced validation
2. `Controllers/CustomerController.cs` - Added OrderHistory action
3. `Controllers/OrderController.cs` - Updated Index with OrderDetails
4. `Views/Customer/Index.cshtml` - Added order count badge
5. `Views/Customer/Details.cshtml` - Show order list with link
6. `Views/Customer/OrderHistory.cshtml` - NEW ORDER HISTORY VIEW
7. `Views/Order/Index.cshtml` - Enhanced with totals
8. `Views/Order/Details.cshtml` - Enhanced with customer link

## 🎓 Kết Luận

Chức năng này cung cấp một cách toàn diện để:
- Xem tất cả đơn hàng của một khách hàng
- Theo dõi lịch sử mua hàng
- Xem chi tiết sản phẩm trong mỗi đơn
- Dễ dàng điều hướng giữa khách hàng và đơn hàng
