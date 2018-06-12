using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class RegisterController : Controller
    {
        NBNBEntities db = new NBNBEntities();

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users user)
        {
            HttpPostedFileBase postimageBase = Request.Files["AvatarFile"];
            var chk_member = db.Users.Where(o => o.UserName == user.UserName).FirstOrDefault();

            if (chk_member != null)
            {
                return Content("<script>;alert('该账号已经有人注册了！');history.go(-1)</script>");
            }
            try
            {
                if (postimageBase != null)
                {
                    string filePath = postimageBase.FileName;
                    string filename = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                    string serverpath = Server.MapPath(@"/Images/Users/") + filename;
                    string relativepath = @"/Images/Users/" + filename;
                    postimageBase.SaveAs(serverpath);
                    user.UserAvatar = relativepath;
                }
                else
                {
                    return Content("<script>;alert('请先上传图片！');history.go(-1)</script>");
                }

                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                    return Content("<script>;alert('注册成功!跳转登录!');window.location.href='/Login/Index'</script>");
                }
                else
                {
                    return Content("<script>;alert('注册失败！');history.go(-1)</script>");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}