﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using natural_medicine.Models;
using System.Data.Entity.Core.Objects;
using natural_medicine.Areas.Admin.Security;
using System.Data.Entity;

namespace natural_medicine.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Admin/Orders
        private MyDBContext context = new MyDBContext();
        [CustomAuthorize]
        public ActionResult ViewOrder(DateTime ?start_date, DateTime ?end_date, int ?page)
        {
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (start_date != null && end_date != null)
            {
                if(start_date == end_date)
                {
                    DateTime start = (DateTime)start_date;
                    var model = context.VIEW_ORDER.Where(x => EntityFunctions.TruncateTime(x.create_at) == start.Date)
                        .OrderByDescending(x => x.create_at).ToList();
                    return View(model.ToPagedList(pageNumber, 100));
                }
                else
                {
                    var model = context.VIEW_ORDER
                    .Where(x => EntityFunctions.TruncateTime(x.create_at) >= start_date 
                        && EntityFunctions.TruncateTime(x.create_at) <= end_date)
                    .OrderByDescending(x => x.create_at).ToList();
                    return View(model.ToPagedList(pageNumber, 100));
                }
            }
            else{
                var model = context.VIEW_ORDER.OrderByDescending(x => x.create_at).ToList();
                return View(model.ToPagedList(pageNumber, pageSize));
            }
        }
        [CustomAuthorize]
        public ActionResult ViewOrder_Detail(int id)
        {
            var order = context.VIEW_ORDER.Where(x => x.id == id).FirstOrDefault();
            var order_status = context.orders_status.ToList();
            ViewBag.order = order;
            ViewBag.order_status = order_status;
            var model = context.VIEW_ORDER_DETAIL.Where(x => x.order_id == id).ToList();
            return View(model);
        }
        [HttpPost]
        public JsonResult UpdateOrderStatus(order donhang)
        {
            using (DbContextTransaction dbTran = context.Database.BeginTransaction())
            {
                try
                {
                    var model = context.orders.Where(x => x.id == donhang.id).FirstOrDefault();
                    model.status_id = donhang.status_id;
                    if (donhang.status_id == 3) //Trạng thái đã đóng gói: Trừ sản phẩm đi
                    {
                        List<orders_detail> list = context.orders_detail.Where(x => x.order_id == donhang.id).ToList();
                        foreach(orders_detail it in list)
                        {
                            //Cập nhật lại tồn kho
                            var product = context.products.Where(x => x.id == it.product_id).FirstOrDefault();
                            product.inventory_quantity = product.inventory_quantity - it.quantity;
                            context.SaveChanges();
                        }
                    }
                    context.SaveChanges();
                    dbTran.Commit();
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    dbTran.Rollback();
                    return Json("error", JsonRequestBehavior.AllowGet);
                }
            }

        }

    }
    
}