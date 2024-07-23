using LanchesMac.Models;

namespace LanchesMac.ViewModels
{
    public class OrderSnackViewModel
    {
        public Order Order { get; set; } = null!;
        public IEnumerable<DetailOrder> DetailOrder { get; set; } = null!;
    }
}
