using System.ComponentModel.DataAnnotations;

namespace LanchesMac.Models
{
    public class CartPurchaseItem
    {
        public int Id { get; set; }
        public Snack Snack { get; set; } = new Snack();
        public int Amount { get; set; }


        [StringLength(200)]
        public string CartPurchaseId { get; set; } = string.Empty;
    }
}
