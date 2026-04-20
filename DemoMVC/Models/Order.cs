using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ngày đặt hàng không được để trống")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        // FK: một đơn hàng thuộc một khách hàng
        [Required(ErrorMessage = "Khách hàng không được để trống")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        // Một đơn hàng có nhiều chi tiết đơn hàng
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        // 👇 TÊN KHÁCH HÀNG (lấy từ Customer)
        [NotMapped]
        public string? CustomerName => Customer?.Name;

        // 👇 TỔNG SỐ LƯỢNG SẢN PHẨM
        [NotMapped]
        public int TotalQuantity => OrderDetails?.Sum(x => x.Quantity) ?? 0;

        // 👇 TỔNG TIỀN ĐƠN HÀNG
        [NotMapped]
        public decimal TotalAmount => OrderDetails?.Sum(x => x.Quantity * x.Price) ?? 0;
    }
}