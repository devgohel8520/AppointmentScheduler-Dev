using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class HourController : HourBaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var model = await this.BusinessHourService.Gets(RegisterViewModel.Employee.ServiceLocationId.Value, TableType.ServiceLocationId);
            model.Status = true;
            if (model.Data == null)
                model.Data = new List<BusinessHourViewModel>();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long? id)
        {
            if (!id.HasValue)
                return RedirectToAction("index", "hour", new { area = "admin" });

            var model = await this.BusinessHourService.Get(id);
            var fromHours = this.GetHoursOfDay();
            model.Status = true;
            ViewBag.FromHours = fromHours.Select(s => new SelectListItem()
            {
                Value = s.Value,
                Text = s.Value
            });
            ViewBag.ToHours = fromHours.Select(s => new SelectListItem()
            {
                Value = s.Value,
                Text = s.Value
            });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Data")] ResponseViewModel<BusinessHourViewModel> model)
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
                    if (CheckSplitDate(model.Data))
                    {
                        var response = await this.BusinessHourService.Update(model.Data);
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
                        result.Message = "Please validate your business time.";
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

        [NonAction]
        private bool CheckSplitDate(BusinessHourViewModel model)
        {
            if (model.IsHoliday)
                return true;

            if (model.From.Hour >= model.To.Hour)
                return false;

            if (model.IsSplit1.HasValue)
            {
                if (model.IsSplit1.Value)
                {
                    if (model.To.Hour >= model.FromSplit1.Value.Hour)
                        return false;

                    if (model.FromSplit1.Value.Hour >= model.ToSplit1.Value.Hour)
                        return false;
                }
            }

            if (model.IsSplit2.HasValue)
            {
                if (model.IsSplit2.Value)
                {
                    if (model.ToSplit1.Value.Hour >= model.FromSplit2.Value.Hour)
                        return false;
                    if (model.FromSplit2.Value.Hour >= model.ToSplit2.Value.Hour)
                        return false;
                }
            }
            return true;
        }

        [NonAction]
        public Dictionary<int, string> GetHoursOfDay()
        {
            var now = DateTime.Now;
            var date = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            var hours = new Dictionary<int, string>();
            for (var i = 1; i <= 48; i++)
            {
                hours.Add(i, date.ToString("hh:mm tt"));
                date = date.AddMinutes(30);
            }
            return hours;
        }
    }
}