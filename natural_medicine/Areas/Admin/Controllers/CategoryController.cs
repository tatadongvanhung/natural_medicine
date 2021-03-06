using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using natural_medicine.Models;
using PagedList;

namespace natural_medicine.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private MyDBContext context = new MyDBContext();
        // GET: Admin/Category
        public ActionResult InsertCategory()
        {
            return View();
        }
        [HttpPost]
        public JsonResult InsertCategory(category theloai)
        {
            try
            {
                context.categories.Add(theloai);
                context.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
           
        }
        public ActionResult ViewCategory(int ?page)
        {
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var model = context.categories.ToList();
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DeleteCategory(int id)
        {
            var obj = context.categories.Where(x => x.id == id).FirstOrDefault();
            context.categories.Remove(obj);
            context.SaveChanges();
            return RedirectToAction("ViewCategory");
        }
        public ActionResult UpdateCategory(int id)
        {
            var model = context.categories.Where(x => x.id == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public JsonResult UpdateCategory(category ctg)
        {
            var model = context.categories.Where(x => x.id == ctg.id).FirstOrDefault();
            try
            {
                model.name = ctg.name;
                model.note = ctg.note;
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