using Microsoft.AspNetCore.Mvc;
using WebBanHang.Data;
using WebBanHang.Models;
using WebBanHang.Services;
using WebBanHang.Helpers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace WebBanHang.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string CartSession = "CartSession";
        private const string VoucherSession = "VoucherCode";

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSession) ?? new List<CartItem>();
            var subTotal = cartItems.Sum(c => (c.Price ?? 0) * c.Quantity);

            var viewModel = new CartViewModel
            {
                CartItems = cartItems,
                SubTotal = subTotal,
                GrandTotal = subTotal,
            };

            var voucherCode = HttpContext.Session.GetString(VoucherSession);
            if (!string.IsNullOrEmpty(voucherCode))
            {
                var voucher = _context.Vouchers.FirstOrDefault(v => v.Code == voucherCode && v.IsActive && v.ExpiryDate > DateTime.Now);
                if (voucher != null && subTotal >= voucher.MinAmount)
                {
                    viewModel.AppliedVoucherCode = voucher.Code;
                    if (voucher.DiscountType == DiscountType.FixedAmount)
                    {
                        viewModel.DiscountAmount = voucher.DiscountValue;
                    }
                    else // Percentage
                    {
                        viewModel.DiscountAmount = subTotal * (voucher.DiscountValue / 100);
                    }
                    viewModel.GrandTotal = subTotal - viewModel.DiscountAmount;
                }
                else
                {
                    // Nếu voucher không còn hợp lệ (ví dụ: giỏ hàng thay đổi không đủ MinAmount), xóa khỏi session
                    HttpContext.Session.Remove(VoucherSession);
                }
            }

            return View(viewModel);
        }


        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSession) ?? new List<CartItem>();

            var existingItem = cart.FirstOrDefault(c => c.ProductId == id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            HttpContext.Session.SetObjectAsJson(CartSession, cart);
            TempData["Message"] = "Đã thêm vào giỏ hàng thành công!";
            
            var referer = Request.Headers["Referer"].ToString();
            return !string.IsNullOrEmpty(referer) ? Redirect(referer) : RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSession) ?? new List<CartItem>();
            var itemToRemove = cart.FirstOrDefault(c => c.ProductId == id);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.SetObjectAsJson(CartSession, cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSession);
            if (cart == null || !cart.Any())
            {
                TempData["Message"] = "Giỏ hàng rỗng!";
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Checkout", "Order");
        }
        
        [HttpPost]
        public IActionResult UpdateQuantity(int[] ProductIds, int[] Quantities)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSession) ?? new List<CartItem>();

            for (int i = 0; i < ProductIds.Length; i++)
            {
                var item = cart.FirstOrDefault(c => c.ProductId == ProductIds[i]);
                if (item != null && Quantities[i] > 0)
                {
                    item.Quantity = Quantities[i];
                }
            }

            HttpContext.Session.SetObjectAsJson(CartSession, cart);
            TempData["Message"] = "Cập nhật giỏ hàng thành công!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ApplyVoucher(string voucherCode)
        {
            if (string.IsNullOrEmpty(voucherCode))
            {
                TempData["Message"] = "Vui lòng nhập mã voucher.";
                return RedirectToAction("Index");
            }
            
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSession) ?? new List<CartItem>();
            var cartTotal = cart.Sum(c => (c.Price ?? 0) * c.Quantity);

            var voucher = _context.Vouchers.FirstOrDefault(v => v.Code.ToUpper() == voucherCode.ToUpper() && v.IsActive && v.ExpiryDate > DateTime.Now);

            if (voucher == null)
            {
                TempData["Message"] = "Mã voucher không hợp lệ hoặc đã hết hạn!";
            }
            else if (cartTotal < voucher.MinAmount)
            {
                TempData["Message"] = $"Voucher chỉ áp dụng cho đơn hàng từ {voucher.MinAmount:N0}đ.";
            }
            else
            {
                HttpContext.Session.SetString(VoucherSession, voucher.Code);
                TempData["Message"] = "Áp dụng voucher thành công!";
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveVoucher()
        {
            HttpContext.Session.Remove(VoucherSession);
            TempData["Message"] = "Đã xóa voucher.";
            return RedirectToAction("Index");
        }
    }
}