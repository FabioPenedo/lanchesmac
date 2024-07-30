using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    public class DetailOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int SnackId { get; set; }
        public int Amount { get; set; }



        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }



        public virtual Snack Snack { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;


    }
}
