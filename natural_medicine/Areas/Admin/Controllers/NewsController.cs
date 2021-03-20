using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using natural_medicine.Areas.Admin.Security;
using natural_medicine.Models;
using PagedList;

namespace natural_medicine.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private MyDBContext context = new MyDBContext();
        // GET: Admin/News
        [CustomAuthorize]
        public ActionResult InsertNews()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertNews(news data)
        {
            data.create_at = DateTime.Now;
            context.news.Add(data);
            context.SaveChanges();
            return RedirectToAction("viewnews");
        }
        [CustomAuthorize]
        public ActionResult ViewNews(int ?page)
        {
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var model = context.news.OrderByDescending(x => x.date_post).ToList();
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        [CustomAuthorize]
        public ActionResult DeleteNews(int id)
        {
            var model = context.news.Where(x => x.id == id).FirstOrDefault();
            context.news.Remove(model);
            context.SaveChanges();
            return RedirectToAction("viewnews");
        }

        public ActionResult UpdateNews(int id)
        {
            var model = context.news.Where(x => x.id == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateNews(news data)
        {
            var model = context.news.Where(x => x.id == data.id).FirstOrDefault();
            model.title = data.title;
            model.date_post = data.date_post;
            model.content = data.content;
            model.update_at = DateTime.Now;
            context.SaveChanges();
            return RedirectToAction("viewnews");
        }
    }
}