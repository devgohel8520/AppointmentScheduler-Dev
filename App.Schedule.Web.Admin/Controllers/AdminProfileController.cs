using System.Web.Mvc;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Controllers
{
    public class AdminProfileController : BaseController
    {
        public ActionResult Index()
        {
            Session["HomeLink"] = "Profile";
            var model = new ServiceDataViewModel<AdministratorViewModel>() {
                Data = admin,
                HasError = false
            };
            ViewBag.Token = this.Token;
            return View(model);
        }
    }
}