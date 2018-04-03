using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class MembershipController : MembershipBaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
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
        public async Task<ActionResult> Edit()
        {
            var response = this.ResponseHelper.GetResponse<BusinessViewModel>();

            var Memberships = await this.GetMemberships();
            ViewBag.MembershipId = Memberships.Select(s => new SelectListItem()
            {
                Value = Convert.ToString(s.Id),
                Text = s.Title
            });

            var result = await BusinessService.Get(RegisterViewModel.Business.Id);
            if (result != null && result.Status)
            {
                response.Status = result.Status;
                response.Message = result.Message;
                response.Data = result.Data.Business;
            }
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Data")] ResponseViewModel<BusinessViewModel> model)
        {
            var result = new ResponseViewModel<BusinessViewModel>();
            try
            {
                var response = await this.BusinessService.Update(FieldType.Membership,model.Data);
                if (response.Status)
                {
                    result.Status = true;
                    result.Message = response.Message;
                    var status = SetSessionValueByName("aMembershipId", Convert.ToString(model.Data.MembershipId));
                    if (!status)
                    {
                        result.Status = false;
                        result.Message = "Please logout and login to update.";
                    }
                }
                else
                {
                    result.Status = false;
                    result.Message = response.Message;
                }
            }
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }


        [NonAction]
        private async Task<List<MembershipViewModel>> GetMemberships()
        {
            var response = await this.MembershipService.Gets();
            if (response != null)
            {
                return response.Data;
            }
            else
            {
                return new List<MembershipViewModel>();
            }
        }
    }
}