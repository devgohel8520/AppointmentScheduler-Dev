using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Services
{
    public class CountryService : AppointmentUserBaseService, IAppointmentUserService<CountryViewModel>
    {
        public CountryService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<List<CountryViewModel>>> Gets()
        {
            var returnResponse = new ResponseViewModel<List<CountryViewModel>>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_COUNTRIES);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<CountryViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public Task<ResponseViewModel<CountryViewModel>> Find(Predicate<CountryViewModel> pridict)
        {
            return null;
        }

        public Task<ResponseViewModel<CountryViewModel>> Get(long? id)
        {
            return null;
        }
        
        public Task<ResponseViewModel<CountryViewModel>> Add(CountryViewModel model)
        {
            return null;
        }

        public Task<ResponseViewModel<CountryViewModel>> Deactive(long? id, bool status)
        {
            return null;
        }

        public Task<ResponseViewModel<CountryViewModel>> Delete(long? id)
        {
            return null;
        }
        
        public Task<ResponseViewModel<CountryViewModel>> Update(CountryViewModel model)
        {
            return null;
        }
    }
}
