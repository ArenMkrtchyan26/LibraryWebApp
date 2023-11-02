using BLL.ViewModels;
using LibraryDAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7073/");
        private readonly HttpClient _client;
        public BookController()
        {
            _client = new HttpClient(); 
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<BookAddEditVM> books = new List<BookAddEditVM>();
            
            
            HttpResponseMessage response=_client.GetAsync(_client.BaseAddress+ "api/Book/GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                string data=response.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<BookAddEditVM>>(data);
            }
            return View(books);
        }
        [HttpGet]
        public IActionResult FirstPage()
        {
            
            return View();
        }
        [HttpGet]
        public IActionResult AddBook(string pass)
        {
            ViewBag.Pass = pass;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(BookAddEditVM bookVM, string pass)
        {
            bookVM.IsIssued = true;
            if (ModelState.IsValid)
            {

                HttpResponseMessage response = await _client.PostAsJsonAsync($"api/Book/Index?pass={pass}", bookVM);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index","Book");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBooksUser(LoginUser model)
        {
            ViewBag.Pass = model.Password;
            if (ModelState.IsValid)
            {

                List<BookAddEditVM> books = new List<BookAddEditVM>();
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"api/Book/GetUserBooks?password={model.Password}");
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    books = JsonConvert.DeserializeObject<List<BookAddEditVM>>(data);
                    return View(books);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.BookId= id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBook(EditBookVM editVM,int Id)
        {
            editVM.ID=Id;
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/Book/Update", editVM);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Book");
            }
            return RedirectToAction("Index", "Book");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + $"api/Book/DeleteBook?Id={Id}");
            return RedirectToAction("Index","Book");
        }
    }
}
