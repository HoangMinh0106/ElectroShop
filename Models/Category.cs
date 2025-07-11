using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models
{
    public class Category
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Tên danh mục")]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
