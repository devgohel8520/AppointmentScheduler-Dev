using System.Web.Mvc;
using App.Schedule.Web.Admin.Services;

namespace App.Schedule.Web.Admin.Controllers
{
    public class BusinessCategoryBaseController : BaseController
    {
        protected DashboardService DashboardService;
        protected BusinessCategoryService BusinessCategoryService;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var status = this.LoginStatus();
            if (!status)
            {
                filterContext.Result = RedirectToAction("Index", "Login");
            }
            else
            {
                this.DashboardService = new DashboardService(this.Token);
                this.BusinessCategoryService = new BusinessCategoryService(this.Token);
            }
        }
    }
}