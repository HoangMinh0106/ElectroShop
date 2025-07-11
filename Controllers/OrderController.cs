using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using WebBanHang.Data;
using WebBanHang.Models;
using WebBanHang.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Helpers; // Cần thiết cho Session

namespace WebBanHang.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly VnPayService _vnPayService;
        private readonly IConfiguration _config;
        
        // Khai báo hằng số cho session keys để tránh lỗi chính tả
        private const string CartSession = "CartSession";
        private const string VoucherSession = "VoucherCode";

        public OrderController(ApplicationDbContext context, IConfiguration config, VnPayService vnPayService)
        {
            _context = context;
            _config = config;
            _vnPayService = vnPayService;
        }

        // GET: /Order/Checkout ➜ hiển thị form thanh toán
        public IActionResult Checkout()
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSession) ?? new List<CartItem>();
            if (!cartItems.Any())
            {
                TempData["Message"] = "Giỏ hàng trống!";
                return RedirectToAction("Index", "Cart");
            }
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                TempData["Message"] = "Không tìm thấy người dùng!";
                return RedirectToAction("Login", "Account");
            }
            var model = new CheckoutViewModel
            {
                CustomerName = user.Username,
                Phone = user.PhoneNumber,
                Address = user.Address
            };
            return View(model);
        }

        // POST: /Order/Checkout ➜ xử lý đặt hàng
        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSession) ?? new List<CartItem>();
            if (!cartItems.Any())
            {
                TempData["Message"] = "Giỏ hàng trống!";
                return RedirectToAction("Index", "Cart");
            }

            var username = HttpContext.Session.GetString("Username");
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // ======================== PHẦN ĐƯỢC THÊM VÀO ========================
            // Tính toán lại tổng tiền và áp dụng voucher
            var subTotal = cartItems.Sum(item => (item.Price ?? 0) * item.Quantity);
            decimal discountAmount = 0;
            string? appliedVoucherCode = HttpContext.Session.GetString(VoucherSession);

            if (!string.IsNullOrEmpty(appliedVoucherCode))
            {
                var voucher = _context.Vouchers.FirstOrDefault(v => v.Code == appliedVoucherCode && v.IsActive && v.ExpiryDate > DateTime.Now);
                if (voucher != null && subTotal >= voucher.MinAmount)
                {
                    discountAmount = voucher.DiscountType == DiscountType.FixedAmount
                        ? voucher.DiscountValue
                        : subTotal * (voucher.DiscountValue / 100);
                }
            }
            // ====================== KẾT THÚC PHẦN THÊM VÀO ======================

            // Tạo đơn hàng mới với thông tin đã được cập nhật
            var order = new Order
            {
                UserId = user.Id,
                CustomerName = model.CustomerName,
                Phone = model.Phone,
                Address = model.Address,
                OrderDate = DateTime.Now,
                PaymentMethod = model.PaymentMethod,
                PaymentStatus = "Pending",
                Discount = discountAmount, // Thuộc tính mới
                VoucherCode = appliedVoucherCode, // Thuộc tính mới
                TotalAmount = subTotal - discountAmount, // Dùng tổng tiền cuối cùng
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId ?? 0,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price ?? 0
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            // Phân nhánh logic dựa trên phương thức thanh toán
            if (model.PaymentMethod == "VNPAY")
            {
                var vnPayModel = new VnPayRequestModel
                {
                    Amount = (double)order.TotalAmount, // Gửi đi số tiền đã giảm
                    CreatedDate = order.OrderDate,
                    Description = $"{order.CustomerName} thanh toan don hang {order.Id}",
                    FullName = order.CustomerName,
                    OrderId = order.Id
                };
                return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
            }
            else
            {
                order.PaymentStatus = "COD";
                _context.SaveChanges();

                HttpContext.Session.Remove(CartSession);
                HttpContext.Session.Remove(VoucherSession); // Xóa voucher sau khi đặt hàng
                TempData["Message"] = "Đặt hàng thành công!";
                return RedirectToAction("OrderConfirmation", new { id = order.Id });
            }
        }

        // Action xử lý callback từ VNPAY
        public IActionResult PaymentCallBack()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                var errorMessage = response == null ? "Lỗi xác thực chữ ký VNPAY." : $"Thanh toán thất bại. Mã lỗi VNPAY: {response.VnPayResponseCode}";
                TempData["Message"] = errorMessage;

                if (response != null && !string.IsNullOrEmpty(response.OrderId))
                {
                    var orderIdFailed = Convert.ToInt32(response.OrderId);
                    var orderFailed = _context.Orders.FirstOrDefault(o => o.Id == orderIdFailed);
                    if (orderFailed != null)
                    {
                        _context.Orders.Remove(orderFailed);
                        _context.SaveChanges();
                    }
                }
                return RedirectToAction("PaymentFail");
            }

            var orderId = Convert.ToInt32(response.OrderId);
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);

            if (order != null)
            {
                if (order.PaymentStatus == "Paid")
                {
                    TempData["Message"] = $"Đơn hàng #{order.Id} đã được thanh toán thành công trước đó.";
                    return RedirectToAction("OrderConfirmation", new { id = order.Id });
                }

                order.PaymentStatus = "Paid";
                order.TransactionId = response.TransactionId;
                _context.SaveChanges();

                HttpContext.Session.Remove(CartSession);
                HttpContext.Session.Remove(VoucherSession); // Xóa voucher sau khi đặt hàng
                TempData["Message"] = $"Thanh toán thành công cho đơn hàng #{order.Id}!";
                return RedirectToAction("OrderConfirmation", new { id = order.Id });
            }

            TempData["Message"] = "Không tìm thấy đơn hàng tương ứng!";
            return RedirectToAction("PaymentFail");
        }
        
        // Action hiển thị trang xác nhận
        public IActionResult OrderConfirmation(int id)
        {
            var order = _context.Orders.Include(o => o.OrderItems)
                                       .ThenInclude(oi => oi.Product)
                                       .FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            return View(order);
        }

        // Action hiển thị trang thất bại
        public IActionResult PaymentFail()
        {
            return View();
        }
    }
}