using System.Web.Mvc;
using System.Threading.Tasks;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class AccountController : AccountBaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var result = await BusinessService.Get(RegisterViewModel.Business.Id);
            if (result != null && result.Status)
            {
                var response = new ResponseViewModel<BusinessViewModel>();
                response.Status = result.Status;
                response.Message = result.Message;
                response.Data = result.Data.Business;
                return View(response);
            }
            else
            {
                var response = this.ResponseHelper.GetResponse<BusinessViewModel>();
                return View(Response);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Administrator()
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
        public async Task<ActionResult> Membership()
        {
            var result = await MembershipService.Get(RegisterViewModel.Business.MembershipId);
            if (result != null && result.Status)
                return View(result);
            else
            {
                var response = this.ResponseHelper.GetResponse<MembershipViewModel>();
                return View(Response);
            }
        }

        [HttpGet]
        public ActionResult Billing()
        {
            return View();
        }

    }
}