using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models
{
    public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; } = new();

        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string? Address { get; set; }

        public decimal TotalAmount => CartItems?.Sum(i => i.Price.GetValueOrDefault() * i.Quantity) ?? 0;
         
        public string PaymentMethod { get; set; } // "COD" hoặc "VNPAY"
    }
}
