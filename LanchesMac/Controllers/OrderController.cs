using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly CartPurchase _cartPurchase;

        public OrderController(IOrderRepository orderRepository, CartPurchase cartPurchase)
        {
            _orderRepository = orderRepository;
            _cartPurchase = cartPurchase;
        }

        public IActionResult Checkout()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            return View();
        }
    }
}
