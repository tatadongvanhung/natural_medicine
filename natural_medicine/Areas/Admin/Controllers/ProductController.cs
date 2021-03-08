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
    public class ProductController : Controller
    {
        // GET: Admin/Product
        private MyDBContext context = new MyDBContext();
        [CustomAuthorize]
        public ActionResult InsertProduct()
        {
            return View();
        }
        [CustomAuthorize]
        public ActionResult ViewProduct(int ?category_id, String search, int ?page)
        {
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var category = context.categories.ToList();
            ViewBag.category = category;
            if (category_id != null && search != null)
            {
                var model = context.products
                    .Where(x => x.category_id == category_id
                    && x.name.Contains(search)).ToList();
                return View(model.ToPagedList(pageNumber, pageSize));
            } else if (search != null) {
                var model = context.products
                    .Where(x => x.name.Contains(search)).ToList();
                return View(model.ToPagedList(pageNumber, pageSize));
            }
            else if (category_id != null)
            {
                
                var model = context.products.Where(x => x.category_id == category_id).ToList();
                return View(model.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var model = context.products.ToList();
                return View(model.ToPagedList(pageNumber, pageSize));
            }
            
        }

        public ActionResult ImportProduct(int id)
        {
            return View();
        }
        public ActionResult UpdateProduct(int id)
        {
            return View();
        }
        public ActionResult DeleteProduct(int id)
        {
            return View();
        }
    }
}