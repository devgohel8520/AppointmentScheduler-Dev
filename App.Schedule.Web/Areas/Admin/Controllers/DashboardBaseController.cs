using System;
using System.Web;
using System.Web.Mvc;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class DashboardBaseController : BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var status = LoginStatus();
            if (!status)
            {
                filterContext.Result = RedirectToAction("Login", "Home", new { area = "Admin" });
            }
        }

        [NonAction]
        public bool LogoutStatus()
        {
            if (Request.Cookies["aadminappointment"] != null)
            {
                var admin = new HttpCookie("aadminappointment");
                Session["aEmail"] = "";
                admin.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(admin);
                return true;
            }
            return false;
        }
    }
}