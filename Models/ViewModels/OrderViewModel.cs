namespace WebBanHang.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public List<CartViewModel> CartItems { get; set; } = new List<CartViewModel>();

        // THÊM CÁC THUỘC TÍNH CÒN THIẾU VÀO ĐÂY
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
    }
}
