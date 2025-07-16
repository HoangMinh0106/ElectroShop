namespace WebBanHang.Models
{
    // Model này dùng để gửi dữ liệu review về client một cách an toàn (tránh lỗi lặp vô tận khi chuyển sang JSON)
    public class ReviewViewModel
    {
        public string Username { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string ReviewDate { get; set; }
    }
}