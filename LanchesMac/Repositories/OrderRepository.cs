using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly CartPurchase _cartPurchase;

        public OrderRepository(AppDbContext appDbContext, CartPurchase cartPurchase)
        {
            _appDbContext = appDbContext;
            _cartPurchase = cartPurchase;
        }

        public void CreateOrder(Order order)
        {
            order.OrderDispatched = DateTime.Now;
            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();

            var cartPurchaseItens = _cartPurchase.Items;

            if(cartPurchaseItens != null)
            {
                foreach (var cartItem in cartPurchaseItens)
                {
                    var detailOrder = new DetailOrder()
                    {
                        Amount = cartItem.Amount,
                        SnackId = cartItem.Snack.Id,
                        OrderId = order.Id,
                        Price = cartItem.Snack.Price,
                    };
                    _appDbContext.DetailsOrder.Add(detailOrder);
                }
                _appDbContext.SaveChanges();
            }
        }
    }
}
