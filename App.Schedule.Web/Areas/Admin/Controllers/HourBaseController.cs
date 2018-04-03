using App.Schedule.Web.Services;
using System;
using System.Web.Mvc;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class HourBaseController : BaseController
    {
        protected BusinessHourService BusinessHourService;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var status = this.LoginStatus();
            if (!status)
            {
                filterContext.Result = RedirectToAction("Login", "Home", new { area = "Admin" });
            }
            else
            {
                this.BusinessHourService = new BusinessHourService(this.Token);
            }
        }
    }
}