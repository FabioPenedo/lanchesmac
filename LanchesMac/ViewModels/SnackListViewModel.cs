using LanchesMac.Models;

namespace LanchesMac.ViewModels
{
    public class SnackListViewModel
    {
        public IEnumerable<Snack> Snacks { get; set; } = Enumerable.Empty<Snack>();
        public string CurrentCategory { get; set; } = string.Empty;
    }
}
