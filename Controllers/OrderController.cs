using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Collections.Generic;
using WebBanHang.Data;
using WebBanHang.Models;
using WebBanHang.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Helpers;
using System.Threading.Tasks;

namespace WebBanHang.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly VnPayService _vnPayService;
        private readonly IConfiguration _config;
        
        // Giữ nguyên tên session key của bạn
        private const string CartSession = "CartSession";
        private const string VoucherSession = "VoucherCode";

        public OrderController(ApplicationDbContext context, IConfiguration config, VnPayService vnPayService)
        {
            _context = context;
            _config = config;
            _vnPayService = vnPayService;
        }


        public async Task<IActionResult> Checkout()
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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                TempData["Message"] = "Không tìm thấy người dùng!";
                return RedirectToAction("Login", "Account");
            }
            
            // Bổ sung đoạn code này để lấy ImageUrl từ database
            foreach (var item in cartItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    item.ImageUrl = product.ImageUrl;
                }
            }


            // Tính toán tổng tiền và áp dụng voucher
            var subTotal = cartItems.Sum(item => (item.Price ?? 0) * item.Quantity);
            decimal discountAmount = 0;
            string? appliedVoucherCode = HttpContext.Session.GetString(VoucherSession);

            if (!string.IsNullOrEmpty(appliedVoucherCode))
            {
                var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.Code == appliedVoucherCode && v.IsActive && v.ExpiryDate > DateTime.Now);
                // thêm điều kiện kiểm tra UserId
                if (voucher != null && subTotal >= voucher.MinAmount && (voucher.UserId == null || voucher.UserId == user.Id))
                {
                    discountAmount = voucher.DiscountType == DiscountType.FixedAmount
                        ? voucher.DiscountValue
                        : subTotal * (voucher.DiscountValue / 100);
                }
                else
                {
                    // Nếu voucher không còn hợp lệ, xóa khỏi session
                    HttpContext.Session.Remove(VoucherSession);
                    appliedVoucherCode = null; // Đặt lại mã voucher
                }
            }
            var grandTotal = subTotal - discountAmount;


            var model = new CheckoutViewModel
            {
                CartItems = cartItems,
                CustomerName = user.Username,
                Phone = user.PhoneNumber,
                Address = user.Address,
                
                SubTotal = subTotal,
                DiscountAmount = discountAmount,
                GrandTotal = grandTotal,
                // hiển thị mã voucher đã áp dụng
                AppliedVoucherCode = appliedVoucherCode
            };
            return View(model);
        }

    
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            // Bổ sung đoạn code này để lấy ImageUrl từ database cho model
            var cartItemsForView = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSession) ?? new List<CartItem>();
            foreach (var item in cartItemsForView)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    item.ImageUrl = product.ImageUrl;
                }
            }
            model.CartItems = cartItemsForView;
            
            if (!ModelState.IsValid)
            {
                var subTotalForView = cartItemsForView.Sum(item => (item.Price ?? 0) * item.Quantity);
                model.SubTotal = subTotalForView;
    
                return View(model);
            }

            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSession) ?? new List<CartItem>();
            if (!cartItems.Any())
            {
                TempData["Message"] = "Giỏ hàng trống!";
                return RedirectToAction("Index", "Cart");
            }

            var username = HttpContext.Session.GetString("Username");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var subTotal = cartItems.Sum(item => (item.Price ?? 0) * item.Quantity);
            decimal discountAmount = 0;
            string? appliedVoucherCode = HttpContext.Session.GetString(VoucherSession);

            if (!string.IsNullOrEmpty(appliedVoucherCode))
            {
                var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.Code == appliedVoucherCode && v.IsActive && v.ExpiryDate > DateTime.Now);
                // kiểm tra UserId của voucher
                if (voucher != null && subTotal >= voucher.MinAmount && (voucher.UserId == null || voucher.UserId == user.Id))
                {
                    discountAmount = voucher.DiscountType == DiscountType.FixedAmount
                        ? voucher.DiscountValue
                        : subTotal * (voucher.DiscountValue / 100);
                }
                else
                {
                    appliedVoucherCode = null; // Đảm bảo không lưu voucher không hợp lệ vào đơn hàng
                }
            }

            var order = new Order
            {
                UserId = user.Id,
                CustomerName = model.CustomerName,
                Phone = model.Phone,
                Address = model.Address,
                OrderDate = DateTime.Now,
                PaymentMethod = model.PaymentMethod,
                PaymentStatus = "Pending",
                Discount = discountAmount,
                VoucherCode = appliedVoucherCode,
                TotalAmount = subTotal - discountAmount,
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId ?? 0,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price ?? 0
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            if (model.PaymentMethod == "VNPAY")
            {
                var vnPayModel = new VnPayRequestModel
                {
                    Amount = (double)order.TotalAmount,
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
                await _context.SaveChangesAsync();

                HttpContext.Session.Remove(CartSession);
                HttpContext.Session.Remove(VoucherSession);
                TempData["Message"] = "Đặt hàng thành công!";
                return RedirectToAction("OrderConfirmation", new { id = order.Id });
            }
        }

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
                HttpContext.Session.Remove(VoucherSession);
                TempData["Message"] = $"Thanh toán thành công cho đơn hàng #{order.Id}!";
                return RedirectToAction("OrderConfirmation", new { id = order.Id });
            }

            TempData["Message"] = "Không tìm thấy đơn hàng tương ứng!";
            return RedirectToAction("PaymentFail");
        }
        

        public IActionResult OrderConfirmation(int id)
        {
            var order = _context.Orders.Include(o => o.OrderItems)
                                         .ThenInclude(oi => oi.Product)
                                         .FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            return View(order);
        }


        public IActionResult PaymentFail()
        {
            return View();
        }
    }
}