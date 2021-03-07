using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using natural_medicine.Areas.Admin.Security;

namespace natural_medicine.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        [CustomAuthorize]
        public ActionResult InsertProduct()
        {
            return View();
        }
        [CustomAuthorize]
        public ActionResult ViewProduct()
        {
            return View();
        }
    }
}