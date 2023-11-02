using BLL.Services.Interfaces;
using BLL.ViewModels;
using Humanizer;
using LibraryDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagementSystem.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7073/");
        private readonly HttpClient _client;

        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserVM model)
        {

            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("api/User/Index", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login","Book");
                }
            }
            return View();
        }
    }
}
