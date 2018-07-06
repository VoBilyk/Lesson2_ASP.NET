using System;
using System.Collections.Generic;

namespace Lesson2_ASP.NET.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<Post> Posts { get; set; }

        public List<Todo> Todos { get; set; }

        public List<Address> Addresses { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
