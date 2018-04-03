using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Areas.Admin.Controllers
{
    public class ProfileController : ProfileBaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var result = await BusinessService.Get(RegisterViewModel.Business.Id);
            if (result != null && result.Status)
            {
                var response = new ResponseViewModel<BusinessViewModel>()
                {
                    Status = result.Status,
                    Message = result.Message,
                    Data = result.Data.Business
                };

                //get countries and set to dropdown list
                var Countries = await this.GetCountries();
                ViewBag.CountryId = Countries.Select(s => new SelectListItem()
                {
                    Value = Convert.ToString(s.Id),
                    Text = s.Name
                });

                //get timezones and set to dropdown list
                var Timezones = await this.GetTimeZone();
                ViewBag.TimezoneId = Timezones.Select(s => new SelectListItem()
                {
                    Value = Convert.ToString(s.Id),
                    Text = s.Title
                });

                //get business category and set to dropdown list
                var businessCategories = await this.GetBusinessCategories();
                var parentCategories = businessCategories.ToDictionary(d => d.Id, d => d.Name);
                var groupCategories = businessCategories.Select(s => s.Name).Select(ss => new SelectListGroup() { Name = ss }).ToList();

                var childCategories = (from c in businessCategories
                                       join p in businessCategories
                                       on c.ParentId equals p.Id
                                       select new
                                       {
                                           Id = c.Id,
                                           Text = c.Name,
                                           ParentId = c.ParentId
                                       }).ToList();

                var groupedData = childCategories
                                       .Where(f => f.ParentId != 0)
                                       .Select(x => new SelectListItem
                                       {
                                           Value = x.Id.ToString(),
                                           Text = x.Text,
                                           Group = groupCategories.First(a => a.Name == parentCategories[x.ParentId.Value])
                                       }).ToList();

                ViewBag.BusinessCategoryId = groupedData;

                return View(response);
            }
            else
            {
                var response = this.ResponseHelper.GetResponse<BusinessViewModel>();
                return View(Response);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Data")] ResponseViewModel<BusinessViewModel> model)
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
                    var registerviewModel = new RegisterViewModel()
                    {
                        Business = model.Data,
                        Employee = null
                    };
                    var response = await this.BusinessService.Update(registerviewModel);
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

        /// <summary>
        /// To get the list of Timezones in the database using web api call.
        /// </summary>
        /// <returns>Task of timezone list.</returns>
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

        /// <summary>
        /// To get the list of business category in the database using web api call.
        /// </summary>
        /// <returns>Task of business category list.</returns>
        [NonAction]
        private async Task<List<BusinessCategoryViewModel>> GetBusinessCategories()
        {
            var response = await this.BusinessCategoryService.Gets();
            if (response != null)
            {
                return response.Data;
            }
            else
            {
                return new List<BusinessCategoryViewModel>();
            }
        }

    }
}