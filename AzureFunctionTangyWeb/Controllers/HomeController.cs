using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureFunctionTangyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace AzureFunctionTangyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly BlobServiceClient _blobServiceClient;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory,BlobServiceClient blobServiceClient)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _blobServiceClient = blobServiceClient;
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7139/api/");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index( SalesRequest salesRequest, IFormFile file)
        {
            salesRequest.Id =Guid.NewGuid().ToString();
            using (var content = new StringContent(JsonConvert.SerializeObject(salesRequest), System.Text.Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage responseMessage = await _httpClient.PostAsync("OnSalesUploadWriteToQueue", content);
                string returnValue =await responseMessage.Content.ReadAsStringAsync();
            }

            if(file != null)
            {
                string filename = salesRequest.Id + Path.GetExtension(file.Name);
                BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient("functionsalesrep");
                var blobClient = blobContainerClient.GetBlobClient(filename);
                var httpheader = new BlobHttpHeaders()
                {
                    ContentType=file.ContentType
                };

                await blobClient.UploadAsync(file.OpenReadStream(), httpheader).ConfigureAwait(false);
            }

            return RedirectToAction(nameof(Index)); 
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
