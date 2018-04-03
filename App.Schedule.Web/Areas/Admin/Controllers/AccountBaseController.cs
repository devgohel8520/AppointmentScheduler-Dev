using System.Web.Mvc;
using App.Schedule.Web.Services;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class AccountBaseController : BaseController
    {
        protected BusinessService BusinessService;
        protected MembershipService MembershipService;
        protected BusinessEmployeeService BusinessEmployeeService;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var status = this.LoginStatus();
            if (!status)
            {
                filterContext.Result = RedirectToAction("Login", "Home", new { area = "Admin" });
            }
            else
            {
                this.BusinessService = new BusinessService(this.Token);
                this.MembershipService = new MembershipService(this.Token);
                this.BusinessEmployeeService = new BusinessEmployeeService(this.Token);
            }
        }
    }
}