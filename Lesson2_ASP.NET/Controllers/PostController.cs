using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lesson2_ASP.NET.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lesson2_ASP.NET.Controllers
{
    public class PostController : Controller
    {
        private UserService userService;

        public PostController()
        {
            userService = UserService.Instance;
        }

        public IActionResult Index(int id)
        {
            return View(userService.GetPost(id));
        }
    }
}