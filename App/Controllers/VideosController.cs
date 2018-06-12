using BLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using PagedList;
using System.Net;

namespace App.Controllers
{
    public class VideosController : Controller
    {
        NBNBEntities db = new NBNBEntities();
        VideosManager videosManager = new VideosManager();
        VideoCommentManager videoCommentManager = new VideoCommentManager();

        // GET: Videos
        public ActionResult Index(int? page, string searchString)
        {
            var videos = videosManager.GetAllVideos();

            if (!String.IsNullOrEmpty(searchString))
            {
                videos = videos.Where(s => s.VName.Contains(searchString));
            }
            //第几页  
            int pageNumber = page ?? 1;
            //每页显示多少条  
            int pageSize = 24;
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Video> pagedList = videos.ToPagedList(pageNumber, pageSize);
            //将分页处理后的列表传给View  
            return View(pagedList);
        }

        // GET: Videos/AddVideos
        public ActionResult AddVideos()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: VideosManager/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddVideos(Video video)
        {
            if (Session["UserID"] != null)
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
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult PlayVideo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Video video = db.Video.Find(id);
            VideoComment videosComments = db.VideoComment.Find(id);

            if (video == null)
            {
                return HttpNotFound();
            }

            var videosComment = from m in db.VideoComment.Where(p => p.VID == id).OrderByDescending(p => p.VCTime) select m;
            var videosReply = from m in db.VideoReply.OrderByDescending(r => r.VRTime) select m;
            var index = new App.Models.VideoViewModel()
            {
                Video = video,
                VideoComment = videosComments,
                VideoComment1 = videosComment,
                VideoReply1 = videosReply,
            };

            return View(index);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Comment(VideoComment videoComment)
        {
            string Commenttextarea = Request["Commenttextarea"];
            int vid = Convert.ToInt32(Request["vid"]);
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            Users users = db.Users.Find(UserID);
            int id = vid;

            if (ModelState.IsValid)
            {
                videoComment.VID = vid;
                videoComment.UserID = Convert.ToInt32(Session["UserID"].ToString());
                videoComment.VCContent = Commenttextarea;
                videoComment.VCTime = System.DateTime.Now;
                db.VideoComment.Add(videoComment);
                db.SaveChanges();
            }

            return RedirectToAction("PlayVideo", "Videos", new { id });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Reply(VideoReply videoReply)
        {
            string Commenttextareas = Request["Commenttextareas"];
            int vid = Convert.ToInt32(Request["vid"]);
            int vcid = Convert.ToInt32(Request["vcid"]);
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            Users users = db.Users.Find(UserID);
            int id = vid;

            if (ModelState.IsValid)
            {
                videoReply.VCID = vcid;
                videoReply.UserID = Convert.ToInt32(Session["UserID"].ToString());
                videoReply.VRContent = Commenttextareas;
                videoReply.VRTime = System.DateTime.Now;
                db.VideoReply.Add(videoReply);
                db.SaveChanges();
            }

            return RedirectToAction("PlayVideo", "Videos", new { id });
        }
    }
}