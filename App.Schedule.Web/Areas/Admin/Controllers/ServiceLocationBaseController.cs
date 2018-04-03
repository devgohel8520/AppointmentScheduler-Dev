using System.Web.Mvc;
using App.Schedule.Web.Services;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class ServiceLocationBaseController : BaseController
    {
        protected ServiceLocationService ServiceLocationService;
        protected BusinessHourService BusinessHourService;
        protected BusinessHolidayService BusinessHolidayService;
        protected TimezoneService TimezoneService;
        protected CountryService CountryService;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var status = this.LoginStatus();
            if (!status)
            {
                filterContext.Result = RedirectToAction("Login", "Home", new { area = "Admin" });
            }
            else
            {
                this.ServiceLocationService = new ServiceLocationService(this.Token);
                this.BusinessHourService = new BusinessHourService(this.Token);
                this.BusinessHolidayService = new BusinessHolidayService(this.Token);
                this.TimezoneService = new TimezoneService(this.Token);
                this.CountryService = new CountryService(this.Token);
            }
        }
    }
}