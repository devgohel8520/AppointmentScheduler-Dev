using System.Web.Mvc;
using App.Schedule.Web.Services;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class EmployeeBaseController : BaseController
    {
        protected BusinessEmployeeService BusinessEmployeeService;
        protected ServiceLocationService ServiceLocationService;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var status = LoginStatus();
            if (!status)
            {
                filterContext.Result = RedirectToAction("Login", "Home", new { area = "Admin" });
            }
            else
            {
                this.BusinessEmployeeService = new BusinessEmployeeService(this.Token);
                this.ServiceLocationService = new ServiceLocationService(this.Token);
            }
        }
    }
}