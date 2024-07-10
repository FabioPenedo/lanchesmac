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
            int totalItemsOrdered = 0;
            decimal priceTotalOrdered = 0.0m;

            //obtem os itens do carrinho de compra do cliente
            List<CartPurchaseItem> items = _cartPurchase.GetCartPurchaseItens();
            _cartPurchase.Items = items;

            //verifica se existem itens de pedido
            if(_cartPurchase.Items.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho esta vazio, que tal incluir um lanche...");
            }

            //calcula o total de itens e o total de pedido
            foreach (var item in items)
            {
                totalItemsOrdered += item.Amount;
                priceTotalOrdered += (item.Snack.Price * item.Amount);
            }

            //atribui os valores obtidos ao pedido
            order.TotalItemsOrdered = totalItemsOrdered;
            order.TotalOrder = priceTotalOrdered;

            //valida os dados do pedido
            if(ModelState.IsValid)
            {
                //cria o pedido e os detalhes
                _orderRepository.CreateOrder(order);

                //define mensagens ao cliente
                ViewBag.CheckoutCompleteMessage = "Obrigado pelo seu pedido :)";
                ViewBag.TotalOrder = _cartPurchase.GetCartTotalPurchase();

                //limpa o carrinho do cliente
                _cartPurchase.CleanCart();

                //exibe a view com dados do cliente e do pedido
                return View("~/Views/Order/CheckoutComplete.cshtml", order);
            }
            return View(order);
        }
    }
}
