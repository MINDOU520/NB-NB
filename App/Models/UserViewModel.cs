using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class UserViewModel
    {
        public News News { get; set; }

        public IEnumerable<News> News1 { get; set; }

        public IEnumerable<NewsComment> NewsComment1 { get; set; }

        public Users user { get;set; }

        public IEnumerable<Entry> entries { get; set; }

        public IEnumerable<Posts> posts { get; set; }

        public IEnumerable<Match> match { get; set; }
    }

    public class NewsCommentViewModel
    {
        public NewsComment NComment { get; set; }

        public string NTitle { get; set; }
    }

    public class MatchViewModel1
    {
        public Entry ety { get; set; }
        public string MName { get; set; }
    }
}