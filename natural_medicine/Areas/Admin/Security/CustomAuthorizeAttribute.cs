using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using natural_medicine.Models;

namespace natural_medicine.Areas.Admin.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current == null)
            {
                filterContext.Result = new RedirectResult("/admin/admin/Login");
                return;
            }
            var acc = (user)HttpContext.Current.Session["loginAdmin"];

            if (acc == null)
            {
                filterContext.Result = new RedirectResult("/admin/admin/login");
            }
        }
    }
}