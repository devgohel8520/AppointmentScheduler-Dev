using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Services
{
    public class TimezoneService : AppointmentUserBaseService, IAppointmentUserService<TimezoneViewModel>
    {
        public Task<ResponseViewModel<TimezoneViewModel>> Find(Predicate<TimezoneViewModel> pridict)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<TimezoneViewModel>> Get(long? id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel<List<TimezoneViewModel>>> Gets()
        {
            var returnResponse = new ResponseViewModel<List<TimezoneViewModel>>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_TIMEZONES);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<TimezoneViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public Task<ResponseViewModel<TimezoneViewModel>> Update(TimezoneViewModel model)
        {
            throw new NotImplementedException();
        }

        public TimezoneService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public Task<ResponseViewModel<TimezoneViewModel>> Add(TimezoneViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<TimezoneViewModel>> Deactive(long? id, bool status)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<TimezoneViewModel>> Delete(long? id)
        {
            throw new NotImplementedException();
        }
    }
}
