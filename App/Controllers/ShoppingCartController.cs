using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class ShoppingCartController : Controller
    {
        NBNBEntities db = new NBNBEntities();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            var Shopping = db.ShoppingCart.Where(p => p.UserID == UserID).ToList();

            return View(Shopping);
        }

        public ActionResult Add(ShoppingCart cart, int? id)
        {
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            Goods goods = db.Goods.Find(id);

            if (goods == null)
            {
                return HttpNotFound();
            }

            var Cart = db.ShoppingCart.Where(o => o.GID == id).Where(o => o.UserID == UserID).FirstOrDefault();

            if (Cart != null)
            {
                Cart.Count++;
            }
            else
            {
                cart.Count = 1;
                cart.GID = id;
                cart.UserID = Convert.ToInt32(Session["UserID"].ToString());
                cart.SCTime = DateTime.Now;
                db.ShoppingCart.Add(cart);
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            var Cart = db.ShoppingCart.FirstOrDefault(p => p.Goods.GID == id);
            if (Cart != null)
            {
                if(Cart.Count>1)
                {
                Cart.Count--;
                }
                else
                {
                db.ShoppingCart.Remove(Cart);
                }              
                db.SaveChanges();               
            }

            return RedirectToAction("Index");
        }

        public ActionResult EmptyCart(int id )
        {
            var cartItems = db.ShoppingCart.Where(p => p.Goods.GID == id);

            foreach (var m in cartItems)
            { 
                db.ShoppingCart.Remove(m);
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateAmount(List<ShoppingCart> Carts)
        {
            foreach (var item in Carts)
            {
                var Cart = db.ShoppingCart.FirstOrDefault(p => p.Goods.GID == item.Goods.GID);
                if (Cart != null)
                {
                    Cart.Count = item.Count;
                }
            }

            return RedirectToAction("Index", "ShoppingCart");
        }

        public int GetCount()
        {
            int UserId = Convert.ToInt32(Session["UserID"].ToString());
            int count = db.ShoppingCart.Count(p => p.UserID == UserId);

            return count;
        }
    }
}
