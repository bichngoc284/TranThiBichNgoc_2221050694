using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Đơn hàng không được để trống")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Sản phẩm không được để trống")]
        public int ProductId { get; set; }

        [Range(1, 1000, ErrorMessage = "Số lượng phải từ 1 đến 1000")]
        public int Quantity { get; set; }

        [Range(0.01, 100000000, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Price { get; set; }

        // 👇 THÊM (để lưu thông tin khi mua)
        [StringLength(150, ErrorMessage = "Tên sản phẩm tối đa 150 ký tự")]
        public string? ProductName { get; set; }

        [StringLength(50, ErrorMessage = "Mã sản phẩm tối đa 50 ký tự")]
        public string? ProductCode { get; set; }

        // 👇 Thành tiền (không lưu DB)
        [NotMapped]
        public decimal Total => Quantity * Price;

        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}