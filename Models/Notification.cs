using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHang.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Url { get; set; } // Đường link khi nhấn vào thông báo

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}