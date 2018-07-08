using Lesson2_ASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson2_ASP.NET.Services
{
    public class UserService
    {
        private static readonly Lazy<UserService> lazy = new Lazy<UserService>(() => new UserService());

        public static UserService Instance { get { return lazy.Value; } }

        public List<User> Users { get; private set; }

        private UserService()
        {
            Users = DBService.Users;
        }

        public User GetUser(int id) => Users.FirstOrDefault(u => u.Id.Equals(id));

        public Post GetPost(int id)
        {
            var post = (from u in Users
                        from p in u.Posts
                        where p.Id == id
                        select p).FirstOrDefault();

            return post;
        }

        public Todo GetTodo(int id)
        {
            var todo = (from u in Users
                        from t in u.Todos
                        where t.Id == id
                        select t).FirstOrDefault();

            return todo;
        }


        public IEnumerable<(Post Post, int CommentNumber)> GetCommentNumberByUserPosts(int userId)
        {
            return Users.FirstOrDefault(u => u.Id == userId)?.Posts.Select(p => (p, p.Comments.Count));
        }

        public IEnumerable<Comment> GetCommentsByUserPosts(int userId)
        {
            var user = GetUser(userId);

            if(user == null)
            {
                return null;
            }

            var postComments = from post in user?.Posts
                                from comment in post?.Comments
                                where comment.Body.Length < 50
                                select comment;

            return postComments;
        }

        public IEnumerable<(int Id, string Name)> GetFineshedTodosByUser(int userId)
        {
            return Users.FirstOrDefault(u => u.Id == userId)?.Todos.Where(t => t.IsComplete).Select(t => (t.Id, t.Name));
        }

        public IEnumerable<User> GetSortUsersAndTodos()
        {
            return Users.OrderBy(u => u.Name).ThenByDescending(u => u.Todos.Select(t => t.Name.Length));
        }

        public UserInfo GetUserInfo(int userId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);
            var lastPost = user?.Posts.OrderByDescending(p => p.CreatedAt).FirstOrDefault();
            var commentNumberByLastPost = lastPost?.Comments.Count;
            var unfinishedTodoNumber = user?.Todos.Where(t => !t.IsComplete).Count();
            var theMostPopularPostByComments = user?.Posts.OrderByDescending(p => p.Comments.Where(c => c.Body.Length > 80).Count()).FirstOrDefault();
            var theMostPopularPostByLikes = user?.Posts.OrderByDescending(p => p.Likes).FirstOrDefault();

            return new UserInfo(user, lastPost, commentNumberByLastPost, unfinishedTodoNumber, theMostPopularPostByComments, theMostPopularPostByLikes);
        }

        public PostInfo GetPostInfo(int postId)
        {
            var post = GetPost(postId);

            var theLongestComment = post?.Comments.OrderByDescending(c => c.Body).FirstOrDefault();
            var theMostLikedComment = post?.Comments.OrderByDescending(c => c.Likes).FirstOrDefault();
            var commentsNumber = post?.Comments.Where(c => c.Likes == 0 || c.Body.Length < 80).Count();

            return new PostInfo(post, theLongestComment, theMostLikedComment, commentsNumber);
        }
    }
}
