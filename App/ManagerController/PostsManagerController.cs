using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;

namespace App.ManagerController
{
    public class PostsManagerController : Controller
    {
        // private NBNBEntities db = new NBNBEntities();
        NBNBEntities db = new NBNBEntities();
        PostsManager postsManager = new PostsManager();
        PostsCommentManager postsCommentManager = new PostsCommentManager();

        // GET: PostsManager
        public ActionResult Index(string searchString)
        {
            if (Session["MGID"] != null)
            {
                var post = postsManager.GetAllPosts();
                if (!String.IsNullOrEmpty(searchString))
                {
                    post = post.Where(s => s.PTitle.Contains(searchString));
                }
                var posts = db.Posts.Include(p => p.PostsComment).Include(p => p.Users).Include(p => p.Video);

                return View(posts.ToList());
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }            
        }

        // GET: PostsManager/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Posts posts = db.Posts.Find(id);

                if (posts == null)
                {
                    return HttpNotFound();
                }

                return View(posts);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }            
        }

        // GET: PostsManager/Create
        public ActionResult Create()
        {
            if (Session["MGID"] != null)
            {
                ViewBag.PCID = new SelectList(db.PostsComment, "PCID", "PCContent");
                ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
                ViewBag.VID = new SelectList(db.Video, "VID", "VName");

                return View();
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }            
        }

        // POST: PostsManager/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PID,UserID,PTitle,PTime,PContent,PViewnum,VID,PCID,PCommentnum")] Posts posts)
        {
            if (Session["MGID"] != null)
            {
                if (ModelState.IsValid)
                {
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
                return RedirectToAction("ManagerLogin", "Manager");
            }          
        }

        // GET: PostsManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Posts posts = db.Posts.Find(id);

                if (posts == null)
                {
                    return HttpNotFound();
                }
                ViewBag.PCID = new SelectList(db.PostsComment, "PCID", "PCContent", posts.PCID);
                ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", posts.UserID);
                ViewBag.VID = new SelectList(db.Video, "VID", "VName", posts.VID);

                return View(posts);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }            
        }

        // POST: PostsManager/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PID,UserID,PTitle,PTime,PContent,PViewnum,VID,PCID,PCommentnum")] Posts posts)
        {
            if (Session["MGID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(posts).State = EntityState.Modified;
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
                return RedirectToAction("ManagerLogin", "Manager");
            }            
        }

        // GET: PostsManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Posts posts = db.Posts.Find(id);

                if (posts == null)
                {
                    return HttpNotFound();
                }

                return View(posts);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }           
        }

        // POST: PostsManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Posts posts = db.Posts.Find(id);
            db.Posts.Remove(posts);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

    }
}
