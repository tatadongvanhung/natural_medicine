using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using natural_medicine.Models;

namespace natural_medicine.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Admin/Orders
        private MyDBContext context = new MyDBContext();
        public ActionResult ViewOrder(DateTime ?start_date, DateTime ?end_date, int ?page)
        {
            if (page == null) page = 1;
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            if (start_date != null && end_date != null)
            {
                var model = context.VIEW_ORDER.OrderByDescending(x => x.create_at)
                .Where(x => x.create_at > start_date
                        && x.create_at < end_date).ToList();
                return View(model.ToPagedList(pageNumber, pageSize));
            }
            else{
                var model = context.VIEW_ORDER.OrderByDescending(x => x.create_at).ToList();
                return View(model.ToPagedList(pageNumber, pageSize));
            }
        }
    }
}