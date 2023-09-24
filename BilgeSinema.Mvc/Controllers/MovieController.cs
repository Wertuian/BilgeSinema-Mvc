using BilgeSinema.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BilgeSinema.Mvc.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppSettings _appSettings;
        public MovieController(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public IActionResult New()
        {
            return View("Form");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var getUrl = $"{_appSettings.ApiBaseUrl}/movies/{id}";

            using var client = new HttpClient();

            var response = await client.GetFromJsonAsync<MovieFormViewModel>(getUrl) ;

            return View("Form",response);
            
        }



        [HttpPost]
        public async Task<IActionResult> Save(MovieFormViewModel formData) 
        {
            if (formData.Id == 0) // Create
            {
                var insertUrl = $"{_appSettings.ApiBaseUrl}/movies";

                using var client = new HttpClient();

                var response = await client.PostAsJsonAsync(insertUrl, formData);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Film kayıt edilirken bir hata oluştu.";
                    return View("Form", formData);
                }
            }
            else // Update
            {
                var updateUrl = $"{_appSettings.ApiBaseUrl}/movies/{formData.Id}";
                using var client = new HttpClient();
                
                var response = await client.PutAsJsonAsync(updateUrl, formData);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Film Güncellenirken bir hata oluştu.";
                    return View("Form", formData);
                }
            }
            
        }
        public async Task<IActionResult> Delete(int id)
        {
            var deleteUrl = $"{_appSettings.ApiBaseUrl}/movies/{id}";

            using var client = new HttpClient();

            var response = await client.DeleteAsync(deleteUrl);

            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> MakeDiscount(int id)
        {
            var patchUrl = $"{_appSettings.ApiBaseUrl}/movies/{id}";

            using var client = new HttpClient();

            var response = await client.PatchAsync(patchUrl, null);

            return RedirectToAction("Index", "Home");

        }
    }
}


