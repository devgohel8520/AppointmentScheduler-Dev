using System.Web.Mvc;
using App.Schedule.Web.Admin.Services;

namespace App.Schedule.Web.Admin.Controllers
{
    public class RegisterBaseController : BaseController
    {
        protected AdminService AdminService;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (LoginStatus())
            {
                filterContext.Result = RedirectToAction("Index", "Dashboard");
            }
            else
            {
                this.AdminService = new AdminService(this.Token);
            }
        }
    }
}