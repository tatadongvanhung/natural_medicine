using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using natural_medicine.Models;

namespace natural_medicine.Controllers
{
    public class MyOrderController : Controller
    {
        // GET: MyOrder
        private MyDBContext context = new MyDBContext();

        public ActionResult ListOrder()
        {
            var account = (user)Session["loginClient"];
            List<VIEW_ORDER> model = new List<VIEW_ORDER>();
            if(account != null)
            {
                model = context.VIEW_ORDER.Where(x => x.user_id == account.id).OrderByDescending(x => x.id).ToList();
            }
            return View(model);
        }

        public ActionResult OrderDetail(int id)
        {
            var account = (user)Session["loginClient"];
            List<VIEW_ORDER_DETAIL> model = new List<VIEW_ORDER_DETAIL>();
            if (account != null)
            {
                model = context.VIEW_ORDER_DETAIL.Where(x => x.order_id == id).ToList();
            }
            else
            {
                return RedirectToAction("ListOrder");
            }
            return View(model);
        }
    }
}
