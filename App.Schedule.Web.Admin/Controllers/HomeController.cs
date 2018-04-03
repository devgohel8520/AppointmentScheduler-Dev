using System.Web.Mvc;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var model = new ServiceDataViewModel<string>();
            Session["HomeLink"] = "Home";
            return View(model);
        }
    }
}