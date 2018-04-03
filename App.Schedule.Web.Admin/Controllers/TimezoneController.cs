using System;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Controllers
{
    public class TimezoneController : TimezoneBaseController
    {
        public async Task<ActionResult> Index(int? page, string search)
        {
            var model = new ServiceDataViewModel<IPagedList<TimezoneViewModel>>();
            try
            {
                Session["HomeLink"] = "Timezone";
                var pageNumber = page ?? 1;
                ViewBag.search = search;

                var response = await TimezoneService.Gets();
                if (response.Status)
                {
                    var data = response.Data;
                    if (search == null)
                    {
                        model.Data = data.ToPagedList<TimezoneViewModel>(pageNumber, 10);
                        return View(model);
                    }
                    else
                    {
                        model.Data = data.Where(d => d.Title.ToLower().Contains(search.ToLower())).ToList().ToPagedList(pageNumber, 5);
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
        public async Task<ActionResult> Create()
        {
            var model = new ServiceDataViewModel<TimezoneViewModel>();
            var countries = await this.DashboardService.GetCountries();
            ViewBag.CountryId = countries.Select(d => new SelectListItem()
            {
                Value = Convert.ToString(d.Id),
                Text = d.Name
            });
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Data")] ServiceDataViewModel<TimezoneViewModel> model)
        {
            var result = new ResponseViewModel<TimezoneViewModel>();
            if (!ModelState.IsValid)
            {
                var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                result.Status = false;
                result.Message = errMessage;
            }
            else
            {
                model.Data.AdministratorId = admin.Id;
                var response = await this.TimezoneService.Add(model.Data);
                if (response != null)
                {
                    result.Status = response.Status;
                    result.Message = response.Message;
                    result.Data = response.Data;
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
            var model = new ServiceDataViewModel<TimezoneViewModel>();
            try
            {
                model.HasError = true;
                if (!id.HasValue)
                {
                    model.Error = "Please provide a valid id.";
                }
                else
                {
                    var res = await this.TimezoneService.Get(id.Value);
                    if (res.Status)
                    {
                        var countries = await this.DashboardService.GetCountries();
                        ViewBag.CountryId = countries.Select(d => new SelectListItem()
                        {
                            Value = Convert.ToString(d.Id),
                            Text = d.Name
                        });
                        model.HasError = false;
                        model.Data = res.Data;
                    }
                    else
                    {
                        model.Error = res.Message;
                    }
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
        public async Task<ActionResult> Edit([Bind(Include = "Data")] ServiceDataViewModel<TimezoneViewModel> model)
        {
            var result = new ResponseViewModel<TimezoneViewModel>();
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
                    var response = await this.TimezoneService.Update(model.Data);
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
            var model = new ServiceDataViewModel<TimezoneViewModel>();
            try
            {
                model.HasError = true;
                if (!id.HasValue)
                {
                    model.Error = "Please provide a valid id.";
                }
                else
                {
                    var res = await this.TimezoneService.Get(id.Value);
                    if (res.Status)
                    {
                        var countries = await this.DashboardService.GetCountries();
                        ViewBag.CountryId = countries.Select(d => new SelectListItem()
                        {
                            Value = Convert.ToString(d.Id),
                            Text = d.Name
                        });
                        model.HasError = false;
                        model.Data = res.Data;
                    }
                    else
                    {
                        model.Error = res.Message;
                    }
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
        public async Task<ActionResult> Delete([Bind(Include = "Data")] ServiceDataViewModel<TimezoneViewModel> model)
        {
            var result = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                var response = await this.TimezoneService.Delete(model.Data.Id);
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
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }
    }
}