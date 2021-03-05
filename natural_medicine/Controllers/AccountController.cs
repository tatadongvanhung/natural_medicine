using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using natural_medicine.Models;

namespace natural_medicine.Controllers
{
    public class AccountController : Controller
    {

        private MyDBContext context = new MyDBContext();
        public ActionResult MyAccount()
        {
            return View();
        }
        public ActionResult Regiter()
        {
            return View();
        }
        public ActionResult CheckPhoneNumber(string id)
        {
            var result = "";
            var model = context.users.Where(x => x.phone.Trim() == id).FirstOrDefault();
            if (model == null)
            {
                result = "success";
            } else
            {
                result = "error";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}
