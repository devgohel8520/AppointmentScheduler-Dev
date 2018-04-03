using System.Web.Mvc;
using App.Schedule.Web.Services;
using App.Schedule.Web.Areas.Admin.Controllers;

namespace App.Schedule.Web.Controllers
{
    public class HomeBaseController : BaseController
    {
        protected BusinessService BusinessService;
        protected MembershipService MembershipService;
        protected BusinessCategoryService BusinessCategoryService;
        protected BusinessEmployeeService BusinessEmployeeService;
        protected CountryService CountryService;
        protected TimezoneService TimezoneService;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var status = LoginStatus();
            if (status)
            {
                filterContext.Result = RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            else
            {
                this.BusinessService = new BusinessService(this.Token);
                this.MembershipService = new MembershipService(this.Token);
                this.CountryService = new CountryService(this.Token);
                this.TimezoneService = new TimezoneService(this.Token);

                this.BusinessCategoryService = new BusinessCategoryService(this.Token);
                this.BusinessEmployeeService = new BusinessEmployeeService(this.Token);
            }
        }
    }
}