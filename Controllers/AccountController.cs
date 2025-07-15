using Microsoft.AspNetCore.Mvc;
using WebBanHang.Models;
using WebBanHang.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;

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

                // Điều hướng dựa vào vai trò
                if (user.Role == "Admin")
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
        // GET: /Account/Profile
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

            // Trả về View với thông tin của người dùng
            return View(user);
        }

        // POST: /Account/Profile
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
        // GET: /Account/MyOrders
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
    }
}