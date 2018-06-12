using App.Models;
using BLL;
using Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class MatchController : Controller
    {
        NBNBEntities db = new NBNBEntities();
        MatchManager matchManager = new MatchManager();

        // GET: Match
        public ActionResult Index(int? page, string searchString)
        {
            var match = matchManager. GetMatches();

            if (!String.IsNullOrEmpty(searchString))
            {
                match = match.Where(s => s.MName.Contains(searchString));
            }

            //第几页  
            int pageNumber = page ?? 1;
            //每页显示多少条  
            int pageSize = 24;
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Match> pagedList = match.ToPagedList(pageNumber, pageSize);
            //将分页处理后的列表传给View  
            return View(pagedList);           
        }

        public ActionResult Publish()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Publish(Match match)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    match.UserID = Convert.ToInt32(Session["UserID"].ToString());
                    match.MTime = System.DateTime.Now;
                    db.Match.Add(match);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(match);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Add(Entry entry ,int? id)
        {
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            var entried = db.Entry.Where(o => o.UserID == UserID).Where(o => o.MID == id).FirstOrDefault();

            if (entried != null)
            {
                return Content("<script>;alert('您已经参加此赛事！');history.go(-1)</script>");
            }

            int currentEntryCount = db.Entry.Where(o => o.MID == id).Count();
            Match match = db.Match.Find(id);

            if (currentEntryCount >= match.MNum)
            {
                return Content("<script>;alert('抱歉！参赛人数已满！');history.go(-1)</script>");
            }

            db.Entry.Add(new Entry
            {
                UserID = Convert.ToInt32(Session["UserID"].ToString()),
                MID = id,
                ETime = DateTime.Now
            });
            db.SaveChanges();

            return Content("<script>;alert('参赛成功！');history.go(-1)</script>");
        }

        public ActionResult EntryDetails(int id)
        {
            var _match = db.Match.SingleOrDefault(x => x.MID == id);
            var _entries = db.Entry.Where(x => x.MID == id);
            var vm = new MatchViewModel
            {
                match = _match,
                entries = _entries
            };

            return View(vm);
        }        
    }
}