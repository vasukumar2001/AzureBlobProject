using AzureBlobProject.Models;
using AzureBlobProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;

namespace AzureBlobProject.Controllers
{
    public class ContainerController : Controller
    {
        private readonly IContainerService _containerService;
        public ContainerController(IContainerService containerService)
        {
            _containerService = containerService;
        }

        public async Task<IActionResult> Index()
        {
            var allContainer = await _containerService.GetAllContainer().ConfigureAwait(false);
            return View(allContainer);
        }

        public async Task<IActionResult> Create()
        {
            return View(new ContainerModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContainerModel container)
        {
            await _containerService.CreateContainer(container.Name).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string containername)
        {
            await _containerService.DeleteContainer(containername).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }


    }

}
