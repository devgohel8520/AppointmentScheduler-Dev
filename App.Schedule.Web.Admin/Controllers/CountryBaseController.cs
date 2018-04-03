using App.Schedule.Web.Admin.Services;
using System.Web.Mvc;

namespace App.Schedule.Web.Admin.Controllers
{
    public class CountryBaseController : BaseController
    {
        protected CountryService CountryService;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var status = this.LoginStatus();
            if (!status)
            {
                filterContext.Result = RedirectToAction("Index", "Login");
            }
            else
            {
                this.CountryService = new CountryService(this.Token);
            }
        }
    }
}