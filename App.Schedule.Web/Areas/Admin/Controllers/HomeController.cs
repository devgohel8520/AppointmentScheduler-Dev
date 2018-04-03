using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class HomeController : HomeBaseController
    {
        [HttpGet]
        public ActionResult Login()
        {
            var model = new ResponseViewModel<LoginViewModel>();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Login(ServiceDataViewModel<LoginViewModel> model)
        {
            var result = new ResponseViewModel<RegisterViewModel>();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                    result.Status = false;
                    result.Message = errMessage;
                }
                else
                {
                    if (BusinessCategoryService != null)
                    {
                        var response = await BusinessEmployeeService.VerifyLoginCredential(model.Data.Email, model.Data.Password);
                        result.Status = response.Status;
                        result.Message = response.Message;
                        result.Data = response.Data;
                        if (response.Status)
                        {
                            var tokenResponse = await BusinessEmployeeService.VerifyAndGetAdminAccessToken(model.Data.Email, model.Data.Password);
                            result.Status = tokenResponse.Status;
                            result.Message = tokenResponse.Message;
                            if (tokenResponse.Status)
                            {
                                if (string.IsNullOrEmpty(tokenResponse.Data))
                                {
                                    RedirectToAction("Logout", "Dashboard", new { area = "Admin" });
                                }
                                SetAdminSession(response.Data, model.Data.IsKeepLoggedIn, tokenResponse.Data);
                            }
                        }
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = "There was a problem. Please try again later.";
                    }
                }
            }
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Json(new { status = result.Status, message = result.Message, data = result.Data }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Forgot()
        {
            return View();
        }

      

    }
}