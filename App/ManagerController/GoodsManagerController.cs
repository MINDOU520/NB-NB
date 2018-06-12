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
    public class GoodsManagerController : Controller
    {
        NBNBEntities db = new NBNBEntities();
        GoodsManager goodsManager = new GoodsManager();
        GoodsCommentManager goodsCommentManager = new GoodsCommentManager();

        // GET: GoodsMAnager
        public ActionResult Index(string searchString)
        {
            if (Session["MGID"] != null)
            {
                var goods = goodsManager.GetAllGoods();

                if (!String.IsNullOrEmpty(searchString))
                {
                    goods = goods.Where(s => s.GName.Contains(searchString));
                }

                return View(db.Goods.ToList());
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }           
        }

        // GET: GoodsMAnager/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Goods goods = db.Goods.Find(id);

                if (goods == null)
                {
                    return HttpNotFound();
                }

                return View(goods);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }           
        }

        // GET: GoodsMAnager/Create
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

        // POST: GoodsMAnager/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GID,GName,GPrice,GPhoto,GColor,GSize,GSaled,GBiaoshi,GDetails,GCID")] Goods goods)
        {
            if (Session["MGID"] != null)
            {
                HttpPostedFileBase postimageBase = Request.Files["GImage"];

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
                                string serverpath = Server.MapPath(@"/Images/GImage/") + filename;
                                string relativepath = @"/Images/GImage/" + filename;
                                postimageBase.SaveAs(serverpath);
                                goods.GPhoto = relativepath;
                            }
                            else
                            {
                                return Content("<script>;alert('请先上传商品图片！');history.go(-1)</script>");
                            }
                            if (ModelState.IsValid)
                            {
                                db.Goods.Add(goods);
                                db.SaveChanges();

                                return RedirectToAction("Index");
                            }
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

        // GET: GoodsMAnager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Goods goods = db.Goods.Find(id);

                if (goods == null)
                {
                    return HttpNotFound();
                }

                return View(goods);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }            
        }

        // POST: GoodsMAnager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GID,GName,GPrice,GPhoto,GColor,GSize,GSaled,GBiaoshi,GDetails,GCID")] Goods goods)
        {
            if (Session["MGID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(goods).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(goods);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }           
        }

        // GET: GoodsMAnager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Goods goods = db.Goods.Find(id);

                if (goods == null)
                {
                    return HttpNotFound();
                }

                return View(goods);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }           
        }

        // POST: GoodsMAnager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Goods goods = db.Goods.Find(id);
            db.Goods.Remove(goods);
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
