using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace natural_medicine.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        // GET: Admin/Report
        public ActionResult ReportImport()
        {
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