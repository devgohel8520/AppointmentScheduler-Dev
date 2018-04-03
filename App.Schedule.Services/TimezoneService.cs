using System;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Services
{
    public class TimezoneService : AppointmentBaseService, IAppointmentService<TimezoneViewModel>
    {
        public TimezoneService(string token)
        {
            base.SetUpAppointmentService(token);
        }
     
        public async Task<ResponseViewModel<TimezoneViewModel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_TIMEZONE_BYID, id);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<TimezoneViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<List<TimezoneViewModel>>> Gets()
        {
            var returnResponse = new ResponseViewModel<List<TimezoneViewModel>>();
            try
            {
                var url = String.Format(AppointmentService.GET_TIMEZONES);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<TimezoneViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<TimezoneViewModel>> Add(TimezoneViewModel model)
        {
            var returnResponse = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.POST_TIMEZONE);
                var response = await this.appointmentService.httpClient.PostAsync(url, content);
                returnResponse = await base.GetHttpResponse<TimezoneViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<TimezoneViewModel>> Update(TimezoneViewModel model)
        {
            var returnResponse = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.PUT_TIMEZONE, model.Id);
                var response = await this.appointmentService.httpClient.PutAsync(url, content);
                returnResponse = await base.GetHttpResponse<TimezoneViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<TimezoneViewModel>> Delete(long? id)
        {
            var returnResponse = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please enter a valid timezone id.";
                }
                else
                {
                    var url = String.Format(AppointmentService.DELETE_TIMEZONE, id.Value);
                    var response = await this.appointmentService.httpClient.DeleteAsync(url);
                    returnResponse = await base.GetHttpResponse<TimezoneViewModel>(response);
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public Task<ResponseViewModel<TimezoneViewModel>> Deactive(long? id, bool status)
        {
            return Task.FromResult(new ResponseViewModel<TimezoneViewModel>());
        }
    }
}
