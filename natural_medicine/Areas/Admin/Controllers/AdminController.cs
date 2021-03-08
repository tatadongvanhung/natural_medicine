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
    public class AdminController : Controller
    {
        private MyDBContext context = new MyDBContext();

        [CustomAuthorize]
        public ActionResult Index()
        {
            var model = context.VIEW_ORDER.OrderByDescending(x => x.create_at).Take(5);
            var count_customer = context.users.Where(x => x.user_type == 1).Count();
            var count_category = context.categories.Count();
            var count_order = context.orders.Count();
            ViewBag.count_customer = count_customer;
            ViewBag.count_category = count_category;
            ViewBag.count_order = count_order;
            return View(model);
        }
        public ActionResult Login()
        {
            return View();
        }
        [CustomAuthorize]
        public ActionResult ViewCustomer(int ?page)
        {
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var model = context.users.Where(x => x.user_type == 1).ToList();
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        [CustomAuthorize]
        public ActionResult DeleteCustomer(int id)
        {
            var model = context.users.Where(x => x.id == id).FirstOrDefault();
            context.users.Remove(model);
            context.SaveChanges();
            return RedirectToAction("ViewCustomer");
        }
        [HttpPost]
        public ActionResult Login(user auth)
        {
            user model = context.users.Where(X => X.phone == auth.phone && X.user_type != 1).FirstOrDefault();
            if (model != null && model.user_type != 1)
            {
                Boolean checkPasswork = BCrypt.Net.BCrypt.Verify(auth.password, model.password.Trim());
                if (checkPasswork)
                {
                    Session["loginAdmin"] = model;
                    model.last_login_at = DateTime.Now;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            else
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            
        }
        public ActionResult Logout()
        {
            Session["loginAdmin"] = null;
            return RedirectToAction("login");
        }
        [CustomAuthorize]
        public ActionResult CheckPhoneNumber(string id)
        {
            var result = "";
            var model = context.users.Where(x => x.phone.Trim() == id).FirstOrDefault();
            if (model == null)
            {
                result = "success";
            }
            else
            {
                result = "error";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize]
        public ActionResult InsertAdmin()
        {
            return View();
        }
        [HttpPost]
        public JsonResult InsertAdmin(user model)
        {
            model.password =  BCrypt.Net.BCrypt.HashPassword(model.password, 14);
            model.create_at = DateTime.Now;
            context.users.Add(model);
            context.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
            
        }
        [CustomAuthorize]
        public ActionResult ViewAdmin(int ?page)
        {
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var model = context.users.Where(x => x.user_type != 1).ToList();
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        [CustomAuthorize]
        public ActionResult DeleteAdmin(int id)
        {
            var model = context.users.Where(x => x.id == id).FirstOrDefault();
            context.users.Remove(model);
            context.SaveChanges();
            return RedirectToAction("ViewAdmin");
        }
        [CustomAuthorize]
        public ActionResult UpdateAdmin(int id)
        {
            var model = context.users.Where(x => x.id == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public JsonResult UpdateAdmin(user model)
        {
            var obj = context.users.Where(x => x.id == model.id).FirstOrDefault();
            if (model.password != null)
            {
                var password = BCrypt.Net.BCrypt.HashPassword(model.password, 14);
                model.password = password;
                obj.name = model.name;
                obj.phone = model.phone;
                obj.email = model.email;
                obj.password = model.password;
                obj.user_type = model.user_type;
                obj.update_at = DateTime.Now;
                context.SaveChanges();
            } else
            {
                obj.name = model.name;
                obj.phone = model.phone;
                obj.email = model.email;
                obj.user_type = model.user_type;
                obj.update_at = DateTime.Now;
                context.SaveChanges();
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}