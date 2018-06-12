using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace App.ManagerController
{
    public class VideosManagerController : Controller
    {
        private NBNBEntities db = new NBNBEntities();

        // GET: VideosManager
        public ActionResult Index()
        {
            if (Session["MGID"] != null)
            {
                return View(db.Video.ToList());
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }           
        }

        // GET: VideosManager/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Video video = db.Video.Find(id);

                if (video == null)
                {
                    return HttpNotFound();
                }

                return View(video);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }
            
        }

        // GET: VideosManager/Create
        public ActionResult Create()
        {
            if (Session["MGID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }
        }

        // POST: VideosManager/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Video video)
        {
            if (Session["MGID"] != null)
            {
                HttpPostedFileBase postimageBase = Request.Files["Vimage"];
                HttpPostedFileBase postvideoBase = Request.Files["VideoFile"];

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
                                string serverpath = Server.MapPath(@"/Images/VSrcimg/") + filename;
                                string relativepath = @"/Images/VSrcimg/" + filename;
                                postimageBase.SaveAs(serverpath);
                                video.VSrcimg = relativepath;
                            }
                            else
                            {
                                return Content("<script>;alert('请先封面上传图片！');history.go(-1)</script>");
                            }
                            if (postvideoBase != null)
                            {
                                string filePath = postvideoBase.FileName;
                                string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                                string serverpath = Server.MapPath(@"/Video/") + fileName;
                                string relativepath = @"/Video/" + fileName;
                                postvideoBase.SaveAs(serverpath);
                                video.VSavepath = relativepath;
                            }
                            else
                            {
                                return Content("<script>;alert('视频没有上传！');history.go(-1)</script>");
                            }
                            video.VTime = System.DateTime.Now;
                            db.Video.Add(video);
                            db.SaveChanges();

                            return Content("<script>;alert('上传成功!');window.location.href='/VideosManager/Index'</script>");
                        }
                        else
                        {
                            return Content("<script>;alert('添加失败！');history.go(-1)</script>");
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

        // GET: VideosManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Video video = db.Video.Find(id);

                if (video == null)
                {
                    return HttpNotFound();
                }

                return View(video);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }
            
        }

        // POST: VideosManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VID,VName,VTime,VSavepath,VClicknum,VDownloadnum,VSrcimg")] Video video)
        {
            if (Session["MGID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(video).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(video);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }            
        }

        // GET: VideosManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Video video = db.Video.Find(id);

                if (video == null)
                {
                    return HttpNotFound();
                }

                return View(video);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }            
        }

        // POST: VideosManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Video video = db.Video.Find(id);
            db.Video.Remove(video);
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