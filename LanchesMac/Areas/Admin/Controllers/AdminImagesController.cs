using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagesController : Controller
    {
        private readonly ConfigurationImages _myConfig;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminImagesController(IWebHostEnvironment hostEnvironment, IOptions<ConfigurationImages> myConfiguration)
        {
            _webHostEnvironment = hostEnvironment;
            _myConfig = myConfiguration.Value;

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
