using BLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        NBNBEntities db = new NBNBEntities();
        NewsManager newsManager = new NewsManager();
        PostsManager postsManager = new PostsManager();
        VideosManager videosManager = new VideosManager();

        public ActionResult Index()
        {
            var news = newsManager.GetNews();
            var posts = postsManager.GetPosts();
            var videos = videosManager.GetVideos();
            var index = new App.Models.HomeViewModels()
            {
                New1 = news,
                Posts1 = posts,
                Video1 = videos,
            };

            return View(index);
        }
    }
}