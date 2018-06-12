using Models;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class LoginController : Controller
    {
        NBNBEntities db = new NBNBEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["UserAvatar"] = null;

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Users user, string returnUrl)
        {
            string ValidateCode = Request["txtverifcode"];

            if (ValidateCode != Session["ValidateCode"].ToString())
            {
                return Content("<script>;alert('验证码错误！');history.go(-1)</script>");
            }

            try
            {
                var users = db.Users.Where(o => o.UserName == user.UserName).Where(o => o.UserPassword == user.UserPassword).FirstOrDefault();
                if (users != null)
                {
                    //以下代码将权限保存到Session
                    HttpContext.Session["UserID"] = users.UserID;
                    HttpContext.Session["UserName"] = users.UserName;
                    HttpContext.Session["UserAvatar"] = users.UserAvatar;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Content("<script>;alert('该账号不存在!');history.go(-1)</script>");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}