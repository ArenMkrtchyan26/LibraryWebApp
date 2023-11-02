using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL.Services.Interfaces
{
    public interface IBookService
    {
        void Add(BookAddEditVM model,string password);
        void Update(EditBookVM model);
        List<BookAddEditVM> GetBooksOfUser(string usPassword);
        List<BookAddEditVM> GetAll();
        void Delete(int id);
        

    }
}
