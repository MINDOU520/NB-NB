using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class HomeViewModels
    {
        public IEnumerable<News> New1 { get; set; }

        public IEnumerable<Posts> Posts1 { get; set; }

        public IEnumerable<Video> Video1 { get; set; }
    }
}