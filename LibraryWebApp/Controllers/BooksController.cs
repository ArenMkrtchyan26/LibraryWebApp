using LibraryDAL;
using LibraryDAL.Entities;
using LibraryDAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        //private readonly IBookService _bookService;
        private readonly DataContext _context;
        public BooksController(/*IBookService bookService,*/ DataContext context)
        {
          //  _bookService = bookService;
            _context = context;
        }
        [HttpPost("Addbook")]
        public IActionResult Index(Book book)
        {
            var data = _context.Books.Add(book);
            return Ok(data);
        }
    }
}
