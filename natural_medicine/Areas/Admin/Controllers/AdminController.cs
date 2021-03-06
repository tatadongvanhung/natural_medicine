using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using natural_medicine.Models;

namespace natural_medicine.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        private MyDBContext context = new MyDBContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ViewCustomer(int ?page)
        {
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var model = context.users.Where(x => x.user_type == 1).ToList();
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DeleteCustomer(int id)
        {
            var model = context.users.Where(x => x.id == id).FirstOrDefault();
            context.users.Remove(model);
            context.SaveChanges();
            return RedirectToAction("ViewCustomer");
        }
    }
}