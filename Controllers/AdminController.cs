using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Data;
using WebBanHang.Models;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;

namespace WebBanHang.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // === TRANG CHÍNH ADMIN ===
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");

            return View("~/Views/Admin/Index.cshtml");
        }

        // === QUẢN LÝ NGƯỜI DÙNG ===
        public IActionResult AdminUser()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");

            var users = _context.Users.ToList();
            return View("~/Views/Admin/AdminUser.cshtml", users);
        }

        public IActionResult DeleteUser(int id)
        {
             if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");

            var user = _context.Users.Find(id);
            if (user != null)
            {
                if (user.Role == "Admin")
                {
                    TempData["Error"] = "Không thể xóa tài khoản Admin.";
                    return RedirectToAction("AdminUser");
                }
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction("AdminUser");
        }

        // === QUẢN LÝ DANH MỤC ===
        public IActionResult Categories()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");

            var categories = _context.Categories.ToList();
            return View("~/Views/Admin/Categories.cshtml", categories);
        }

        public IActionResult CreateCategory()
        {
             if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");
            return View("~/Views/Admin/CreateCategory.cshtml");
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View("~/Views/Admin/CreateCategory.cshtml", category);
        }

        public IActionResult EditCategory(int id)
        {
             if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();
            return View("~/Views/Admin/EditCategory.cshtml", category);
        }

        [HttpPost]
        public IActionResult EditCategoryPost(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View("~/Views/Admin/EditCategory.cshtml", category);
        }

        public IActionResult DeleteCategory(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");

            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();
            
            var productsInCategory = _context.Products.Any(p => p.CategoryId == id);
            if (productsInCategory)
            {
                TempData["Error"] = "Không thể xóa danh mục này vì vẫn còn sản phẩm tồn tại trong đó.";
                return RedirectToAction("Categories");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Categories");
        }

        // === QUẢN LÝ SẢN PHẨM ===
        public IActionResult Products()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");
            var products = _context.Products.Include(p => p.Category).ToList();
            return View("~/Views/Admin/Products.cshtml", products);
        }

        public IActionResult CreateProduct()
        {
             if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");
            ViewBag.Categories = _context.Categories.ToList();
            return View("~/Views/Admin/CreateProduct.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                 if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Path.GetRandomFileName() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/Image", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    product.ImageUrl = "/Image/" + fileName;
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Products");
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View("~/Views/Admin/CreateProduct.cshtml", product);
        }

        public IActionResult EditProduct(int id)
        {
             if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            ViewBag.Categories = _context.Categories.ToList();
            return View("~/Views/Admin/EditProduct.cshtml", product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                var existing = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == product.Id);
                if (existing == null) return NotFound();

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Path.GetRandomFileName() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/Image", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    product.ImageUrl = "/Image/" + fileName;
                }
                else
                {
                    product.ImageUrl = existing.ImageUrl;
                }
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Products");
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View("~/Views/Admin/EditProduct.cshtml", product);
        }

        public IActionResult DeleteProduct(int id)
        {
             if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Products");
        }

        // === THỐNG KÊ DOANH THU ===
        public IActionResult Revenue()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");
                
            var orders = _context.Orders
                .Where(o => o.PaymentStatus == "Paid" || o.PaymentStatus == "COD")
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            ViewBag.TotalRevenue = orders.Sum(o => o.TotalAmount);
            ViewBag.RevenueByDay = orders.GroupBy(o => o.OrderDate.Date).Select(g => new { Date = g.Key, Total = g.Sum(x => x.TotalAmount) }).OrderBy(g => g.Date).ToList();
            ViewBag.RevenueByMonth = orders.GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month }).Select(g => new { Month = new DateTime(g.Key.Year, g.Key.Month, 1), Total = g.Sum(x => x.TotalAmount) }).OrderBy(g => g.Month).ToList();
            ViewBag.RevenueByYear = orders.GroupBy(o => o.OrderDate.Year).Select(g => new { Year = g.Key, Total = g.Sum(x => x.TotalAmount) }).OrderBy(g => g.Year).ToList();
            
            return View("~/Views/Admin/Revenue.cshtml", orders);
        }

        // === QUẢN LÝ ĐƠN HÀNG ===
public IActionResult ManageOrders()
{
    if (HttpContext.Session.GetString("Role") != "Admin")
        return RedirectToAction("AccessDenied", "Account");
        
    var ordersWithUsers = _context.Orders
        .Join(
            _context.Users,         // Bảng để join
            order => order.UserId,  // Khóa từ bảng Orders
            user => user.Id,        // Khóa từ bảng Users
            (order, user) => new OrderViewModel // Tạo đối tượng mới từ kết quả
            {
                OrderId = order.Id,
                Username = user.Username, // Lấy username từ đối tượng user đã join
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus
            })
        .OrderByDescending(o => o.OrderDate)
        .ToList();

    return View("~/Views/Admin/ManageOrders.cshtml", ordersWithUsers);
}

        // === QUẢN LÝ VOUCHER ===
        public IActionResult Vouchers()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");

            var vouchers = _context.Vouchers.ToList();
            return View("~/Views/Admin/Vouchers.cshtml", vouchers);
        }

        public IActionResult CreateVoucher()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");
                
            return View("~/Views/Admin/CreateVoucher.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVoucher(Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                _context.Vouchers.Add(voucher);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Tạo voucher thành công!";
                return RedirectToAction(nameof(Vouchers));
            }
            return View("~/Views/Admin/CreateVoucher.cshtml", voucher);
        }

        public async Task<IActionResult> EditVoucher(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");

            if (id == null) return NotFound();
            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null) return NotFound();
            return View("~/Views/Admin/EditVoucher.cshtml", voucher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVoucher(int id, Voucher voucher)
        {
            if (id != voucher.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voucher);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Cập nhật voucher thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Vouchers.Any(e => e.Id == voucher.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Vouchers));
            }
            return View("~/Views/Admin/EditVoucher.cshtml", voucher);
        }
        
        public async Task<IActionResult> DeleteVoucher(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Account");

            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher != null)
            {
                _context.Vouchers.Remove(voucher);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Xóa voucher thành công!";
            }
            
            return RedirectToAction(nameof(Vouchers));
        }
    }
}