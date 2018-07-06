using System;

namespace Lesson2_ASP.NET.Models
{
    public class Todo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsComplete { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
