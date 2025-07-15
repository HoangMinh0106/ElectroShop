using System.Collections.Generic; // Đảm bảo bạn đã có using này
using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; } = null!;

        [Required]
        public string? Password { get; set; } = null!;

        [Required]
        public string? Gender { get; set; } = null!;

        [Required]
        public string? Address { get; set; } = null!;

        [Required]
        public string? PhoneNumber { get; set; } = null!;

        [Required]
        public string? Role { get; set; } = "User"; 
        
        // Sửa lại dòng này để khởi tạo một danh sách rỗng
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}