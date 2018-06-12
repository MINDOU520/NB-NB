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
    public class NewsManagerController : Controller
    {
        NBNBEntities db = new NBNBEntities();
        NewsManager newsManager = new NewsManager();
        NewsCommentManager newsCommentManager = new NewsCommentManager();

        // GET: NewsManager
        public ActionResult Index(string searchString)
        {
            if (Session["MGID"] != null)
            {
                var news1 = newsManager.GetAllNews();

                if (!String.IsNullOrEmpty(searchString))
                {
                    news1 = news1.Where(s => s.NTitle.Contains(searchString));
                }

                var news = db.News.Include(n => n.NewsComment).Include(n => n.Video);

                return View(news.ToList());
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }           
        }

        // GET: NewsManager/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                News news = db.News.Find(id);

                if (news == null)
                {
                    return HttpNotFound();
                }

                return View(news);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }           
        }

        // GET: NewsManager/Create
        public ActionResult Create()
        {
            if (Session["MGID"] != null)
            {
                ViewBag.NCID = new SelectList(db.NewsComment, "NCID", "NCContent");
                ViewBag.VID = new SelectList(db.Video, "VID", "VName");

                return View();
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }           
        }

        // POST: NewsManager/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News news)
        {
            if (Session["MGID"] != null)
            {
                HttpPostedFileBase postimageBase = Request.Files["NImage"];
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            if (postimageBase != null)
                            {
                                string filePath = postimageBase.FileName;
                                string filename = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                                string serverpath = Server.MapPath(@"/Images/NImage/") + filename;
                                string relativepath = @"/Images/NImage/" + filename;
                                postimageBase.SaveAs(serverpath);
                                news.NImage = relativepath;
                            }
                            else
                            {
                                return Content("<script>;alert('请先封面上传图片！');history.go(-1)</script>");
                            }
                            if (ModelState.IsValid)
                            {
                                news.NTime = System.DateTime.Now;
                                db.News.Add(news);
                                db.SaveChanges();

                                return RedirectToAction("Index");
                            }

                            ViewBag.NCID = new SelectList(db.NewsComment, "NCID", "NCContent", news.NCID);
                            ViewBag.VID = new SelectList(db.Video, "VID", "VName", news.VID);
                        }
                    }
                    catch (Exception ex)
                    {
                        return Content(ex.Message);
                    }
                }

                return View();
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }          
        }

        // GET: NewsManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                News news = db.News.Find(id);

                if (news == null)
                {
                    return HttpNotFound();
                }
                ViewBag.NCID = new SelectList(db.NewsComment, "NCID", "NCContent", news.NCID);
                ViewBag.VID = new SelectList(db.Video, "VID", "VName", news.VID);

                return View(news);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }            
        }

        // POST: NewsManager/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NID,NTime,NTitle,NImage,NContent,NCID,NViewnum,NCommentnum,VID")] News news)
        {
            if (Session["MGID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(news).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                ViewBag.NCID = new SelectList(db.NewsComment, "NCID", "NCContent", news.NCID);
                ViewBag.VID = new SelectList(db.Video, "VID", "VName", news.VID);

                return View(news);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }           
        }

        // GET: NewsManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                News news = db.News.Find(id);

                if (news == null)
                {
                    return HttpNotFound();
                }

                return View(news);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }           
        }

        // POST: NewsManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
