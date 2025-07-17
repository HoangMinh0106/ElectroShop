using Microsoft.AspNetCore.Mvc;
using WebBanHang.Models;
using WebBanHang.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks; 

namespace WebBanHang.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Gán vai trò mặc định là User
                user.Role = "User";

                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Lưu thông tin đăng nhập vào Session
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);
                // Chuyển hướng dựa trên vai trò
                if (user.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    return RedirectToAction("Index", "Admin");
                else
                    return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu.";
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Profile()
        {
            // Lấy username từ session
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                // Nếu chưa đăng nhập, chuyển hướng về trang đăng nhập
                return RedirectToAction("Login");
            }

            // Tìm người dùng trong database
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                // Nếu không tìm thấy user, có thể session đã cũ, xóa session và yêu cầu đăng nhập lại
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }


            return View(user);
        }


        [HttpPost]
        public IActionResult Profile(User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedUser);
            }

            // Tìm người dùng hiện tại trong database
            var existingUser = _context.Users.Find(updatedUser.Id);
            if (existingUser != null)
            {
                // Cập nhật các thông tin cho phép thay đổi
                existingUser.Gender = updatedUser.Gender;
                existingUser.PhoneNumber = updatedUser.PhoneNumber;
                existingUser.Address = updatedUser.Address;

                _context.Users.Update(existingUser);
                _context.SaveChanges();

                TempData["Message"] = "Cập nhật hồ sơ thành công!";
            }

            return RedirectToAction("Profile");
        }

        public IActionResult MyOrders()
        {
            // Lấy username từ session
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login");
            }

            // Tìm user và lấy kèm tất cả các đơn hàng của họ
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound();
            }

            // Lọc ra các đơn hàng có UserId khớp với Id của người dùng
            var userOrders = _context.Orders
                                     .Where(order => order.UserId == user.Id)
                                     .OrderByDescending(o => o.OrderDate)
                                     .ToList();

            return View(userOrders);
        }

        public async Task<IActionResult> MyVouchers()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return NotFound();
            }

            // Lấy tất cả voucher có UserId khớp với Id của người dùng, sắp xếp theo ngày hết hạn
            var userVouchers = await _context.Vouchers
                                             .Where(v => v.UserId == user.Id && v.IsTemplate == false) // Chỉ lấy voucher thật, không lấy template
                                             .OrderByDescending(v => v.ExpiryDate)
                                             .ToListAsync();

            return View(userVouchers);
        }

        
    }
}