using Microsoft.AspNetCore.Mvc;
using WebBanHang.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(decimal? minPrice, decimal? maxPrice, string searchTerm)
        {
            var categories = _context.Categories.ToList();

            var products = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.Name.Contains(searchTerm));
            }

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

            var productList = (minPrice == null && maxPrice == null && string.IsNullOrEmpty(searchTerm))
                ? products.Take(8).ToList()
                : products.ToList();

            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.SearchTerm = searchTerm;
            ViewBag.Categories = categories;
            ViewBag.Products = productList;

            return View();
        }


        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Reviews) // Tải các review liên quan
                    .ThenInclude(r => r.User) // Tải thông tin người dùng đã viết review
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Tính rating trung bình và gửi ra view
            if (product.Reviews.Any())
            {
                ViewBag.AverageRating = product.Reviews.Average(r => r.Rating);
                ViewBag.ReviewCount = product.Reviews.Count();
            }
            else
            {
                ViewBag.AverageRating = 0;
                ViewBag.ReviewCount = 0;
            }

            return View(product);
        }
        public IActionResult ProductsByCategory(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null) return NotFound();

            var products = _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .ToList();

            ViewBag.CategoryName = category.Name;
            return View(products);
        }

        

        [HttpPost]
        public async Task<IActionResult> AddReview(int ProductId, int Rating, string Comment)
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để đánh giá." });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return Json(new { success = false, message = "Lỗi xác thực người dùng." });
            }

            // Tạo một đối tượng Review mới một cách thủ công từ các tham số
            var review = new Review
            {
                ProductId = ProductId,
                Rating = Rating,
                Comment = Comment,
                UserId = user.Id,
                ReviewDate = DateTime.Now
            };

            // Kiểm tra tính hợp lệ của đối tượng vừa tạo
            if (ModelState.IsValid)
            {
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                // Tạo ViewModel để gửi về client
                var result = new ReviewViewModel
                {
                    Username = user.Username,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    ReviewDate = review.ReviewDate.ToString("dd/MM/yyyy")
                };

                return Json(new { success = true, review = result });
            }

            // Nếu vẫn có lỗi, trả về thông báo
            return Json(new { success = false, message = "Dữ liệu gửi lên không hợp lệ. Vui lòng thử lại." });
        }

    }
}
