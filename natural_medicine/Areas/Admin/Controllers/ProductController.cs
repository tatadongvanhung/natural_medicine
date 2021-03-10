using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using natural_medicine.Areas.Admin.Security;
using natural_medicine.Models;
using PagedList;
using System.IO;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace natural_medicine.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        private MyDBContext context = new MyDBContext();
        public ActionResult InsertProduct()
        {
            ViewBag.category = context.categories.ToList();
            ViewBag.publisher = context.publishers.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult InsertProduct(HttpPostedFileBase file, product model)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    // lấy tên tệp tin
                    var fileName = Path.GetFileName(file.FileName);
                    fileName = "img_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + "_"+ fileName;
                    // lưu trữ tệp tin vào folder ~/App_Data/uploads
                    var path = Path.Combine(Server.MapPath("~/Content/assets/img"), fileName);
                    file.SaveAs(path);
                    model.image_url = fileName;
                    model.inventory_quantity = 0;
                    context.products.Add(model);
                    context.SaveChanges();
                    return RedirectToAction("viewproduct");
                }
                else
                {
                    return RedirectToAction("insertproduct");
                }
            }
            catch
            {
                return RedirectToAction("insertproduct");
            }
        }
        
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
                var model = context.products.OrderByDescending(x => x.id).ToList();
                return View(model.ToPagedList(pageNumber, pageSize));
            }
            
        }
        public ActionResult ViewImportProduct(DateTime? start_date, DateTime? end_date,int ?product_id, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var product = context.products.ToList();
            
            if (product_id != null && start_date != null && end_date != null)
            {
               
                var model = context.VIEW_IMPORT_PRODUCT
                .Where(x => EntityFunctions.TruncateTime(x.create_at) >= start_date
                    && EntityFunctions.TruncateTime(x.create_at) <= end_date
                    && x.product_id == product_id)
                .OrderByDescending(x => x.create_at).ToList();
                ViewBag.product = product;
                return View(model.ToPagedList(pageNumber, 100));
            } else if (product_id != null && start_date == null && end_date == null)
            {
                var model = context.VIEW_IMPORT_PRODUCT
                .Where(x => x.product_id == product_id).OrderByDescending(x => x.create_at).ToList();
                ViewBag.product = product;
                return View(model.ToPagedList(pageNumber, 100));
            } else if(product_id == null && start_date != null && end_date != null)
            {
                var model = context.VIEW_IMPORT_PRODUCT
                .Where(x => EntityFunctions.TruncateTime(x.create_at) >= start_date
                    && EntityFunctions.TruncateTime(x.create_at) <= end_date)
                .OrderByDescending(x => x.create_at).ToList();
                ViewBag.product = product;
                return View(model.ToPagedList(pageNumber, 100));
            }
            else
            {
                var model = context.VIEW_IMPORT_PRODUCT.OrderByDescending(x => x.create_at).ToList();
                ViewBag.product = product;
                return View(model.ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult ImportProduct(int id)
        {
            ViewBag.publisher = context.publishers.ToList();
            var model = context.products.Where(x => x.id == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult ImportProduct(import nhap)
        {
            using (DbContextTransaction dbTran = context.Database.BeginTransaction())
            {
                try
                {
                    nhap.create_at = DateTime.Now;
                    nhap.import_date = DateTime.Today;
                    context.imports.Add(nhap);
                    context.SaveChanges();
                    var model = context.products.Where(x => x.id == nhap.product_id).FirstOrDefault();
                    model.inventory_quantity = model.inventory_quantity + nhap.quantity;
                    context.SaveChanges();
                    dbTran.Commit();
                    return RedirectToAction("viewproduct");
                }
                catch
                {
                    TempData["error"] = "Đã xảy ra lỗi!";
                    dbTran.Rollback();
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
        }
        public ActionResult UpdateProduct(int id)
        {
            var model = context.products.Where(x => x.id == id).FirstOrDefault();
            ViewBag.category = context.categories.ToList();
            ViewBag.publisher = context.publishers.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateProduct(HttpPostedFileBase file, product model)
        {
            try
            {
                var obj = context.products.Where(x => x.id == model.id).FirstOrDefault();
                if (file != null && file.ContentLength > 0)
                {
                    // lấy tên tệp tin
                    var fileName = Path.GetFileName(file.FileName);
                    fileName = "img_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + "_" + fileName;
                    // lưu trữ tệp tin vào folder ~/App_Data/uploads
                    var path = Path.Combine(Server.MapPath("~/Content/assets/img"), fileName);
                    file.SaveAs(path);
                    obj.name = model.name;
                    obj.ingredient = model.ingredient;
                    obj.uses = model.uses;
                    obj.unit = model.unit;
                    obj.dosage = model.dosage;
                    obj.description = model.description;
                    obj.price = model.price;
                    obj.note = model.note;
                    obj.category_id = model.category_id;
                    obj.publisher_id = model.publisher_id;
                    obj.image_url = fileName;
                    context.SaveChanges();
                    return RedirectToAction("viewproduct");
                }
                else
                {
                    obj.name = model.name;
                    obj.ingredient = model.ingredient;
                    obj.uses = model.uses;
                    obj.unit = model.unit;
                    obj.dosage = model.dosage;
                    obj.description = model.description;
                    obj.price = model.price;
                    obj.note = model.note;
                    obj.category_id = model.category_id;
                    obj.publisher_id = model.publisher_id;
                    context.SaveChanges();
                    return RedirectToAction("viewproduct");
                }
            }
            catch
            {
                return RedirectToAction("insertproduct");
            }
        }
        public ActionResult DeleteProduct(int id)
        {
            var model = context.products.Where(x => x.id == id).FirstOrDefault();
            context.products.Remove(model);
            context.SaveChanges();
            return RedirectToAction("viewproduct");
        }
    }
}