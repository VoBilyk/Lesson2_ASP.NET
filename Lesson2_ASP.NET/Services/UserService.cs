﻿using Lesson2_ASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson2_ASP.NET.Services
{
    public class UserService
    {
        private static readonly Lazy<UserService> lazy = new Lazy<UserService>(() => new UserService());

        public static UserService Instance { get { return lazy.Value; } }

        private readonly List<User> users;

        private UserService()
        {
            users = DB.Users;
        }


        public IEnumerable<(Post Post, int CommentNumber)> GetCommentNumberByUserPosts(int userId)
        {
            return users.FirstOrDefault(u => u.Id == userId)?.Posts.Select(p => (p, p.Comments.Count));
        }

        public IEnumerable<Comment> GetCommentsByUserPosts(int userId)
        {
            var postComments = from post in users.FirstOrDefault(u => u.Id == userId)?.Posts
                               from comment in post.Comments
                               where comment.Body.Length < 50
                               select comment;

            return postComments;
        }

        public IEnumerable<(int Id, string Name)> GetFineshedTodosByUser(int userId)
        {
            return users.FirstOrDefault(u => u.Id == userId)?.Todos.Where(t => t.IsComplete).Select(t => (t.Id, t.Name));
        }

        public IEnumerable<User> GetSortUsersAndTodos()
        {
            return users.OrderBy(u => u.Name).ThenByDescending(u => u.Todos.Select(t => t.Name.Length));
        }

        public UserInfo GetUserInfo(int userId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            var lastPost = user?.Posts.OrderByDescending(p => p.CreatedAt).FirstOrDefault();
            var commentNumberByLastPost = lastPost?.Comments.Count;
            var unfinishedTodoNumber = user?.Todos.Where(t => !t.IsComplete).Count();
            var theMostPopularPostByComments = user?.Posts.OrderByDescending(p => p.Comments.Where(c => c.Body.Length > 80).Count()).FirstOrDefault();
            var theMostPopularPostByLikes = user?.Posts.OrderByDescending(p => p.Likes).FirstOrDefault();

            return new UserInfo(user, lastPost, commentNumberByLastPost, unfinishedTodoNumber, theMostPopularPostByComments, theMostPopularPostByLikes);
        }

        public PostInfo GetPostInfo(int postId)
        {
            var post = (from u in users
                        from p in u.Posts
                        where p.Id == postId
                        select p).FirstOrDefault();

            var theLongestComment = post?.Comments.OrderByDescending(c => c.Body).FirstOrDefault();
            var theMostLikedComment = post?.Comments.OrderByDescending(c => c.Likes).FirstOrDefault();
            var commentsNumber = post?.Comments.Where(c => c.Likes == 0 || c.Body.Length < 80).Count();

            return new PostInfo(post, theLongestComment, theMostLikedComment, commentsNumber);
        }
    }
}
