using LanchesMac.Context;
using LanchesMac.Models;

namespace LanchesMac.Areas.Admin.Services
{
    public class ServicesSalesChart
    {
        private readonly AppDbContext _context;

        public ServicesSalesChart(AppDbContext context)
        {
            _context = context;
        }

        public List<GraphicSnack> GetSalesSnack(int days = 360)
        {
            var data = DateTime.Now.AddDays(-days);

            var snacks = (from pd in _context.DetailsOrder
                          join l in _context.Snacks on pd.SnackId equals l.Id
                          where pd.Order.OrderDispatched >= data
                          group pd by new { pd.SnackId, l.Name, pd.Amount }
                          into g
                          select new
                          {
                              SnackName = g.Key.Name,
                              SnacksAmount = g.Sum(q => q.Amount),
                              SnacksTotalValue = g.Sum(a => a.Price * a.Amount)
                          });

            var lista = new List<GraphicSnack>();
            foreach (var item in snacks)
            {
                var lanche = new GraphicSnack();
                lanche.SnackName = item.SnackName;
                lanche.SnacksAmount = item.SnacksAmount;
                lanche.SnacksTotalValue = item.SnacksTotalValue;
                lista.Add(lanche);
            }
            return lista;
        }
    }
}
