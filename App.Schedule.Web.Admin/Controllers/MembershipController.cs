using System;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Controllers
{
    public class MembershipController : MembershipBaseController
    {
        public async Task<ActionResult> Index(int? page, string search)
        {
            var model = new ServiceDataViewModel<IPagedList<MembershipViewModel>>();
            try
            {
                Session["HomeLink"] = "Membership";
                var pageNumber = page ?? 1;
                ViewBag.search = search;

                var response = await MembershipService.Gets();
                if (response.Status)
                {
                    var data = response.Data;
                    if (search == null)
                    {
                        model.Data = data.ToPagedList<MembershipViewModel>(pageNumber, 10);
                        return View(model);
                    }
                    else
                    {
                        model.Data = data.Where(d => d.Title.ToLower().Contains(search.ToLower())).ToList().ToPagedList(pageNumber, 10);
                        return View(model);
                    }
                }
                else
                {
                    model.HasError = !response.Status;
                    model.Error = response.Message;
                }
            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.Error = "There was a problem. Please try again later.";
                model.HasMore = true;
                model.ErrorDescription = ex.Message.ToString();
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ServiceDataViewModel<MembershipViewModel>();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Data")] ServiceDataViewModel<MembershipViewModel> model)
        {
            var result = new ResponseViewModel<MembershipViewModel>();
            if (!ModelState.IsValid)
            {
                var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                result.Status = false;
                result.Message = errMessage;
            }
            else
            {
                model.Data.AdministratorId = admin.Id;
                var response = await this.MembershipService.Add(model.Data);
                if (response != null)
                {
                    result.Status = response.Status;
                    result.Message = response.Message;
                }
                else
                {
                    result.Status = false;
                    result.Message = "There was a problem. Please try again later.";
                }
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long? id)
        {
            var model = new ServiceDataViewModel<MembershipViewModel>();
            try
            {
                var res = await this.MembershipService.Get(id.Value);
                if (res.Status)
                {
                    var countries = await this.DashboardService.GetMemberships();
                    model.HasError = false;
                    model.Data = res.Data;
                }
                else
                {
                    model.HasError = true;
                    model.Error = res.Message;
                }
            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.Error = "There was a problem. Please try again later.";
                model.ErrorDescription = ex.Message.ToString();
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Data")] ServiceDataViewModel<MembershipViewModel> model)
        {
            var result = new ResponseViewModel<MembershipViewModel>();
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
                    model.Data.AdministratorId = admin.Id;
                    var response = await this.MembershipService.Update(model.Data);
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
            }
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(long? id)
        {
            var model = new ServiceDataViewModel<MembershipViewModel>();
            try
            {
                model.HasError = true;
                var res = await this.MembershipService.Get(id.Value);
                if (res.Status)
                {
                    var countries = await this.DashboardService.GetMemberships();
                    model.HasError = false;
                    model.Data = res.Data;
                }
                else
                {
                    model.Error = res.Message;
                }
            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.Error = "There was a problem. Please try again later.";
                model.ErrorDescription = ex.Message.ToString();
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind(Include = "Data")] ServiceDataViewModel<MembershipViewModel> model)
        {
            var result = new ResponseViewModel<MembershipViewModel>();
            try
            {
                var response = await this.MembershipService.Delete(model.Data.Id);
                if (response != null)
                {
                    result.Status = response.Status;
                    result.Message = response.Message;
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

        [HttpGet]
        public async Task<ActionResult> Deactive(long? id)
        {
            var model = new ServiceDataViewModel<MembershipViewModel>();
            try
            {
                model.HasError = true;
                var res = await this.MembershipService.Get(id.Value);
                if (res.Status)
                {
                    var countries = await this.DashboardService.GetMemberships();
                    model.HasError = false;
                    model.Data = res.Data;
                }
                else
                {
                    model.Error = res.Message;
                }
            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.Error = "There was a problem. Please try again later.";
                model.ErrorDescription = ex.Message.ToString();
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deactive([Bind(Include = "Data")] ServiceDataViewModel<MembershipViewModel> model)
        {
            var result = new ResponseViewModel<MembershipViewModel>();
            try
            {
                var response = await this.MembershipService.Deactive(model.Data.Id, model.Data.IsActive);
                if (response != null)
                {
                    result.Status = response.Status;
                    result.Message = response.Message;
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
    }
}