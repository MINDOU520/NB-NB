using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class VideoViewModel
    {
        public Video Video { get; set; }

        public IEnumerable<Video> Video1 { get; set; }

        public IEnumerable<VideoComment> VideoComment1 { get; set; }

        public IEnumerable<VideoReply> VideoReply1 { get; set; }

        public VideoReply VideoReply { get; set; }

        public VideoComment VideoComment { get; set; }

        public string VSavepath { get; set; }

        public string VTime { get; set; }
    }
}