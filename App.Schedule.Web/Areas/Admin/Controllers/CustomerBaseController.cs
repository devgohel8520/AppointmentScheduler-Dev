using System.Web.Mvc;
using App.Schedule.Web.Services;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class CustomerBaseController : BaseController
    {
        protected BusinessCustomerService BusinessCustomerService;
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
                this.BusinessCustomerService = new BusinessCustomerService(this.Token);
                this.ServiceLocationService = new ServiceLocationService(this.Token);
            }
        }
    }
}