using LanchesMac.Areas.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminGraphicController : Controller
    {
        private readonly ServicesSalesChart _salesChart;

        public AdminGraphicController(ServicesSalesChart salesChart)
        {
            _salesChart = salesChart ?? throw new ArgumentNullException(nameof(salesChart));
        }

        public async Task<JsonResult> SalesSnacks(int dias)
        {
            var snacksTotalSales = await _salesChart.GetSalesSnack(dias);
            return Json(snacksTotalSales);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MonthlySales()
        {
            return View();
        }
        public IActionResult WeeklySales()
        {
            return View();
        }
    }
}
