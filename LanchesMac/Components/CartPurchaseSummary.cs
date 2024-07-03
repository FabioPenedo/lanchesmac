using LanchesMac.Models;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Components
{
    public class CartPurchaseSummary : ViewComponent
    {
        private readonly CartPurchase _cartPurchase;

        public CartPurchaseSummary(CartPurchase cartPurchase)
        {
            _cartPurchase = cartPurchase;
        }

        public IViewComponentResult Invoke()
        {
            var itens = _cartPurchase.GetCartPurchaseItens();
            _cartPurchase.Items = itens;

            var cartPurchaseVM = new CartPurchaseViewModel
            {
                CartPurchase = _cartPurchase,
                CartPurchaseTotal = _cartPurchase.GetCartTotalPurchase()
            };

            return View(cartPurchaseVM);
        }
    }
}
