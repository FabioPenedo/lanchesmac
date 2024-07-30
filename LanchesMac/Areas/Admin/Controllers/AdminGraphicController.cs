﻿using LanchesMac.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    public class AdminGraphicController : Controller
    {
        private readonly ServicesSalesChart _salesChart;

        public AdminGraphicController(ServicesSalesChart salesChart)
        {
            _salesChart = salesChart ?? throw new ArgumentNullException(nameof(salesChart));
        }

        public JsonResult SalesSnacks(int dias)
        {
            var snacksTotalSales = _salesChart.GetSalesSnack(dias);
            return Json(snacksTotalSales);
        }

        public IActionResult Index(int dias)
        {
            return View();
        }

        public IActionResult MonthlySales(int dias)
        {
            return View();
        }
        public IActionResult WeeklySales(int dias)
        {
            return View();
        }
    }
}