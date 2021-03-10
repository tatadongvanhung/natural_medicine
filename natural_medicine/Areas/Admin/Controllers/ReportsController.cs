using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using natural_medicine.Models;

namespace natural_medicine.Areas.Admin.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Admin/Reports
        private MyDBContext context = new MyDBContext();

        public ActionResult ReportImport(DateTime ?start_date, DateTime ?end_date)
        {
            //SELECT* FROM dbo.products
            //JOIN(SELECT product_id, SUM(quantity) AS sum_quantity, SUM(quantity * price) AS sum_total
            //FROM dbo.imports WHERE import_date >= '03/08/2021' AND import_date <= '03/09/2021'
            //GROUP BY product_id) AS nhap ON nhap.product_id = products.id
            return View();
        }
        public ActionResult ReportExport()
        {
            return View();
        }
        public ActionResult TotalRevenue()
        {
            return View();
        }
    }
}