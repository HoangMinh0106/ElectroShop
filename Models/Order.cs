namespace WebBanHang.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public int User { get; set; } 
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string? TransactionId { get; set; }
        public decimal TotalAmount { get; set; }


        public decimal Discount { get; set; }      // Lưu số tiền được giảm giá
        public string? VoucherCode { get; set; }   // Lưu mã voucher đã áp dụng
    }
}