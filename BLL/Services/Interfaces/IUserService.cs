using BLL.ViewModels;
using LibraryDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        void Add(UserVM model);
        bool CheckGet(string password);
    }
}
