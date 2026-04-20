# 🚀 Quick Start - Xem Đơn Hàng Khách Hàng

## Các URLs Chính

| Chức Năng | URL | Mô Tả |
|-----------|-----|-------|
| Danh sách khách hàng | `/Customer/Index` | Xem tất cả khách hàng |
| Chi tiết khách hàng | `/Customer/Details/1` | Thông tin + đơn hàng gần đây |
| Lịch sử đơn hàng | `/Customer/OrderHistory/1` | Toàn bộ lịch sử đơn hàng |
| Danh sách đơn | `/Order/Index` | Xem tất cả đơn hàng |
| Chi tiết đơn hàng | `/Order/Details/1` | Thông tin chi tiết + sản phẩm |

## 🎯 Các Bước Xem Đơn Hàng

### Cách 1: Qua Trang Chi Tiết Khách Hàng
```
1. Vào /Customer/Index
2. Click "Chi tiết" trên khách hàng
3. Xem danh sách đơn hàng
4. Click "Xem chi tiết lịch sử →" để xem đầy đủ
```

### Cách 2: Qua Trang Lịch Sử Đơn Hàng (Đầy Đủ Nhất)
```
1. Vào /Customer/Index
2. Click "Chi tiết" 
3. Click "Xem chi tiết lịch sử →"
4. Xem toàn bộ lịch sử đơn hàng chi tiết
```

### Cách 3: Qua Danh Sách Đơn Hàng
```
1. Vào /Order/Index
2. Click "Chi tiết" trên đơn hàng
3. Click tên khách hàng để xem thông tin
4. Click "Xem chi tiết lịch sử →" từ Customer Details
```

## 📊 Dữ Liệu Hiển Thị

### Trang Customer Details
- ✅ Tên khách hàng
- ✅ Email
- ✅ Số điện thoại
- ✅ Danh sách đơn hàng gần đây
  - Mã đơn hàng
  - Ngày đặt
  - Số lượng & Tổng tiền
  - Chi tiết sản phẩm

### Trang OrderHistory
- ✅ Thông tin khách hàng
- ✅ Tổng số đơn hàng
- ✅ Danh sách GẦY ĐỦ toàn bộ đơn (sắp xếp mới nhất)
  - Mã đơn hàng
  - Ngày giờ đặt hàng
  - Tất cả sản phẩm + chi tiết
  - Mã sản phẩm (nếu có)
  - Tổng cộng mỗi đơn

## 💡 Tips & Tricks

1. **Từ bất kỳ trang Order nào**, click vào **tên khách hàng** sẽ quay về Customer Details
2. **Trang OrderHistory** là nơi tốt nhất để xem **toàn bộ lịch sử**
3. **Badge số đơn hàng** hiển thị trên danh sách khách hàng
4. Tất cả **ngày tháng** đều là **dd/MM/yyyy HH:mm**
5. Tất cả **tiền tệ** đều là **VND**

## 🔄 Navigation

```
Trang Chủ
    ↓
Khách Hàng (Danh sách)
    ↓
Chi Tiết Khách Hàng ←→ Lịch Sử Đơn Hàng
    ↓
Chi Tiết Đơn Hàng
```

## ⚙️ Chức Năng CRUD

### Quản Lý Khách Hàng
- Thêm mới: `/Customer/Create`
- Xem chi tiết: `/Customer/Details/{id}`
- Chỉnh sửa: `/Customer/Edit/{id}`
- Xóa: `/Customer/Delete/{id}`

### Quản Lý Đơn Hàng
- Thêm mới: `/Order/Create`
- Xem danh sách: `/Order/Index`
- Xem chi tiết: `/Order/Details/{id}`
- Chỉnh sửa: `/Order/Edit/{id}`
- Xóa: `/Order/Delete/{id}`

## 📝 Ví Dụ Cụ Thể

### Xem Đơn Hàng Của Khách Hàng ID=5
```
URL: /Customer/Details/5
Hoặc
URL: /Customer/OrderHistory/5
```

### Xem Chi Tiết Đơn Hàng ID=123
```
URL: /Order/Details/123
```

### Xem Tất Cả Khách Hàng
```
URL: /Customer/Index
```

## 🎨 Styling

- Sử dụng **Bootstrap 5**
- Tables có **striped rows**
- Buttons có **color coding**: 
  - 🟦 Primary (blue) - Hành động chính
  - 🟨 Warning (yellow) - Chỉnh sửa
  - 🟩 Info (light blue) - Xem chi tiết
  - 🟥 Danger (red) - Xóa
  - ⬜ Secondary (gray) - Quay lại

## ❓ FAQs

**Q: Làm sao để xem tất cả đơn hàng của một khách hàng?**
A: Vào `/Customer/OrderHistory/{customerId}`

**Q: Đơn hàng được sắp xếp theo thứ tự gì?**
A: Mới nhất lên đầu (OrderByDescending by OrderDate)

**Q: Có thể xem thông tin sản phẩm từ đâu?**
A: Trong trang OrderHistory, mỗi đơn hàng có bảng chi tiết sản phẩm

**Q: Tổng tiền hiển thị như thế nào?**
A: Tính từ Quantity × Price, được format với dấu ',' (N0) và từ "VND"

---

**📌 Tài liệu đầy đủ**: Xem `HUONG_DAN_XEM_DON_HANG.md`
