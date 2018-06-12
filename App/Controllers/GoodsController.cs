using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using PagedList;
using System.Net;

namespace App.Controllers
{
    public class GoodsController : Controller
    {
        NBNBEntities db = new NBNBEntities();
        GoodsManager goodsManager = new GoodsManager();
        GoodsCommentManager goodsCommentManager = new GoodsCommentManager();

        // GET: Goods
        public ActionResult Index(string searchString)
        {
            var goods = goodsManager.GetAllGoods();
            var goodsbasketball = goodsManager.Getbasketball();
            var goodsclothes = goodsManager.Getclothes();
            var goodsshose = goodsManager.Getshose();
            var goodsprotective = goodsManager.Getprotective();
            var goodsother = goodsManager.Getother();

            if (!String.IsNullOrEmpty(searchString))
            {
                goods = goods.Where(s => s.GName.Contains(searchString));               
            }

            var index = new App.Models.GoodsViewModel
            {
                Goods1 = goods,
                Basketball = goodsbasketball,
                Clothes = goodsclothes,
                Shose = goodsshose,
                Protective = goodsprotective,
                Other = goodsother
            };

            return View(index);
        }

        public ActionResult GoodsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Goods goods = db.Goods.Find(id);
            GoodsComment goodsComments = db.GoodsComment.Find(id);

            if (goods== null)
            {
                return HttpNotFound();
            }

            var goodsComment = from m in db.GoodsComment.Where(p => p.GID == id).OrderByDescending(p => p.GCTime) select m;
            var goodsReply = from m in db.GoodsReply.OrderByDescending(r => r.GRTime) select m;
            var index = new App.Models.GoodsViewModel
            {
                Goods2 = goods,
                GoodsComment = goodsComments,
                GoodsComment1 = goodsComment,
                GoodsReply1 = goodsReply,
            };

            return View(index);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Comment(GoodsComment goodsComment)
        {
            string Commenttextarea = Request["Commenttextarea"];
            int gid = Convert.ToInt32(Request["gid"]);
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            Users users = db.Users.Find(UserID);
            int id = gid;

            if (ModelState.IsValid)
            {
                goodsComment.GID = gid;
                goodsComment.UserID = Convert.ToInt32(Session["UserID"].ToString());
                goodsComment.GCContent = Commenttextarea;
                goodsComment.GCTime = System.DateTime.Now;
                db.GoodsComment.Add(goodsComment);
                db.SaveChanges();
            }

            return RedirectToAction("GoodsDetails", "Goods", new { id });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Reply(GoodsReply goodsReply)
        {
            string Commenttextareas = Request["Commenttextareas"];
            int gid = Convert.ToInt32(Request["gid"]);
            int gcid = Convert.ToInt32(Request["gcid"]);
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            Users users = db.Users.Find(UserID);
            int id = gid;

            if (ModelState.IsValid)
            {
                goodsReply.GCID = gcid;
                goodsReply.UserID = Convert.ToInt32(Session["UserID"].ToString());
                goodsReply.GRContent = Commenttextareas;
                goodsReply.GRTime = System.DateTime.Now;
                db.GoodsReply.Add(goodsReply);
                db.SaveChanges();
            }

            return RedirectToAction("GoodsDetails", "Goods", new { id });
        }
    }
}