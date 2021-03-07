using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using natural_medicine.Models;
using natural_medicine.Areas.Admin.Security;

namespace natural_medicine.Areas.Admin.Controllers
{
    public class PublishersController : Controller
    {
        // GET: Admin/Publishers
        private MyDBContext context = new MyDBContext();

        [CustomAuthorize]
        public ActionResult InsertPublisher()
        {
            return View();
        }
        [HttpPost]
        public JsonResult InsertPublisher(publisher ncungcap)
        {
            try
            {
                context.publishers.Add(ncungcap);
                context.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }

        }
        [CustomAuthorize]
        public ActionResult ViewPublisher(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var model = context.publishers.ToList();
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        [CustomAuthorize]
        public ActionResult DeletePublisher(int id)
        {
            var obj = context.publishers.Where(x => x.id == id).FirstOrDefault();
            context.publishers.Remove(obj);
            context.SaveChanges();
            return RedirectToAction("ViewPublisher");
        }
        [CustomAuthorize]
        public ActionResult UpdatePublisher(int id)
        {
            var model = context.publishers.Where(x => x.id == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public JsonResult UpdatePublisher(publisher ctg)
        {
            var model = context.publishers.Where(x => x.id == ctg.id).FirstOrDefault();
            try
            {
                model.name = ctg.name;
                model.phone = ctg.phone;
                model.address = ctg.address;
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