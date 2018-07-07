using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lesson2_ASP.NET.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lesson2_ASP.NET.Controllers
{
    public class QueryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // GET: /query/commentnumberbyuserpost/
        public IActionResult CommentNumberByUserPosts(int id)
        {
            var query = UserService.Instance.GetCommentNumberByUserPosts(id)?.ToList();

            if(query == null || query.Count == 0)
            {
                ViewData["Success"] = "Don`t have any data";
            }

            return View(query);
        }
    }
}