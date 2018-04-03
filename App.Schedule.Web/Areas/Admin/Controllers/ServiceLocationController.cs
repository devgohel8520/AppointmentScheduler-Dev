using System;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class ServiceLocationController : ServiceLocationBaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index(int? page, string search)
        {
            var model = this.ResponseHelper.GetResponse<IPagedList<ServiceLocationViewModel>>();
            var pageNumber = page ?? 1;
            ViewBag.search = search;

            ViewBag.BusinessId = RegisterViewModel.Business.Id;
            ViewBag.ServiceLocationId = RegisterViewModel.Employee.ServiceLocationId;

            var result = await ServiceLocationService.Gets(RegisterViewModel.Business.Id, TableType.BusinessId);
            if (result.Status)
            {
                var data = result.Data;
                model.Status = result.Status;
                model.Message = result.Message;
                if (search == null)
                {
                    model.Data = data.ToPagedList<ServiceLocationViewModel>(pageNumber, 5);
                }
                else
                {
                    model.Data = data.Where(d => d.Name.ToLower().Contains(search.ToLower())).ToList().ToPagedList(pageNumber, 5);
                }
            }
            else
            {
                model.Status = false;
                model.Message = result.Message;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Location(long? id)
        {
            if (!id.HasValue)
                return RedirectToAction("index", "servicelocation", new { area = "admin" });

            ViewBag.Id = id.Value;
            var result = await ServiceLocationService.Get(id.Value);
            if (result != null && result.Status)
                return View(result);
            else
            {
                var response = this.ResponseHelper.GetResponse<ServiceLocationViewModel>();
                return View(Response);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult> Add(long? id)
        {
            if (!id.HasValue)
                return RedirectToAction("index", "servicelocation", new { area = "admin" });
            ViewBag.BusinessId = id.Value;

            var response = this.ResponseHelper.GetResponse<ServiceLocationViewModel>();
            response.Data = new ServiceLocationViewModel();
            response.Data.BusinessId = id.Value;

            var Countries = await this.GetCountries();
            ViewBag.CountryId = Countries.Select(s => new SelectListItem()
            {
                Value = Convert.ToString(s.Id),
                Text = s.Name
            });

            var Timezones = await this.GetTimeZone();
            ViewBag.TimezoneId = Timezones.Select(s => new SelectListItem()
            {
                Value = Convert.ToString(s.Id),
                Text = s.Title
            });

            response.Status = true;
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "Data")]ResponseViewModel<ServiceLocationViewModel> model)
        {
            var result = new ResponseViewModel<ServiceLocationViewModel>();

            if (!ModelState.IsValid)
            {
                var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                result.Status = false;
                result.Message = errMessage;
            }
            else
            {
                var response = await this.ServiceLocationService.Add(model.Data);
                if (response == null)
                {
                    result.Status = false;
                    result.Message = response != null ? response.Message : "There was a problem. Please try again later.";
                }
                else
                {
                    result.Status = response.Status;
                    result.Message = response.Message;
                }
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public async Task<ActionResult> Edit(long? id)
        {
            if (!id.HasValue)
                return RedirectToAction("index", "servicelocation", new { area = "admin" });

            ViewBag.ServiceLocationId = id.Value;

            var response = await this.ServiceLocationService.Get(id.Value);

            if (response != null)
            {
                if (response.Status)
                {
                    var Countries = await this.GetCountries();
                    ViewBag.CountryId = Countries.Select(s => new SelectListItem()
                    {
                        Value = Convert.ToString(s.Id),
                        Text = s.Name,
                        Selected = (response.Data.CountryId == s.Id) ? true : false
                    });

                    var Timezones = await this.GetTimeZone();
                    ViewBag.TimezoneId = Timezones.Select(s => new SelectListItem()
                    {
                        Value = Convert.ToString(s.Id),
                        Text = s.Title,
                        Selected = (response.Data.TimezoneId == s.Id) ? true : false
                    });

                    response.Status = true;
                    return View(response);
                }

            }
            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Data")]ResponseViewModel<ServiceLocationViewModel> model)
        {
            var result = new ResponseViewModel<ServiceLocationViewModel>();

            if (!ModelState.IsValid)
            {
                var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                result.Status = false;
                result.Message = errMessage;
            }
            else
            {
                var response = await this.ServiceLocationService.Update(model.Data);
                if (response == null)
                {
                    result.Status = false;
                    result.Message = response != null ? response.Message : "There was a problem. Please try again later.";
                }
                else
                {
                    result.Status = response.Status;
                    result.Message = response.Message;
                }
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(long? id)
        {
            if (!id.HasValue)
                return RedirectToAction("index", "servicelocation", new { area = "admin" });

            ViewBag.ServiceLocationId = id.Value;

            var response = await this.ServiceLocationService.Get(id.Value);

            if (response != null)
            {
                if (response.Status)
                {
                    response.Status = true;
                    return View(response);
                }
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind(Include = "Data")]ResponseViewModel<ServiceLocationViewModel> model)
        {
            var result = new ResponseViewModel<ServiceLocationViewModel>();

            var response = await this.ServiceLocationService.Delete(model.Data.Id);
            if (response == null)
            {
                result.Status = false;
                result.Message = response != null ? response.Message : "There was a problem. Please try again later.";
            }
            else
            {
                result.Status = response.Status;
                result.Message = response.Message;
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Hour(long? id)
        {
            if (!id.HasValue)
                return RedirectToAction("index", "servicelocation", new { area = "admin" });

            ViewBag.Id = id.Value;
            var result = await this.BusinessHourService.Gets(id.Value, TableType.ServiceLocationId);
            if (result != null && result.Status)
                return View(result);
            else
            {
                var response = this.ResponseHelper.GetResponse<List<BusinessHourViewModel>>();
                return View(Response);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Holiday(long? id)
        {
            if (!id.HasValue)
                return RedirectToAction("index", "servicelocation", new { area = "admin" });
            ViewBag.Id = id.Value;
            var result = await this.BusinessHolidayService.Gets(id.Value, TableType.ServiceLocationId);
            if (result != null && result.Status)
                return View(result);
            else
            {
                var response = this.ResponseHelper.GetResponse<List<BusinessHolidayViewModel>>();
                return View(Response);
            }
        }

        /// <summary>
        /// To get the list of countries in the database using web api call.
        /// </summary>
        /// <returns>Task of country list.</returns>
        [NonAction]
        private async Task<List<CountryViewModel>> GetCountries()
        {
            var response = await this.CountryService.Gets();
            if (response != null)
            {
                return response.Data;
            }
            else
            {
                return new List<CountryViewModel>();
            }
        }

        [NonAction]
        private async Task<List<TimezoneViewModel>> GetTimeZone()
        {
            var response = await this.TimezoneService.Gets();
            if (response != null)
            {
                return response.Data;
            }
            else
            {
                return new List<TimezoneViewModel>();
            }
        }


    }
}