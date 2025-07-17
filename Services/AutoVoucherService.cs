using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebBanHang.Data;
using WebBanHang.Models;
using Microsoft.EntityFrameworkCore;

namespace WebBanHang.Services
{
    public class AutoVoucherService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AutoVoucherService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    await AssignVouchersToEligibleUsers(context);
                }

                // Chờ 1 giờ rồi chạy lại
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private async Task AssignVouchersToEligibleUsers(ApplicationDbContext context)
        {
            const int pointThreshold = 500; // Ngưỡng điểm để nhận voucher
            const string templateCode = "REWARD_50K"; // Mã của voucher mẫu

            // Tìm voucher mẫu
            var voucherTemplate = await context.Vouchers.FirstOrDefaultAsync(v => v.IsTemplate && v.Code == templateCode);
            if (voucherTemplate == null)
            {
                return;
            }

            // Tìm tất cả user đủ điểm
            var eligibleUsers = await context.Users.Where(u => u.Points >= pointThreshold).ToListAsync();

            foreach (var user in eligibleUsers)
            {
                // Tạo một voucher mới từ mẫu
                var newVoucher = new Voucher
                {
                    Code = $"REWARD_{Guid.NewGuid().ToString("N").ToUpper().Substring(0, 8)}",
                    DiscountType = voucherTemplate.DiscountType,
                    DiscountValue = voucherTemplate.DiscountValue,
                    MinAmount = voucherTemplate.MinAmount,
                    ExpiryDate = DateTime.Now.AddDays(30), // Hạn dùng 30 ngày
                    IsActive = true,
                    IsTemplate = false,
                    UserId = user.Id
                };

                context.Vouchers.Add(newVoucher);
                var notification = new Notification
                {
                    UserId = user.Id,
                    Message = $"Chúc mừng! Bạn nhận được voucher thưởng do tích đủ {pointThreshold} điểm.",
                    Url = "/Account/MyVouchers"
                };
                context.Notifications.Add(notification);

                // Trừ điểm của user
                user.Points -= pointThreshold;
                context.Users.Update(user);
            }

            if (eligibleUsers.Any())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}