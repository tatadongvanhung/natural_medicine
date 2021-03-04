using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using natural_medicine.Models;

namespace natural_medicine.Controllers
{
    public class HomeController : Controller
    {
        private MyDBContext context = new MyDBContext();
        public ActionResult Index()
        {
            var model = context.products.Where(X => X.name != null).ToList();
            return View(model);
        }

        public ActionResult _Slidebar()
        {
            var model = context.categories.Where(x => x.name != null).ToList();
            return View(model);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Product(int id)
        {
            var product_detail = context.products.Where(X => X.id == id).FirstOrDefault();
            var model = context.products.Where(X => X.category_id == product_detail.category_id).ToList();
            ViewBag.SP = product_detail;
            return View(model);
        }

        public ActionResult Category(int id)
        {
            var model = context.products.Where(X => X.category_id == id).ToList();
            ViewBag.category = context.categories.Where(x => x.id == id).FirstOrDefault();
            return View(model);
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}