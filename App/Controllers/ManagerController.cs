using Models;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{    
    public class ManagerController : Controller
    {
        NBNBEntities db = new NBNBEntities();

        // GET: Manager
        public ActionResult Index()
        {
            if(Session["MGID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ManagerLogin", "Manager");
            }
        }

        [AllowAnonymous]
        public ActionResult ManagerLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ManagerLogin(Manager manager, string returnUrl)
        {
            string ValidateCode = Request["txtverifcode"];

            if (ValidateCode != Session["ValidateCode"].ToString())
            {
                return Content("<script>;alert('验证码错误！');history.go(-1)</script>");
            }

            try
            {
                var managers = db.Manager.Where(o => o.MGName == manager.MGName).Where(o => o.MGPassword == manager.MGPassword).FirstOrDefault();

                if (managers != null)
                {
                    //以下代码将权限保存到Session
                    HttpContext.Session["MGID"] = managers.MGID;
                    HttpContext.Session["MGName"] = managers.MGName;
                    return RedirectToAction("Index", "Manager");
                }
                else
                {
                    return Content("<script>;alert('该管理员不存在!');history.go(-1)</script>");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}