using BLL;
using Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class NewsController : Controller
    {
        NBNBEntities db = new NBNBEntities();
        NewsManager newsManager = new NewsManager();
        NewsCommentManager newsCommentManager = new NewsCommentManager();

        // GET: News
        public ActionResult Index(int? page, string searchString)
        {
            var news = newsManager.GetAllNews();

            if (!String.IsNullOrEmpty(searchString))
            {
                news = news.Where(s => s.NTitle.Contains(searchString));
            }

            //第几页  
            int pageNumber = page ?? 1;
            //每页显示多少条  
            int pageSize = 10;
            //通过ToPagedList扩展方法进行分页  
            IPagedList<News> pagedList = news.ToPagedList(pageNumber, pageSize);
            //将分页处理后的列表传给View  
            return View(pagedList);
        }

        /// <summary>
        /// 详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            News news = db.News.Find(id);
            NewsComment newsComments = db.NewsComment.Find(id);

            if (news == null)
            {
                return HttpNotFound();
            }

            var newsComment = from m in db.NewsComment.Where(p => p.NID == id).OrderByDescending(p => p.NCTime) select m;
            var newsReply = from m in db.NewsReply.OrderByDescending(r => r.NRTime) select m;
            var index = new App.Models.NewsViewModel()
            {
                News = news,
                NewsComment = newsComments,
                NewsComment1 = newsComment,
                NewsReply1 = newsReply,
            };

            return View(index);
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Comment(NewsComment newsComment)
        {
            string Commenttextarea = Request["Commenttextarea"];
            int nid = Convert.ToInt32(Request["nid"]);
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            Users users = db.Users.Find(UserID);
            int id = nid;

            if (ModelState.IsValid)
            {
                newsComment.NID = nid;
                newsComment.UserID = Convert.ToInt32(Session["UserID"].ToString());
                newsComment.NCContent = Commenttextarea;
                newsComment.NCTime = System.DateTime.Now;
                db.NewsComment.Add(newsComment);
                db.SaveChanges();
            }          

            return RedirectToAction("Details", "News", new {id});
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Reply(NewsReply newsReply)
        {
            string Commenttextareas = Request["Commenttextareas"];
            int nid = Convert.ToInt32(Request["nid"]);
            int ncid = Convert.ToInt32(Request["ncid"]);
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            Users users = db.Users.Find(UserID);
            int id = nid;

            if (ModelState.IsValid)
            {
                newsReply.NCID = ncid;
                newsReply.UserID = Convert.ToInt32(Session["UserID"].ToString());
                newsReply.NRContent = Commenttextareas;
                newsReply.NRTime = System.DateTime.Now;
                db.NewsReply.Add(newsReply);
                db.SaveChanges();
            }

            return RedirectToAction("Details", "News", new { id});
        }
    }
}