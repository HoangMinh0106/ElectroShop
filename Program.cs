using Microsoft.EntityFrameworkCore;
using WebBanHang.Data;
using WebBanHang.Helpers;
using WebBanHang.Services;
using WebBanHang.Models; // Thêm dòng này để sử dụng model User
using System.Linq; // Đảm bảo có LINQ



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<VnPayService>();
// Thêm dịch vụ VnPayService

// Đăng ký CartService
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<CartService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// 👉 Thêm DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Các dịch vụ khác
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Middleware
app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// ✅ Thêm đoạn này để tạo tài khoản admin nếu chưa có
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Kiểm tra nếu chưa có tài khoản admin thì tạo mới
    if (!db.Users.Any(u => u.Role == "Admin"))
    {
        db.Users.Add(new User
        {
            Username = "admin",
            Password = "123456", // Nên hash nếu dùng thật
            Role = "Admin",
            Gender = "Nam",
            PhoneNumber = "0903050953",
            Address = "Hồ Chí Minh"
        });

        db.SaveChanges();
    }
}

app.Run();
