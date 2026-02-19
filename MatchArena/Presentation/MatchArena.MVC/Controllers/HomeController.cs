using MatchArena.Domain.Entities;
using MatchArena.MVC.Models;
using MatchArena.MVC.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;

namespace MatchArena.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestClient _client;

        public HomeController()
        {
            _client = new RestClient("https://localhost:7246/");
        }
        public async Task<IActionResult> Index()
        {
            RestRequest request = new RestRequest("Categories",Method.Get);

            var response = await _client.ExecuteAsync<List<GetCategoryItemVM>>(request);
            
            

            return View(response.Data);
        }
            
    }
}
