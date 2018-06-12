using App.Models;
using BLL;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class UserController : Controller
    {
        NBNBEntities db = new NBNBEntities();

        // GET: User
        public ActionResult Index()
        {
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            Users users = db.Users.Find(UserID);

            return View(users);
        }

        public ActionResult MyComment()
        {
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            var ncomments = db.NewsComment.Where(p => p.UserID == UserID).ToList();
            IList<NewsCommentViewModel> vmlist = new List<NewsCommentViewModel>();

            foreach (var item in ncomments)
            {
                string ntitle = db.News.Where(x => x.NID == item.NID).Select(x => x.NTitle).SingleOrDefault();
                vmlist.Add(new NewsCommentViewModel
                {
                    NComment = item,
                    NTitle = ntitle
                });
            }

            return View(vmlist);
        }

        public ActionResult DelectComment(int id)
        {
            NewsComment newsComment = db.NewsComment.Find(id);

            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            var delc = db.NewsComment.Where(c => c.NCID == id).Where(c => c.UserID == UserID).SingleOrDefault();
            db.NewsComment.Remove(newsComment);
            db.SaveChanges();

            return RedirectToAction("MyComment");
        }

        public ActionResult MyMatch()
        {
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            var entry = db.Entry.Where(p => p.UserID == UserID).ToList();
            IList<MatchViewModel1> mlist = new List<MatchViewModel1>();

            foreach (var item in entry)
            {
                string mname = db.Match.Where(m => m.MID == item.MID).Select(m => m.MName).SingleOrDefault();
                mlist.Add(new MatchViewModel1
                {
                    ety = item,
                    MName = mname,
                });
            }

            return View(mlist);
        }

        public ActionResult MyPosts(int? id)
        {
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            Posts posts = db.Posts.Find(id);
            var p = db.Posts.Where(m => m.UserID == UserID).OrderByDescending(n => n.PTime).ToList();
            var index = new Models.UserViewModel
            {
                posts = p,
            };

            return View(index);
        }

        public ActionResult Delect(int id)
        {
            Posts posts = db.Posts.Find(id);
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            var del = db.Posts.Where(p => p.PID == id).Where(p => p.UserID == UserID).SingleOrDefault();
            db.Posts.Remove(posts);
            db.SaveChanges();

            return RedirectToAction("MyPosts");
        }

        public ActionResult Changeinfo()
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(Session["UserID"]);

            if (users == null)
            {
                return HttpNotFound();
            }

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Changeinfo([Bind(Include = "UserName,UserTpl,UserSex,UserEmail,UserAge,UserHeight,UserWeight")] Users users)
        {
            if (TryUpdateModel(users, new string[] { "UserName", "UserEmail", "UserTpl", "UserSex", "UserAge", "UserHeight", "UserWeight" }))
            {
                if (ModelState.IsValid)
                {
                    UpdateModel(users);
                    db.SaveChanges();
                }
            }
            else ModelState.AddModelError("", "更新模型数据失败");
            return View("Index", users);
        }
    }
}