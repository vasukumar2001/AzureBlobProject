using AzureBlobProject.Models;
using AzureBlobProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobProject.Controllers
{
    public class BlobController : Controller
    {
        private readonly IBlobService _blobService;
        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }
        [HttpGet]
        public async Task<IActionResult> Manage(string containername)
        {
            var blobObj = await _blobService.GetAllBlobs(containername).ConfigureAwait(false);
            return View(blobObj);
        }

        [HttpGet]
        public async Task<IActionResult> AddFile(string containername)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(string containername, IFormFile file)
        {
            if (file == null || file.Length < 1) return View();

            var filename =Path.GetFileNameWithoutExtension(file.FileName) +"_"+Guid.NewGuid()+Path.GetExtension(file.FileName);
            var result = await _blobService.CreateBlob(filename, file, containername, new BlobModel()).ConfigureAwait(false);

            if (result)
            {
                return RedirectToAction("Index", "Container");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewFile(string name, string containername)
        {
            return Redirect(await _blobService.GetBlob(name, containername).ConfigureAwait(false));
        }


        [HttpGet]
        public async Task<IActionResult> DeleteFile(string name, string containername)
        {
            await _blobService.DeleteBlob(name, containername).ConfigureAwait(false);

            return RedirectToAction("Manage", new { containername });
        }
    }
}
