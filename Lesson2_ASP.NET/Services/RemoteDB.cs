using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Lesson2_ASP.NET.Models;


namespace Lesson2_ASP.NET.Services
{
    public class RemoteDB
    {
        public string CreateURL(string endpoint)
        {
            var possibleEndpoints = new string[] { "users", "posts", "comments", "todos", "address" };
            if (!possibleEndpoints.Contains(endpoint))
            {
                throw new ArgumentException($"Endpoint can`t be {endpoint}");
            }

            return @"https://5b128555d50a5c0014ef1204.mockapi.io/" + endpoint;
        }


        public static List<User> Download()
        {
            var url = @"https://5b128555d50a5c0014ef1204.mockapi.io/";

            List<User> users = null;
            List<Post> posts = null;
            List<Todo> todos = null;
            List<Comment> comments = null;
            List<Address> addresses = null;

            #region Requests
            var client = new HttpClient();

            var usersResponse = client.GetAsync(url + "users").Result;
            if (usersResponse.IsSuccessStatusCode)
            {
                string userJSON = usersResponse.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(userJSON);
            }

            var postsResponse = client.GetAsync(url + "posts").Result;
            if (postsResponse.IsSuccessStatusCode)
            {
                string postsJSON = postsResponse.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<List<Post>>(postsJSON);
            }

            var todosResponse = client.GetAsync(url + "todos").Result;
            if (todosResponse.IsSuccessStatusCode)
            {
                string todosJSON = todosResponse.Content.ReadAsStringAsync().Result;
                todos = JsonConvert.DeserializeObject<List<Todo>>(todosJSON);
            }

            var commentsResponse = client.GetAsync(url + "comments").Result;
            if (commentsResponse.IsSuccessStatusCode)
            {
                string commentsJSON = commentsResponse.Content.ReadAsStringAsync().Result;
                comments = JsonConvert.DeserializeObject<List<Comment>>(commentsJSON);
            }

            var addressesResponse = client.GetAsync(url + "address").Result;
            if (commentsResponse.IsSuccessStatusCode)
            {
                string adrressesJSON = addressesResponse.Content.ReadAsStringAsync().Result;
                addresses = JsonConvert.DeserializeObject<List<Address>>(adrressesJSON);
            }
            #endregion

            foreach (var user in users)
            {
                user.Todos = todos.Where(t => t.UserId == user.Id).ToList();
                user.Posts = posts.Where(u => u.UserId == user.Id).ToList();
                user.Addresses = addresses.Where(a => a.UserId == user.Id).ToList();

                foreach (var post in user.Posts)
                {
                    post.Comments = comments.Where(x => x.PostId == post.Id).ToList();
                }
            }

            return users;
        }
    }
}
