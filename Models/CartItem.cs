namespace WebBanHang.Models
{
    public class CartItem
    {
        public int? ProductId { get; set; }           // khớp với Product.Id
        public string? ProductName { get; set; }       // khớp với Product.Name
        public string? ImageUrl { get; set; }          // khớp với Product.ImageUrl
        public decimal? Price { get; set; }            // khớp với Product.Price
        public int Quantity { get; set; }              // không nullable vì Quantity luôn cần có

        
    }
}
