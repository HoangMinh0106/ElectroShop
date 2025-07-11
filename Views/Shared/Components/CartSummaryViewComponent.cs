using Microsoft.AspNetCore.Mvc;
using WebBanHang.Helpers;
using WebBanHang.Models;

namespace WebBanHang.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CartSession") ?? new List<CartItem>();
            int totalQuantity = cart.Sum(item => item.Quantity);
            return View(totalQuantity);
        }
    }
}
