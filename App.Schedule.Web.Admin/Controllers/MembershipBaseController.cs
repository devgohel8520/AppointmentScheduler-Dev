using System.Web.Mvc;
using App.Schedule.Web.Admin.Services;

namespace App.Schedule.Web.Admin.Controllers
{
    public class MembershipBaseController : BaseController
    {
        protected MembershipService MembershipService;
        protected DashboardService DashboardService;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var status = this.LoginStatus();
            if (!status)
            {
                filterContext.Result = RedirectToAction("Index", "Login");
            }
            else
            {
                this.MembershipService = new MembershipService(this.Token);
                this.DashboardService = new DashboardService(this.Token);
            }
        }
    }
}