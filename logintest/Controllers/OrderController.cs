using DHSHOP.Models;
using logintest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace logintest.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ImageDbContext _context;
        private readonly Cart _cart;

        public OrderController(ImageDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var cartItems = _cart.GetAllCartItems();
            _cart.CartItems = cartItems;

            if (_cart.CartItems.Count == 0)
            {
                ModelState.AddModelError("", "Cart is empty, please add a book first.");
            }

            if (ModelState.IsValid)
            {
                CreateOrder(order);
                _cart.ClearCart();
                return View("CheckoutComplete", order);
            }

            return View(order);
        }

        public IActionResult CheckoutComplete(Order order)
        {
            return View(order);
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            var cartItems = _cart.CartItems;

            foreach (var item in cartItems)
            {
                var orderitem = new OrderItem()
                {
                    Quantity = item.Quantity,
                    VGAId = item.image.ProductId,
                    OrderId = order.Id,
                    VGAImage = item.image.ProductImage,
                    VGAName = item.image.ProductName,
                    Price = item.image.ProductPrice * item.Quantity
                };
                order.OrderItems.Add(orderitem);
                order.OrderTotal += orderitem.Price;
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public IActionResult Shipping()
        {
            return View();
        }
    }
}
