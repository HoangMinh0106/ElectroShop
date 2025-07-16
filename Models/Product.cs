using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHang.Models
{
    public class Product
    {
        public int? Id { get; set; }

        [Required]
        public string? Name { get; set; }


        public decimal? Price { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        [Display(Name = "Danh mục")]
        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
