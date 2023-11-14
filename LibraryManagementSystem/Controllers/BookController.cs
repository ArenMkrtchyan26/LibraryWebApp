using BLL.ViewModels;
using LibraryDAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json.Serialization;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        Uri baseAddress = new Uri("https://localhost:7073/");
        private readonly HttpClient _client;
        public BookController(IWebHostEnvironment webHostEnvironment)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<BookAddEditVM> books = new List<BookAddEditVM>();


            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Book/GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
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
        public async Task<IActionResult> AddBook(BookAddEditVM bookVM, string pass,IFormFile image)
        {string fileName=string.Empty;
            if (image != null)
            {
                fileName = Guid.NewGuid() + System.IO.Path.GetExtension(image.FileName);
                string path = $"{_webHostEnvironment.WebRootPath}/images/books/{fileName}";
                //bookVM.ImagePath = path;
                using var fileStream = new FileStream(path, FileMode.Create);
                image.CopyTo(fileStream);
            }
            else { }
            bookVM.Upload = fileName;
            bookVM.IsIssued = true;
            if (ModelState.IsValid)
            {

                HttpResponseMessage response = await _client.PostAsJsonAsync($"api/Book/Index?pass={pass}", bookVM);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Book");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login(string? value)
        {if(value != null)
            {
                ViewBag.Message=value;
                return View();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBooksUser(LoginUser model)
        { 
            HttpResponseMessage responseRequest = await _client.GetAsync(_client.BaseAddress +
                                               $"api/User/CheckUser?password={model.Password}");
            if (responseRequest.IsSuccessStatusCode)
            {
                string check = responseRequest.Content.ReadAsStringAsync().Result;
                ViewBag.Pass = model.Password;
                if (ModelState.IsValid && check == "true")
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
                else
                {
                    string message = "Invalid login or password";
                  //  ModelState.AddModelError(string.Empty, "Login or password is incorrect.");
                    return RedirectToAction("Login",new {value=message});
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login or password is incorrect.");
                return RedirectToAction("Login");
            }
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBook(EditBookVM editVM, int Id)
        {
            editVM.ID = Id;
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
            return RedirectToAction("Index", "Book");
        }
    }

}