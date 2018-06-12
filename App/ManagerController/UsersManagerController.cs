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
    public class UsersManagerController : Controller
    {
        private NBNBEntities db = new NBNBEntities();

        // GET: UsersManager
        public ActionResult Index()
        {
            if (Session["MGID"] != null)
            {
                return View(db.Users.ToList());
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }          
        }

        // GET: UsersManager/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Users users = db.Users.Find(id);

                if (users == null)
                {
                    return HttpNotFound();
                }

                return View(users);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }
            
        }

        // GET: UsersManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Users users = db.Users.Find(id);

                if (users == null)
                {
                    return HttpNotFound();
                }

                return View(users);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }           
        }

        // POST: UsersManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,UserPassword,UserTpl,UserSex,UserAvatar,UserEmail,UserAge,UserHeight,UserWeight")] Users users)
        {
            if (Session["MGID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(users).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(users);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }           
        }

        // GET: UsersManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["MGID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Users users = db.Users.Find(id);
                if (users == null)
                {
                    return HttpNotFound();
                }

                return View(users);
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }          
        }

        // POST: UsersManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
