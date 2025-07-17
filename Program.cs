using Microsoft.EntityFrameworkCore;
using WebBanHang.Data;
using WebBanHang.Helpers;
using WebBanHang.Services;
using WebBanHang.Models;
using System.Linq;
using System.Security.Cryptography; // Thêm using để mã hóa
using System.Text;                // Thêm using để mã hóa

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ cho Session và HttpContextAccessor (chỉ cần 1 lần)
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddScoped<VnPayService>();
builder.Services.AddScoped<CartService>(); 

// Thêm dịch vụ cho Controller và View (MVC) và Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Thêm dịch vụ cho Razor Pages

// Thêm DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//dịch vụ cho AutoVoucherService
builder.Services.AddHostedService<AutoVoucherService>();



var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseSession();

app.UseAuthorization();


app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    // Đảm bảo database đã được tạo
    dbContext.Database.EnsureCreated();

    // Kiểm tra nếu chưa có tài khoản admin thì tạo mới
    if (!dbContext.Users.Any(u => u.Role == "Admin"))
    {
        // Hàm mã hóa mật khẩu mini
        string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        
        
        dbContext.Users.Add(new User
        {
            Username = "admin",
            Password = HashPassword("123456"), // Mật khẩu được mã hóa
            Role = "Admin",
            Gender = "Admin",
            PhoneNumber = "0123456789",
            Address = "Trụ sở chính"
        });

        dbContext.SaveChanges();
    }
}

app.Run();