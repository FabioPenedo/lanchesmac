using LanchesMac.Areas.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminSalesReportController : Controller
    {
        private readonly ServicesSalesReport _servicesSalesReport;

        public AdminSalesReportController(ServicesSalesReport servicesSalesReport)
        {
            _servicesSalesReport = servicesSalesReport;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSalesReport(DateTime? minDate, DateTime? maxDate)
        {
            if(!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if(!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _servicesSalesReport.FindByDateAsync(minDate, maxDate);
            return View(result);
        }
    }
}
