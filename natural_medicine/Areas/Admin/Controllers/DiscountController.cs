using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using natural_medicine.Models;


namespace natural_medicine.Areas.Admin.Controllers
{
    public class DiscountController : Controller
    {
        private MyDBContext context = new MyDBContext();
        // GET: Admin/Discount
        public ActionResult InsertDiscount()
        {
            return View();
        }
        [HttpPost]
        public JsonResult InsertDiscount(discount giamgia)
        {
            try
            {
                giamgia.create_at = DateTime.Now;
                context.discounts.Add(giamgia);
                context.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult ViewDiscount(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var model = context.discounts.OrderByDescending(x => x.end_date).ToList();
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DeleteDiscount(int id)
        {
            var obj = context.discounts.Where(x => x.id == id).FirstOrDefault();
            context.discounts.Remove(obj);
            context.SaveChanges();
            return RedirectToAction("ViewDiscount");
        }
        public ActionResult UpdateDiscount(int id)
        {
            var model = context.discounts.Where(x => x.id == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public JsonResult UpdateDiscount(discount ctg)
        {
            var model = context.discounts.Where(x => x.id == ctg.id).FirstOrDefault();
            try
            {
                model.code = ctg.code;
                model.description = ctg.description;
                model.value = ctg.value;
                model.max_value = ctg.max_value;
                model.start_date = ctg.start_date;
                model.end_date = ctg.end_date;
                context.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}