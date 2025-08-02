using System.Diagnostics;
using AzureBlobProject.Models;
using AzureBlobProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContainerService _containerserivce;

        public HomeController(ILogger<HomeController> logger,IContainerService containerService)
        {
            _logger = logger;
            _containerserivce = containerService;
        }

        public IActionResult Index()
        {
            return View(_containerserivce.GetAllContainerAndBlob().GetAwaiter().GetResult());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
