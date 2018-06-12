using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class PostsViewModel
    {
        public Posts Posts { get; set; }

        public IEnumerable<Posts> Posts1 { get; set; }

        public IEnumerable<PostsComment> PostsComment1 { get; set; }

        public IEnumerable<PostsReply> PostsReply1 { get; set; }

        public PostsReply PostsReply { get; set; }

        public PostsComment PostsComment { get; set; }

        public  Users user { get; set; }
    }
}