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
    public class PostsController : Controller
    {
        NBNBEntities db = new NBNBEntities();
        PostsManager postsManager = new PostsManager();
        PostsCommentManager postsCommentManager = new PostsCommentManager();

        // GET: News
        public ActionResult Index(int? page, string searchString)
        {
            var posts = postsManager.GetAllPosts();

            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.PTitle.Contains(searchString));
            }
            //第几页  
            int pageNumber = page ?? 1;
            //每页显示多少条  
            int pageSize = 10;
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Posts> pagedList = posts.ToPagedList(pageNumber, pageSize);
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

            Posts posts = db.Posts.Find(id);
            PostsComment postsComments = db.PostsComment.Find(id);
      
            if (posts == null)
            {
                return HttpNotFound();
            }

            var postsComment = from m in db.PostsComment.Where(p => p.PID == id).OrderByDescending(p => p.PCTime) select m;
            var postsReply = from m in db.PostsReply.OrderByDescending(r => r.PRTime) select m;
            var index = new App.Models.PostsViewModel()
            {
                Posts = posts,
                PostsComment = postsComments,
                PostsComment1 = postsComment,
                PostsReply1 = postsReply,
            };

            return View(index);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Comment(PostsComment postsComment)
        {
            string Commenttextarea = Request["Commenttextarea"];
            int pid = Convert.ToInt32(Request["pid"]);
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            Users users = db.Users.Find(UserID);
            int id = pid;

            if (ModelState.IsValid)
            {
                postsComment.PID = pid;
                postsComment.UserID = Convert.ToInt32(Session["UserID"].ToString());
                postsComment.PCContent = Commenttextarea;
                postsComment.PCTime = System.DateTime.Now;
                db.PostsComment.Add(postsComment);
                db.SaveChanges();
            }

            return RedirectToAction("Details", "Posts", new { id });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Reply(PostsReply postsReply)
        {
            string Commenttextareas = Request["Commenttextareas"];
            int pid = Convert.ToInt32(Request["pid"]);
            int pcid = Convert.ToInt32(Request["pcid"]);
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            Users users = db.Users.Find(UserID);
            int id = pid;

            if (ModelState.IsValid)
            {
                postsReply.PCID = pcid;
                postsReply.UserID = Convert.ToInt32(Session["UserID"].ToString());
                postsReply.PRContent = Commenttextareas;
                postsReply.PRTime = System.DateTime.Now;
                db.PostsReply.Add(postsReply);
                db.SaveChanges();
            }

            return RedirectToAction("Details", "Posts", new { id });
        }

        // GET: Posts/Create
        public ActionResult AddPosts()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.PCID = new SelectList(db.PostsComment, "PCID", "PCContent");
                ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
                ViewBag.VID = new SelectList(db.Video, "VID", "VName");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddPosts([Bind(Include = "PID,UserID,PTitle,PTime,PContent,PViewnum,VID,PCID,PCommentnum")] Posts posts)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    posts.UserID = Convert.ToInt32(Session["UserID"].ToString());
                    posts.PTime = System.DateTime.Now;
                    db.Posts.Add(posts);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                ViewBag.PCID = new SelectList(db.PostsComment, "PCID", "PCContent", posts.PCID);
                ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", posts.UserID);
                ViewBag.VID = new SelectList(db.Video, "VID", "VName", posts.VID);

                return View(posts);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}