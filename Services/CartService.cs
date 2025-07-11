using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebBanHang.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebBanHang.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSession = "CartSession";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public List<CartItem> GetCartItems()
        {
            var sessionData = Session.GetString(CartSession);
            return string.IsNullOrEmpty(sessionData)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(sessionData);
        }

        public void SaveCartSession(List<CartItem> cartItems)
        {
            var json = JsonConvert.SerializeObject(cartItems);
            Session.SetString(CartSession, json);
        }

        public void ClearCart()
        {
            Session.Remove(CartSession);
        }
    }
}
