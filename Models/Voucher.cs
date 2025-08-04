using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace WebBanHang.Models
{
    public enum DiscountType
    {
        FixedAmount, // Giảm giá một số tiền cố định
        Percentage   // Giảm giá theo phần trăm
    }

    public class Voucher
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        public DiscountType DiscountType { get; set; } // Loại giảm giá

        public decimal DiscountValue { get; set; } // Giá trị giảm (ví dụ: 50000 hoặc 10 cho 10%)

        public decimal MinAmount { get; set; } // Số tiền tối thiểu để áp dụng

        public DateTime ExpiryDate { get; set; } // Ngày hết hạn

        public bool IsActive { get; set; } // Voucher có đang được kích hoạt không

        // Thêm UserId để gán voucher cho người dùng cụ thể.
        // ? userid có thể null nếu voucher không gán cho người dùng nào
        public int? UserId { get; set; }

        
        [ForeignKey("UserId")]
        public User? User { get; set; }
        
        public bool IsTemplate { get; set; } = false; 
    }
}