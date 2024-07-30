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

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }

            if (files.Count > 10)
            {
                ViewData["Erro"] = "Error: Quantidade de arquivos excedeu o limite";
                return View(ViewData);
            }

            long size = files.Sum(f => f.Length);

            var filePathsName = new List<string>();

            string filePath = Path.Combine(_webHostEnvironment.WebRootPath,
                _myConfig.NomePastaImagensProdutos);


            foreach (var formFile in files)
            {
                if (formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".gif")
                    || formFile.FileName.Contains(".png"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                    filePathsName.Add(fileNameWithPath);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor, " +
                                    $"com tamanho total de : {size} bytes";

            ViewBag.Arquivos = filePathsName;
            return View(ViewData);
        }

        public IActionResult GetImages()
        {
            FileManagerModel model = new FileManagerModel();

            string userImagesPath = Path.Combine(_webHostEnvironment.WebRootPath,
                _myConfig.NomePastaImagensProdutos);

            DirectoryInfo dir = new DirectoryInfo(userImagesPath);

            FileInfo[] files = dir.GetFiles();

            model.PathImagesProduct = _myConfig.NomePastaImagensProdutos;

            if(files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {userImagesPath}";
            }

            model.Files = files;

            return View(model);
        }

        public IActionResult DeleteFile(string fname)
        {
            string imageDelete = Path.Combine(_webHostEnvironment.WebRootPath,
               _myConfig.NomePastaImagensProdutos + "\\", fname);

            if((System.IO.File.Exists(imageDelete)))
            {
                System.IO.File.Delete(imageDelete);

                ViewData["Deletado"] = $"Arquivo(s) {imageDelete} deletado com sucesso";
            }

            return View("Index");
        }
    }
}
