﻿namespace Lesson2_ASP.NET.Models
{
    public struct UserInfo
    {
        public User User { get; set; }

        public Post LastPost { get; set; }

        public int? CommentNumberByLastPost { get; set; }

        public int? UnfinishedTodoNumber { get; set; }

        public Post TheMostPopularPostByComments { get; set; }

        public Post TheMostPopularPostByLikes { get; set; }


        public UserInfo(User user, Post lastPost, int? commentNumberByLastPost, int? unfinishedTodoNumber, Post theMostPopularPostByComments, Post theMostPopularPostByLikes)
        {
            User = user;
            LastPost = lastPost;
            CommentNumberByLastPost = commentNumberByLastPost;
            UnfinishedTodoNumber = unfinishedTodoNumber;
            TheMostPopularPostByComments = theMostPopularPostByComments;
            TheMostPopularPostByLikes = theMostPopularPostByLikes;
        }
    }
}
