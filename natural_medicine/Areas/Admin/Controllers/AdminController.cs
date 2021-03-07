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
            user model = context.users.Where(X => X.phone == auth.phone).FirstOrDefault();
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
    }
}