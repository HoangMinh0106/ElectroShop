using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Data;

namespace WebBanHang.ViewComponents
{
    public class NotificationViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationViewComponent(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var username = _httpContextAccessor.HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return Content(string.Empty); // Không hiển thị gì nếu chưa đăng nhập
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return Content(string.Empty);
            }

            var notifications = await _context.Notifications
                                              .Where(n => n.UserId == user.Id)
                                              .OrderByDescending(n => n.CreatedAt)
                                              .Take(5) // Lấy 5 thông báo gần nhất
                                              .ToListAsync();

            ViewBag.UnreadCount = await _context.Notifications.CountAsync(n => n.UserId == user.Id && !n.IsRead);

            return View(notifications);
        }
    }
}