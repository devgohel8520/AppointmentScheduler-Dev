using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using App.Schedule.Web.Admin.Models;

namespace App.Schedule.Web.Admin.Controllers
{
    public class DashboardController : DashboardBaseController
    {
        public ActionResult Logout()
        {
            if (Request.Cookies["aappointment"] != null)
            {
                var admin = new HttpCookie("aappointment");
                Session["aEmail"] = "";
                admin.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(admin);
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Index()
        {
            var model = new DashboardViewModel();
            try
            {
                Session["HomeLink"] = "Dashboard";
                if (admin != null)
                {
                    var admins = await this.DashboardService.GetAdmins();
                    var countries = await this.DashboardService.GetCountries();
                    var timezones = await this.DashboardService.GetTimezones();
                    var memberships = await this.DashboardService.GetMemberships();
                    var businessCategories = await this.DashboardService.GetBusinessCategories();
                    if (admins != null)
                    {
                        model.AdminsCount = admins.Count();
                    }
                    if (countries != null)
                    {
                        model.CountryCount = countries.Count();
                    }
                    if (timezones != null)
                    {
                        model.TimezonCount = timezones.Count();
                    }
                    if (memberships != null)
                    {
                        model.MembershipCount = memberships.Count();
                    }
                    if (businessCategories != null)
                    {
                        model.BusinessCategoryCount = businessCategories.Count();
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
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
    }
}