﻿using System;
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

        // GET: /query/postsByUser/
        public IActionResult PostsByUser(int id)
        {
            var query = UserService.Instance.GetCommentNumberByUserPosts(id)?.ToList();

            if(query == null || query.Count == 0)
            {
                ViewData["Success"] = "Don`t have any data";
            }

            return View(query);
        }


        // GET: /query/commentsByUser/
        public IActionResult CommentsByUser(int id)
        {
            var query = UserService.Instance.GetCommentsByUserPosts(id)?.ToList();

            if (query == null || query.Count == 0)
            {
                ViewData["Success"] = "Don`t have any data";
            }

            return View(query);
        }


        // GET: /query/finishedTodosByUser/
        public IActionResult FinishedTodosByUser(int id)
        {
            var query = UserService.Instance.GetFineshedTodosByUser(id)?.ToList();

            if (query == null || query.Count == 0)
            {
                ViewData["Success"] = "Don`t have any todos";
            }

            return View(query);
        }

        // GET: /query/sortUsers/
        public IActionResult SortUsers()
        {
            var query = UserService.Instance.GetSortUsersAndTodos()?.ToList();

            return View(query);
        }

        // GET: /query/userInfo/
        public IActionResult UserInfo(int id)
        {
            var query = UserService.Instance.GetUserInfo(id);

            return View(query);
        }

        // GET: /query/postInfo/
        public IActionResult PostInfo(int id)
        {
            var query = UserService.Instance.GetPostInfo(id);

            return View(query);
        }
    }
}