using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string BaseUrl = "https://localhost:7090/api/Categories";

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(BaseUrl);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }

            ViewBag.ErrorMessage = "Kategori listesi alınamadı.";
            return View();
        }

        [HttpGet]
        [Route("CreateCategory")]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Lütfen gerekli alanları doldurun.";
                return View(createCategoryDto);
            }

            var client = _httpClientFactory.CreateClient();
            var stringContent = CreateJsonContent(createCategoryDto);
            var responseMessage = await client.PostAsync(BaseUrl, stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }

            ViewBag.ErrorMessage = "Kategori oluşturulamadı. Lütfen tekrar deneyin.";
            return View(createCategoryDto);
        }

        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"{BaseUrl}?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }

            ViewBag.ErrorMessage = "Silme işlemi başarısız oldu.";
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }

        [Route("UpdateCategory/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{BaseUrl}/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);
                return View(category);
            }

            ViewBag.ErrorMessage = "Kategori bilgisi alınamadı. Lütfen tekrar deneyin.";
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }

        [Route("UpdateCategory/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Lütfen gerekli alanları doldurun.";
                return View(updateCategoryDto);
            }

            var client = _httpClientFactory.CreateClient();
            var stringContent = CreateJsonContent(updateCategoryDto);
            var responseMessage = await client.PutAsync(BaseUrl, stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }

            ViewBag.ErrorMessage = "Güncelleme işlemi başarısız oldu. Lütfen tekrar deneyin.";
            return View(updateCategoryDto);
        }

        private StringContent CreateJsonContent(object data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            return new StringContent(jsonData, Encoding.UTF8, "application/json");
        }
    }
}
