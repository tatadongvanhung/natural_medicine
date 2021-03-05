using natural_medicine.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;
using BCrypt.Net;
using System.Linq;
using System.Web.Mvc;
using System;

namespace natural_medicine.Controllers
{
    public class AccountController : Controller
    {

        private MyDBContext context = new MyDBContext();
        public class UserModel {
            public int id { get; set; }
            public string name { get; set; }
            public string phone {get; set; }
            public string email { get; set; }
            public string address{ get; set; }
        }

        [HttpPost]
        public ActionResult Login(user auth)
        {
            user model = context.users.Where(X => X.phone == auth.phone).FirstOrDefault();
            if (model != null && model.user_type == 1){
                Boolean checkPasswork = BCrypt.Net.BCrypt.Verify(auth.password, model.password.Trim());
                if (checkPasswork){
                    Session["loginClient"] = model;
                    model.last_login_at = DateTime.Now;
                    context.SaveChanges();
                }
            } else {
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpPost] 
        public ActionResult PopUpLogin (user auth)
        {
            user model = context.users.Where(X => X.phone == auth.phone).FirstOrDefault();
            if (model != null && model.user_type == 1)
            {
                Boolean checkPasswork = BCrypt.Net.BCrypt.Verify(auth.password, model.password.Trim());
                if (checkPasswork)
                {
                    Session["loginClient"] = model;
                    model.last_login_at = DateTime.Now;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("payment", "cart");
        }
        public ActionResult Logout()
        {
            Session["loginClient"] = null;
            return RedirectToAction("Index","Home");
        }
        public ActionResult MyAccount()
        {
            user auth = (user)Session["loginClient"];
            var adds = context.addresses.Where(x => x.user_id == auth.id).FirstOrDefault();
            ViewBag.user_address = adds;
            return View(auth);
        }
        public ActionResult Regiter()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult ChangeInfo(user model)
        //{
        //    var obj = context.users.Where(x => x.id == model.id).FirstOrDefault();
        //    obj.name = model.name;
        //    obj.phone = model.phone;
        //    context.SaveChanges();
        //    return Redirect(Request.UrlReferrer.ToString());
        //}
        //[HttpPost]
        //public ActionResult ChangePassword(user model)
        //{
        //    var obj = context.users.Where(x => x.id == model.id).FirstOrDefault();
        //    model.password = BCrypt.Net.BCrypt.HashPassword(model.password, 14);
        //    obj.password = model.password;
        //    context.SaveChanges();
        //    return Redirect(Request.UrlReferrer.ToString());
        //}

        [HttpPost]
        public JsonResult ChangeInfo(UserModel model)
        {
            var obj = context.users.Where(x => x.id == model.id).FirstOrDefault();
            obj.name = model.name;
            obj.phone = model.phone;
            obj.update_at = DateTime.Now;
            context.SaveChanges();
            var add = context.addresses.Where(x => x.user_id == obj.id).FirstOrDefault();
            add.address1 = model.address;
            add.update_at = DateTime.Now;
            context.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChangePassword(user model)
        {
            var obj = context.users.Where(x => x.id == model.id).FirstOrDefault();
            model.password = BCrypt.Net.BCrypt.HashPassword(model.password, 14);
            obj.password = model.password;
            context.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public ActionResult RegiterAjax(user user, string address)
        {
            var result = "success";
            using (DbContextTransaction dbTran = context.Database.BeginTransaction())
            {
                try
                {
                    user.user_type = 1;
                    user.password = BCrypt.Net.BCrypt.HashPassword(user.password, 14);
                    user.create_at =  DateTime.Now;
                    context.users.Add(user);
                    context.SaveChanges();
                    var model = context.users.Max(x=> x.id);

                    address user_address = new address();
                    user_address.user_id = model;
                    user_address.address1 = address;
                    user_address.create_at = DateTime.Now;
                    context.addresses.Add(user_address);
                    context.SaveChanges();
                    dbTran.Commit();
                }
                catch {
                    dbTran.Rollback();
                    result = "error";
                }
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult Regiter(user user, string address)
        {
            var result = "success";
            using (DbContextTransaction dbTran = context.Database.BeginTransaction())
            {
                try
                {
                    user.user_type = 1;
                    user.password = BCrypt.Net.BCrypt.HashPassword(user.password, 14);
                    user.create_at = DateTime.Now;
                    context.users.Add(user);
                    context.SaveChanges();
                    var model = context.users.Max(x => x.id);

                    address user_address = new address();
                    user_address.user_id = model;
                    user_address.address1 = address;
                    user_address.create_at = DateTime.Now;
                    context.addresses.Add(user_address);
                    context.SaveChanges();
                    dbTran.Commit();
                }
                catch
                {
                    dbTran.Rollback();
                    result = "error";
                }
            }
            return RedirectToAction("Regiter");
        }


    }
}
