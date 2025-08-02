using AzureFunctionTangyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace AzureFunctionTangyWeb.Controllers
{
    public class GroceryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string GroceryAPIUrl = "http://localhost:7139/api/GroceryList";

        public GroceryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(GroceryAPIUrl);

            var response = await client.GetAsync(GroceryAPIUrl).ConfigureAwait(false);
            var returnValue =  response.Content.ReadAsStringAsync().Result;

            var groceryListToReturn = JsonConvert.DeserializeObject<List<GroceryItem>>(returnValue);

            return View(groceryListToReturn);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: GroceryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GroceryItem obj)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(obj);
                using (var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json"))
                {
                    using var client = _httpClientFactory.CreateClient();
                    client.BaseAddress = new Uri(GroceryAPIUrl);
                    HttpResponseMessage response = await client.PostAsync(GroceryAPIUrl, content);
                    string returnValue = await response.Content.ReadAsStringAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroceryController/Edit/{id}
        public async Task<ActionResult> Edit(string id)
        {
            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(GroceryAPIUrl);
            HttpResponseMessage response = await client.GetAsync(GroceryAPIUrl + "/" + id).ConfigureAwait(false);
            string returnValue = response.Content.ReadAsStringAsync().Result;
            GroceryItem groceryItem = JsonConvert.DeserializeObject<GroceryItem>(returnValue);
            return View(groceryItem);
        }

        // POST: GroceryController/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GroceryItem obj)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(obj);
                using (var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json"))
                {
                    using var client = _httpClientFactory.CreateClient();
                    client.BaseAddress = new Uri(GroceryAPIUrl);
                    HttpResponseMessage response = await client.PutAsync(GroceryAPIUrl + "/" + obj.Id, content).ConfigureAwait(false);
                    string returnValue = response.Content.ReadAsStringAsync().Result;
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroceryController/Delete/{id}
        public async Task<ActionResult> Delete(string id)
        {
            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(GroceryAPIUrl);
            HttpResponseMessage response = await client.GetAsync(GroceryAPIUrl + "/" + id).ConfigureAwait(false);
            string returnValue = response.Content.ReadAsStringAsync().Result;
            GroceryItem groceryItem = JsonConvert.DeserializeObject<GroceryItem>(returnValue);
            return View(groceryItem);
        }

        // POST: GroceryController/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePOST(string id)
        {
            try
            {
                using var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(GroceryAPIUrl);
                HttpResponseMessage response = await client.DeleteAsync(GroceryAPIUrl + "/" + id).ConfigureAwait(false);
                string returnValue = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
