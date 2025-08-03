using System.Collections.Generic;
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

        public string Email { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }

        [Required]
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        //điểm tích lũy 
        public int Points { get; set; } = 0; // Mặc định là 0 điểm khi tạo mới
    }
}