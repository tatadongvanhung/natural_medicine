using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace natural_medicine.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult InsertProduct()
        {
            return View();
        }
        public ActionResult ViewProduct()
        {
            return View();
        }
    }
}