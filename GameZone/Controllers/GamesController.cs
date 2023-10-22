using GameZone.Services;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService;

        public GamesController(ICategoriesService categoriesService, IDevicesService devicesService)
        {
            _categoriesService = categoriesService;
            _devicesService = devicesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesService.GetSelectList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateGameFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _categoriesService.GetSelectList();
                viewModel.Devices = _devicesService.GetSelectList(); ;
                return View(viewModel);
            }

            // Save game to database
            // Save cover to server

            return RedirectToAction(nameof(Index));
        }
    }
}
