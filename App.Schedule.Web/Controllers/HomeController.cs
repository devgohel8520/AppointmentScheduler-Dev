using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;
using App.Schedule.Web.Areas.Admin.Controllers;

namespace App.Schedule.Web.Controllers
{
    /// <summary>
    /// Home controller is used to interact with home, login, register, contatct pages.
    /// </summary>
    public class HomeController : HomeBaseController
    {
        /// <summary>
        /// Get method to display home page for user.
        /// </summary>
        /// <returns>Actionre result class.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get method to display login page.
        /// </summary>
        /// <returns>Action result class.</returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Get method to display register UI page.
        /// </summary>
        /// <returns>Task of action result.</returns>
        [HttpGet]
        public async Task<ActionResult> Register()
        {
            var result = new ResponseViewModel<RegisterViewModel>();
            try
            {
                var Countries = await this.GetCountries();
                ViewBag.CountryId = Countries.Select(s => new SelectListItem()
                {
                    Value = Convert.ToString(s.Id),
                    Text = s.Name
                });
                
                var BusinessCategories = await this.GetBusinessCategories();
                var parentCategories = BusinessCategories.ToDictionary(d => d.Id, d => d.Name);
                var groupCategories = BusinessCategories.Select(s => s.Name).Select(ss => new SelectListGroup() { Name = ss }).ToList();

                var childCategories = (from c in BusinessCategories
                                       join p in BusinessCategories
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

                var Memberships = await this.GetMemberships();
                ViewBag.MembershipId = Memberships.Select(s => new SelectListItem()
                {
                    Value = Convert.ToString(s.Id),
                    Text = s.Title
                });

                var Timezones = await this.GetTimeZone();
                ViewBag.TimezoneId = Timezones.Select(s => new SelectListItem()
                {
                    Value = Convert.ToString(s.Id),
                    Text = s.Title
                });

                return View(result);
            }
            catch(Exception ex)
            {
                result.Status = true;
                result.Message = ex.Message.ToString();
                return View(result);
            }
        }

        /// <summary>
        /// Post method to register Business admin.
        /// </summary>
        /// <param name="model">Provide the business, admin service location and timezone information.</param>
        /// <returns>Task of action result.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include ="Data")]ServiceDataViewModel<RegisterViewModel> model)
        {
            var result = new ResponseViewModel<string>();

            if (!ModelState.IsValid)
            {
                var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                result.Status = false;
                result.Message = errMessage;
            }
            else
            {
                var response = await this.BusinessService.Add(model.Data);
                if (response.Status)
                {
                    result.Status = true;
                    result.Message = response.Message;
                }
                else
                {
                    result.Status = false;
                    result.Message = response!=null ? response.Message : "There was a problem. Please try again later.";
                }
            }
            return Json(new { status = result.Status, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
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

        /// <summary>
        /// To get the list of membership in the database using web api call.
        /// </summary>
        /// <returns>Task of membership list.</returns>
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