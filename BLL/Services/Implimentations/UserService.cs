using BLL.Services.Interfaces;
using BLL.ViewModels;
using LibraryDAL;
using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implimentations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;
        public UserService(IUnitOfWork unitOfWork, DataContext context)
        {
           
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public void Add(UserVM model)
        {
           
        
                User user = new()
                {

                    Name = model.Name,
                    Password = model.Password,
                    Login = model.Login,



                };
                _context.Users.Add(user);
                _unitOfWork.Save();
            }
     public   bool CheckGet(string password)
        {
            var user= _context.Users.FirstOrDefault(x => x.Password == password);
            return user != null?true:false;
        }
    }

    }


