using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lesson2_ASP.NET.Models;
using Lesson2_ASP.NET.Services;


namespace Lesson2_ASP.NET.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(UserService.Instance.Users);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
