using System;
using System.Collections.Generic;
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
            ViewBag.account = account;
            return View(cart);
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
