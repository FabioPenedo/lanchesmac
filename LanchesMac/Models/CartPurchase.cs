using LanchesMac.Context;
using System.Runtime.Intrinsics.X86;

namespace LanchesMac.Models
{
    public class CartPurchase
    {
        private readonly AppDbContext _context;

        public CartPurchase(AppDbContext context)
        {
            _context = context;
        }

        public string CartPurchaseId { get; set; } = string.Empty;
        public List<CartPurchaseItem> Items { get; set; } = new List<CartPurchaseItem>();

        public static CartPurchase GetCart(IServiceProvider services)
        {
            //define uma sessão
            var httpContextAccessor =
                services.GetRequiredService<IHttpContextAccessor>();

            if (httpContextAccessor == null)
            {
                throw new InvalidOperationException("IHttpContextAccessor is not available.");
            }

            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext is not available.");
            }

            var session = httpContext.Session;
            if (session == null)
            {
                throw new InvalidOperationException("Session is not available.");
            }


            //obtem um serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>() ??
                throw new InvalidOperationException("The AppDbContext service is not registered.");


            //obtem ou gera o Id do carrinho
            string carrinhoId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            //atribui o id do carrinho na Sessão
            session.SetString("CartId", carrinhoId);

            return new CartPurchase(context)
            {
                CartPurchaseId = carrinhoId
            };
        }

        public void AddToCart(Snack snack)
        {
            var cartPurchaseItem = _context.CartPurchaseItens.SingleOrDefault(
                x => x.Snack.Id == snack.Id &&
                x.CartPurchaseId == CartPurchaseId);

            if (cartPurchaseItem == null)
            {
                cartPurchaseItem = new CartPurchaseItem
                {
                    CartPurchaseId = CartPurchaseId,
                    Snack = snack,
                    Amount = 1
                };
                _context.CartPurchaseItens.Add(cartPurchaseItem);
            }
            else
            {
                cartPurchaseItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveFromCart(Snack snack)
        {
            var cartPurchaseItem = _context.CartPurchaseItens.SingleOrDefault(
                x => x.Snack.Id == snack.Id &&
                x.CartPurchaseId == CartPurchaseId);

            if (cartPurchaseItem != null)
            {
                if (cartPurchaseItem.Amount > 1)
                {
                    cartPurchaseItem.Amount--;

                }
                else
                {
                    _context.CartPurchaseItens.Remove(cartPurchaseItem);
                }

            }
            _context.SaveChanges();
        }
    }
}
