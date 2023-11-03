using BLL.Services.Implimentations;
using BLL.Services.Interfaces;
using BLL.ViewModels;
using LibraryDAL;
using LibraryDAL.Entities;
using LibraryDAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;
     
        public BookController(IBookService service)
        {
            _service=service;
        }
        [HttpPost]
        public IActionResult Index(BookAddEditVM bookVM,string pass)
        {
            _service.Add(bookVM,pass);
       
            return Ok();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
          var data=_service.GetAll();

            return Ok(data);
        }
        [HttpGet]
        public IActionResult GetUserBooks(string password)
        {
            var data = _service.GetBooksOfUser(password);

            return Ok(data);
        }
        [HttpPut]
        public void Update(EditBookVM bookEditVM)
        {
            _service.Update(bookEditVM);
        }
        [HttpDelete]
        public void DeleteBook(int Id)
        {
            _service.Delete(Id);
        }
      
    }
}
