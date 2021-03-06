﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using natural_medicine.Models;
using PagedList;

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
            var model = context.products.Where(X => X.category_id == product_detail.category_id && X.id != id).ToList();
            ViewBag.SP = product_detail;
            var rate = context.reviews.Where(x => x.product_id == id).ToList();
            ViewBag.rate = rate;

            return View(model);
        }

        public ActionResult Category(int id)
        {
            var model = context.products.Where(X => X.category_id == id).ToList();
            ViewBag.category = context.categories.Where(x => x.id == id).FirstOrDefault();
            return View(model);
        }

        public ActionResult News(int ?page)
        {
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var model = context.news
                .Where(x => EntityFunctions.TruncateTime(x.date_post) <= DateTime.Today)
                .OrderByDescending(x => x.date_post).ToList();
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult NewsDetail(int id)
        {
            var model = context.news.Where(x => x.id==id).FirstOrDefault();
            return View(model);
        }
        

       [HttpPost]
        public ActionResult Search(String search)
        {
            var model = context.products.Where(X => X.name.Contains(search) || X.uses.Contains(search)).ToList();
            return View(model);
        }

        [HttpPost]
        public JsonResult Rating(review data)
        {
            data.create_at = DateTime.Now;
            context.reviews.Add(data);
            context.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}