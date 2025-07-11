using Microsoft.AspNetCore.Mvc;
using WebBanHang.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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


        public IActionResult Details(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
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
    }
}
