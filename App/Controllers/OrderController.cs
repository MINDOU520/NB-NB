using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class OrderController : Controller
    {
        NBNBEntities db = new NBNBEntities();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Finish(string type, int id)
        {
            IList<Goods> goods = new List<Goods>();

            if (type.Equals("cart", StringComparison.OrdinalIgnoreCase))
            {
                var good_ids = db.ShoppingCart.Where(x => x.SCID == id).Select(x => x.GID).ToList();
                goods = db.Goods.Where(x => good_ids.Contains(x.GID)).ToList();
            }
            else if (type.Equals("goods", StringComparison.OrdinalIgnoreCase))
            {

            }

            return View(goods);
        }

        [HttpPost]
        public ActionResult Finish(Orders orders,int id)
        {

            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            Goods goods = db.Goods.Find(id);

            if (goods == null)
            {
                return HttpNotFound();
            }

            var cart = db.ShoppingCart.Where(o => o.GID == id).Where(o => o.UserID == UserID);

            if (cart !=null)
            {
            orders.GID = id;
            orders.UserID = Convert.ToInt32(Session["UserID"].ToString());
            orders.OTime= DateTime.Now;
            db.Orders.Add(orders);
            db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
           
            return Content("<script>;alert('恭喜您，购买成功！');window.location.href='/Home/Index'</script>");
        }
    }
}