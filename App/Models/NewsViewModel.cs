using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class NewsViewModel
    {
        public News News{get;set;}

        public IEnumerable<News> News1 { get; set; }

        public IEnumerable<NewsComment>NewsComment1 { get; set; }

        public IEnumerable<NewsReply> NewsReply1 { get; set; }

        public NewsReply NewsReply { get; set; }

        public NewsComment NewsComment { get; set; }
    }
}