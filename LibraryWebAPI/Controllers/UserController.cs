using BLL.Services.Implimentations;
using BLL.Services.Interfaces;
using BLL.ViewModels;
using LibraryDAL;
using LibraryDAL.Entities;
using LibraryDAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    { 
        private readonly DataContext _context;
        private readonly IUserService _userService;  
        public UserController(DataContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        [HttpPost]
        public IActionResult Index(UserVM model)
        { 
                User user = new User()
                {
                    Name = model.Name,
                    Login = model.Login,
                    Password = model.Password,

                };
                var data = _context.Users.Add(user);
                _context.SaveChanges();
                return Ok();
            }
        [HttpGet]
        public IActionResult CheckUser(string password)
        {
            bool data=_userService.CheckGet(password);
            return Ok(data);
        }
    }

    }

