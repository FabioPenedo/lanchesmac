using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class CartPurchaseController : Controller
    {
        private readonly ISnackRepository _snackRepository;
        private readonly CartPurchase _cartPurchase;

        public CartPurchaseController(ISnackRepository snackRepository, CartPurchase cartPurchase)
        {
            _snackRepository = snackRepository;
            _cartPurchase = cartPurchase;
        }

        public IActionResult Index()
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

        [Authorize]
        public IActionResult AddItemToCart(int snackId)
        {
            var selectedSnack = _snackRepository.Snacks.FirstOrDefault(p => p.Id == snackId);
            if(selectedSnack != null)
            {
                _cartPurchase.AddToCart(selectedSnack);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult RemoveItemToCart(int snackId)
        {
            var selectedSnack = _snackRepository.Snacks.FirstOrDefault(p => p.Id == snackId);
            if (selectedSnack != null)
            {
                _cartPurchase.RemoveFromCart(selectedSnack);
            }
            return RedirectToAction("Index");
        }
    }
}
