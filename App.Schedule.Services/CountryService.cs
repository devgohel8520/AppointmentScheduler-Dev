using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Services
{
    public class CountryService : AppointmentBaseService, IAppointmentService<CountryViewModel>
    {
        public CountryService(string token)
        {
            base.SetUpAppointmentService(token);
        }
             
        public async Task<ResponseViewModel<CountryViewModel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<CountryViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_COUNTRY_BYID, id);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<CountryViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<List<CountryViewModel>>> Gets()
        {
            var returnResponse = new ResponseViewModel<List<CountryViewModel>>();
            try
            {
                var url = String.Format(AppointmentService.GET_COUNTRIES);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<CountryViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<CountryViewModel>> Add(CountryViewModel model)
        {
            var returnResponse = new ResponseViewModel<CountryViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.POST_COUNTRY);
                var response = await this.appointmentService.httpClient.PostAsync(url, content);
                returnResponse = await base.GetHttpResponse<CountryViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<CountryViewModel>> Update(CountryViewModel model)
        {
            var returnResponse = new ResponseViewModel<CountryViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.PUT_COUNTRY, model.Id);
                var response = await this.appointmentService.httpClient.PutAsync(url, content);
                returnResponse = await base.GetHttpResponse<CountryViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<CountryViewModel>> Delete(long? id)
        {
            var returnResponse = new ResponseViewModel<CountryViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please enter a valid country id.";
                }
                else
                {
                    var url = String.Format(AppointmentService.DELETE_COUNTRY, id.Value);
                    var response = await this.appointmentService.httpClient.DeleteAsync(url);
                    returnResponse = await base.GetHttpResponse<CountryViewModel>(response);
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public Task<ResponseViewModel<CountryViewModel>> Deactive(long? id, bool status)
        {
            return Task.FromResult(new ResponseViewModel<CountryViewModel>());
        }
    }
}
