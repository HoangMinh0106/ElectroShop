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
        public string? PhoneNumber { get; set; } = null!; // 🔧 thêm dòng này

        [Required]
        public string? Role { get; set; } = "User"; // 🔧 thêm dòng này
    }
}
