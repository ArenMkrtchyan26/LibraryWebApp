using BLL.QueryExtensions;
using BLL.Services.Interfaces;
using BLL.ViewModels;
using LibraryDAL;
using LibraryDAL.Entities;
using LibraryDAL.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implimentations
{
    public class BookService : IBookService
    {
      //  private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
       private readonly DataContext _context;
        public BookService(IUnitOfWork unitOfWork,DataContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public void Add(BookAddEditVM model,string pass)
        {

           Book book = new()
            {
               
                Name = model.Name,  
                AuthorName = model.AuthorName,
                IsIssued = model.IsIssued,                  
                User=_context.Users.FirstOrDefault(us=>us.Password==pass),
                UserId= _context.Users.FirstOrDefault(us => us.Password == pass).Id,
                Price = model.Price,
                ImagePath = model.Upload,
                Quantity=model.Quantity,
              


            };
            _context.Books.Add(book);
            _unitOfWork.Save();
        }
        public void Delete(int Id)
        {
            var book = _context.Books.FirstOrDefault(us=>us.Id==Id);
            _context.Books.Remove(book);
            _unitOfWork.Save();
        }
        public List<BookAddEditVM> GetBooksOfUser(string usPassword)
        {
            var data = _context.Books.GetByPassword(usPassword);
            return data;
        }
        public List<BookAddEditVM> GetAll()
        {
           return _context.Books.GetAllBooks(); 
        }
        public void Update(EditBookVM model)
        {
            var book = _context.Books.FirstOrDefault(b=> b.Id == model.ID);

            book.Price = model.Price;
            book.Quantity = model.Quantity;
            _context.Books.Update(book);
            _unitOfWork.Save();
        }
    }
}
