using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lesson2_ASP.NET.Services;

namespace Lesson2_ASP.NET.Controllers
{
    public class UserController : Controller
    {
        private UserService userService;

        public UserController()
        {
            userService = UserService.Instance;
        }

        public IActionResult Index(int id)
        {
            return View(userService.GetUser(id));
        }
    }
}