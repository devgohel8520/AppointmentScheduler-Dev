using System.Web.Mvc;
using App.Schedule.Web.Services;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class ProfileBaseController : BaseController
    {
        protected BusinessService BusinessService;
        protected TimezoneService TimezoneService;
        protected BusinessCategoryService BusinessCategoryService;
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
                this.BusinessService = new BusinessService(this.Token);
                this.TimezoneService = new TimezoneService(this.Token);
                this.BusinessCategoryService = new BusinessCategoryService(this.Token);
                this.CountryService = new CountryService(this.Token);
            }
        }
    }
}