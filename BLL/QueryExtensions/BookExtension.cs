using BLL.ViewModels;
using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.QueryExtensions
{
    public static class BookExtension
    {
        public static List<BookAddEditVM> GetByPassword(this DbSet<Book> db, string usPassword)
        {
            var data = db.Where(b => b.User.Password == usPassword).Select(b => new BookAddEditVM()
            {
                Id = b.Id,
                Name = b.Name,
                AuthorName=b.AuthorName,
                Price=b.Price,
                Quantity=b.Quantity,
                IsIssued=b.IsIssued,               
            }).AsNoTracking().ToList();
            return data;
        }
        public static List<BookAddEditVM> GetAllBooks(this DbSet<Book> db)
        {
            var books = db.Select(b => new BookAddEditVM()
            {
                Id = b.Id,
                Name = b.Name,
                AuthorName = b.AuthorName,
                Price = b.Price,
                Quantity = b.Quantity,
                IsIssued = b.IsIssued,
                Upload=b.ImagePath,
            }).AsNoTracking().ToList();
            return books;
        }

    }
}
