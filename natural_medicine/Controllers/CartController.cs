using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using natural_medicine.Models;
using natural_medicine.Models.CartModel;

namespace natural_medicine.Controllers
{
    public class CartController : Controller
    {
        private MyDBContext context = new MyDBContext();
        public ActionResult MyCart()
        {
            var cart = (Cart)Session["CartSession"];
            if (cart == null)
            {
                cart = new Cart();
            }
            return View(cart);
        }

        public ActionResult AddItem(int id, string returnURL)
        {
            var product = context.products.Where(x => x.id == id).FirstOrDefault();
            var cart = (Cart)Session["CartSession"];
            if (cart != null)
            {
                cart.AddItem(product);
                Session["CartSession"] = cart;
            }
            else
            {
                cart = new Cart();
                cart.AddItem(product);
                Session["CartSession"] = cart;
            }
            //if (string.IsNullOrEmpty(returnURL))
            //{
            //    return Redirect(Request.UrlReferrer.ToString());
            //}
            //return Redirect(Request.UrlReferrer.ToString());
            //var result = new { Success = "True", Message = "Error Message" };
            //var result = new { Success = "True"};
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveLine(int id)
        {
            var product = context.products.Where(x => x.id == id).FirstOrDefault();
            var cart = (Cart)Session["CartSession"];
            if (cart != null)
            {
                cart.RemoveLine(product);
                if (cart.Lines.Count() == 0)
                {
                    cart = null;
                }
                Session["CartSession"] = cart;
            }
            return RedirectToAction("mycart");
        }

        public ActionResult UpdateCart(int[] masp, int[] qty)
        {
            var cart = (Cart)Session["CartSession"];

            if (cart != null)
            {
                for (int i = 0; i < masp.Count(); i++)
                {
                    var product = context.products.Find(masp[i]);
                    cart.UpdateItem(product, qty[i]);
                }

                Session["CartSession"] = cart;
            }
            return RedirectToAction("mycart");
        }
        public ActionResult Payment()
        {
            var account = (user)Session["loginClient"];
            var cart = (Cart)Session["CartSession"];
            if (account == null)
            {
                return RedirectToAction("mycart", "cart");
            }
            if (cart == null)
            {
                cart = new Cart();
            }
            var listaddress = context.addresses.Where(x => x.user_id == account.id).ToList();
            var payment_methods = context.payment_methods.ToList();
            ViewBag.account = account;
            ViewBag.listaddress = listaddress;
            ViewBag.payment_methods = payment_methods;
            return View(cart);
        }
        [HttpPost]
        public ActionResult Payment(order hoadon)
        {
            using (DbContextTransaction dbTran = context.Database.BeginTransaction())
            {
                try
                {
                    hoadon.status_id = 1;
                    hoadon.create_at = DateTime.Now;
                    context.orders.Add(hoadon);
                    context.SaveChanges();

                    var order_id_max = context.orders.Max(x => x.id);

                    var cart = (Cart)Session["CartSession"];

                    foreach (var item in cart.Lines)
                    {
                        orders_detail detail = new orders_detail();
                        detail.order_id = order_id_max;
                        detail.product_id = item.product.id;
                        detail.price = item.product.price;
                        detail.quantity = item.quantity;
                        context.orders_detail.Add(detail);
                        context.SaveChanges();
                    }
                    dbTran.Commit();
                    cart.Clear();
                    Session["CartSession"] = cart;
                }
                catch
                {
                    dbTran.Rollback();
                    return RedirectToAction("Error");
                }
            }
            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
        [HttpPost]
        public JsonResult discount(discount gg)
        {
            var discount = context.discounts.Where(x => x.code.Trim() == gg.code).FirstOrDefault();
            if (discount != null)
            {
                if (discount.start_date < DateTime.Now.Date && discount.end_date > DateTime.Now.Date)
                {
                    // Nếu code còn hạn sử dụng
                    var result = new { success = "true", code = discount.code, 
                        error = "false",
                        value = discount.value,
                        max_value = discount.max_value };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new
                    {
                        success = "false",
                        code = "",
                        error = "true",
                        value = 0,
                        max_value = 0
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            } else
            {
                var result = new
                {
                    success = "false",
                    code = "",
                    error = "true",
                    value = 0,
                    max_value = 0
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
