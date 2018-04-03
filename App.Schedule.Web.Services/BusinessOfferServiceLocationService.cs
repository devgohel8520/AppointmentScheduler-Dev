using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace App.Schedule.Web.Services
{
    public class BusinessOfferServiceLocationService : AppointmentUserBaseService, IAppointmentUserService<BusinessOfferServiceLocationViewModel>
    {
        public BusinessOfferServiceLocationService(string token)
        {
            this.SetUpAppointmentService(token);
        }

        public Task<ResponseViewModel<BusinessOfferServiceLocationViewModel>> Find(Predicate<BusinessOfferServiceLocationViewModel> pridict)
        {
            return null;
        }

        public async Task<ResponseViewModel<BusinessOfferServiceLocationViewModel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<BusinessOfferServiceLocationViewModel>()
            {
                Status = false,
                Message = "",
                Data = new BusinessOfferServiceLocationViewModel()
            };
            try
            {
                var url = String.Format(AppointmentUserService.GET_BUSINESSOFFERSERVICELOCATIONBYID, id);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                var result = await base.GetHttpResponse<BusinessOfferServiceLocationViewModel>(response);

                returnResponse.Status = result.Status;
                returnResponse.Message = result.Message;
                returnResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<List<BusinessOfferServiceLocationViewModel>>> Gets(long? id)
        {
            var returnResponse = new ResponseViewModel<List<BusinessOfferServiceLocationViewModel>>();
            try
            {
                var url = String.Format(AppointmentUserService.GETS_BUSINESSOFFERSERVICELOCATION, id.Value, "all");
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<BusinessOfferServiceLocationViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public Task<ResponseViewModel<List<BusinessOfferServiceLocationViewModel>>> Gets()
        {
            return null;
        }

        public async Task<ResponseViewModel<BusinessOfferServiceLocationViewModel>> Add(BusinessOfferServiceLocationViewModel model)
        {
            var returnResponse = new ResponseViewModel<BusinessOfferServiceLocationViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.POST_BUSINESSOFFERSERVICELOCATION);
                var response = await this.appointmentUserService.httpClient.PostAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessOfferServiceLocationViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessOfferServiceLocationViewModel>> Update(BusinessOfferServiceLocationViewModel model)
        {
            var returnResponse = new ResponseViewModel<BusinessOfferServiceLocationViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.PUT_BUSINESSOFFERSERVICELOCATION, model.Id);
                var response = await this.appointmentUserService.httpClient.PutAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessOfferServiceLocationViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public Task<ResponseViewModel<BusinessOfferServiceLocationViewModel>> Deactive(long? id, bool status)
        {
            return null;
        }

        public async Task<ResponseViewModel<BusinessOfferServiceLocationViewModel>> Delete(long? id)
        {
            var returnResponse = new ResponseViewModel<BusinessOfferServiceLocationViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please enter a valid offer id.";
                }
                else
                {
                    var url = String.Format(AppointmentUserService.DELETE_BUSINESSOFFERSERVICELOCATION, id.Value);
                    var response = await this.appointmentUserService.httpClient.DeleteAsync(url);
                    returnResponse = await base.GetHttpResponse<BusinessOfferServiceLocationViewModel>(response);
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }
    }
}
