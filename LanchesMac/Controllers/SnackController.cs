using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class SnackController : Controller
    {
        private readonly ISnackRepository _snackRepository;

        public SnackController(ISnackRepository snackRepository)
        {
            _snackRepository = snackRepository;
        }

        public IActionResult List(string category)
        {
            IEnumerable<Snack> snacks;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                snacks = _snackRepository.Snacks.OrderBy(l => l.Id);
                currentCategory = "Todos os lanches";
            }
            else
            {
                snacks = _snackRepository.Snacks
                         .Where(l => l.Category.Name.Equals(category))
                         .OrderBy(l => l.Name);

                currentCategory = category;
            }

            var snackListViewModel = new SnackListViewModel
            {
                Snacks = snacks,
                CurrentCategory = currentCategory,
            };

            return View(snackListViewModel);
        }

        public IActionResult Details(int snackId)
        {
            var snack = _snackRepository.Snacks.FirstOrDefault(l => l.Id == snackId);
            return View(snack);
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Snack> snacks;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                snacks = _snackRepository.Snacks.OrderBy(p => p.Id);
                currentCategory = "Todos os lanches";
            }
            else
            {
                snacks = _snackRepository.Snacks
                         .Where(p => p.Name.ToLower().Contains(searchString.ToLower()));

                currentCategory = snacks.Any() ? "Lanches" : "Nenhum lanche foi encontrado";
            }

            return View("~/Views/Snack/List.cshtml", new SnackListViewModel
            {
                Snacks = snacks,
                CurrentCategory = currentCategory
            });
        }
    }
}
