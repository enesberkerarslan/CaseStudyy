using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using Uniteds.CaseStudy.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace Uniteds.CaseStudy.Web.Controllers
{
    public class HomeController : Controller
    {
        


        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:44349/api/notes");
            var userId = 1; // Kullanıcının kimliğini alacak doğru kodu buraya ekleyin veya 1 değerini kullanmaya devam edin

            ViewData["UserId"] = userId;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var notes = JsonConvert.DeserializeObject<List<Note>>(content);

                var hierarchicalList = GenerateHierarchicalList(notes, null);
                var count = hierarchicalList.Count; // Ana notların sayısını al

                CheckChildren(hierarchicalList, ref count); // Çocukları kontrol et

                return View(hierarchicalList);
            }
            else
            {
                // İstek başarısız oldu, hata işleme yapabilirsiniz
            }

            // Geri dönüş değeri
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:44349/api/"); // API adresini düzeltin
                    var response = await httpClient.DeleteAsync($"notes/{id}");
                    response.EnsureSuccessStatusCode();
                }

                return RedirectToAction("Index"); // Silme işlemi başarılı olduğunda başka bir sayfaya yönlendirilebilirsiniz
            }
            catch (Exception ex)
            {
                // Hata durumunda yapılacak işlemler
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Note note)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:44349/api/notes");
                    note.UserId = 1;
                    var json = JsonConvert.SerializeObject(note);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    
                    var response = await httpClient.PostAsync("", content);
                    response.EnsureSuccessStatusCode();
                }

                return RedirectToAction("Index"); // Ekleme işlemi başarılı olduğunda başka bir sayfaya yönlendirilebilirsiniz
            }
            catch (Exception ex)
            {
                // Hata durumunda yapılacak işlemler
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Note note)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:44349/api/");
                    var json = JsonConvert.SerializeObject(note);
                    note.UserId = 1;
                    note.ParentId = null;
                    note.IsDeleted = false;

                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync($"notes/{id}", content);
                    response.EnsureSuccessStatusCode();
                }

                return RedirectToAction("Index"); // Güncelleme işlemi başarılı olduğunda başka bir sayfaya yönlendirilebilirsiniz
            }
            catch (Exception ex)
            {
                // Hata durumunda yapılacak işlemler
                return View("Error");
            }
        }






        private List<Note> GenerateHierarchicalList(List<Note> notes, int? parentId)
        {
            var hierarchicalList = new List<Note>();

            var filteredNotes = notes.Where(n => n.ParentId == parentId).ToList();
            foreach (var note in filteredNotes)
            {
                var children = GenerateHierarchicalList(notes, note.Id);
                if (children.Any())
                {
                    note.Children = children;
                }

                hierarchicalList.Add(note);
            }

            return hierarchicalList;
        }
        private void CheckChildren(List<Note> notes, ref int count)
        {
            foreach (var note in notes)
            {
                if (count >= 8)
                {
                    return; // İstenilen sayıya ulaşıldığında döngüden çık
                }

                if (note.Children != null && note.Children.Any())
                {
                    count += note.Children.Count;
                    CheckChildren(note.Children, ref count);
                }
            }
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
