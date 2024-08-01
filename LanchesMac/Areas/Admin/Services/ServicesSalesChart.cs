using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Areas.Admin.Services
{
    public class ServicesSalesChart
    {
        private readonly AppDbContext _context;

        public ServicesSalesChart(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GraphicSnack>> GetSalesSnack(int days = 360)
        {
            var data = DateTime.Now.AddDays(-days);

            var lanches = await _context.DetailsOrder
                .Where(pd => pd.Order.OrderDispatched >= data)
                .GroupBy(pd => new { pd.SnackId, pd.Snack.Name, pd.Amount })
                .Select(g => new
                {
                    SnackName = g.Key.Name,
                    SnacksAmount = g.Sum(q => q.Amount),
                    SnacksTotalValue = g.Sum(a => a.Price * a.Amount)
                })
                .ToListAsync();


            var lista = lanches.Select(item => new GraphicSnack
            {
                SnackName = item.SnackName,
                SnacksAmount = item.SnacksAmount,
                SnacksTotalValue = item.SnacksTotalValue
            }).ToList();

            return lista;

        }
    }
}
