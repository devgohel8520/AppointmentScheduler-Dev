using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Services
{
    public class DashboardService : AppointmentBaseService
    {
        public DashboardService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<IEnumerable<AdministratorViewModel>> GetAdmins()
        {
            var model = new List<AdministratorViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_ADMINS);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                model = await base.GetHttpResponseList<AdministratorViewModel>(response);
            }
            catch
            {
                model = null;
            }
            return model;
        }

        public async Task<IEnumerable<CountryViewModel>> GetCountries()
        {
            var model = new List<CountryViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_COUNTRIES);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                model = await base.GetHttpResponseList<CountryViewModel>(response);
            }
            catch
            {
                model = null;
            }
            return model;
        }

        public async Task<IEnumerable<TimezoneViewModel>> GetTimezones()
        {
            var model = new List<TimezoneViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_TIMEZONES);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                model = await base.GetHttpResponseList<TimezoneViewModel>(response);
            }
            catch
            {
                model = null;
            }
            return model;
        }

        public async Task<IEnumerable<MembershipViewModel>> GetMemberships()
        {
            var model = new List<MembershipViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_MEMBERSHIPS);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                model = await base.GetHttpResponseList<MembershipViewModel>(response);
            }
            catch
            {
                model = null;
            }
            return model;
        }

        public async Task<IEnumerable<BusinessCategoryViewModel>> GetBusinessCategories()
        {
            var model = new List<BusinessCategoryViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_BUSINESSCATEGORIES);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                model = await base.GetHttpResponseList<BusinessCategoryViewModel>(response);
            }
            catch
            {
                model = null;
            }
            return model;
        }
    }
}
