using System.Web.Mvc;
using System.Threading.Tasks;
using App.Schedule.Domains.ViewModel;
using System.Linq;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class BusinessAdminController : AdminBaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var result = await BusinessEmployeeService.Get(RegisterViewModel.Employee.Id);
            if (result != null && result.Status)
                return View(result);
            else
            {
                var response = this.ResponseHelper.GetResponse<BusinessEmployeeViewModel>();
                return View(Response);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            var result = await BusinessEmployeeService.Get(RegisterViewModel.Employee.Id);
            if (result != null && result.Status)
            {
                result.Data.Password = "";
                return View(result);
            }
            else
            {
                var response = this.ResponseHelper.GetResponse<BusinessEmployeeViewModel>();
                return View(Response);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Data")] ResponseViewModel<BusinessEmployeeViewModel> model)
        {
            var result = new ResponseViewModel<BusinessHourViewModel>();
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
                    if (model.Data.Password == model.Data.ConfirmPassword)
                    {
                        var response = await this.BusinessEmployeeService.Update(model.Data);
                        if (response.Status)
                        {
                            result.Status = true;
                            result.Message = response.Message;
                        }
                        else
                        {
                            result.Status = false;
                            result.Message = response.Message;
                        }
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = "Please confirm your password.";
                    }
                }
            }
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

    }
}