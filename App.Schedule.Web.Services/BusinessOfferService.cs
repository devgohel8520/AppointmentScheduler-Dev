using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Services
{
    public class BusinessOfferService : AppointmentUserBaseService, IAppointmentUserService<BusinessOfferViewModel>
    {
        public BusinessOfferService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public Task<ResponseViewModel<BusinessOfferViewModel>> Find(Predicate<BusinessOfferViewModel> pridict)
        {
            return null;
        }

        public async Task<ResponseViewModel<BusinessOfferViewModel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<BusinessOfferViewModel>()
            {
                Status = false,
                Message = "",
                Data = new BusinessOfferViewModel()
            };
            try
            {
                var url = String.Format(AppointmentUserService.GET_BUSINESSOFFERBYID, id);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                var result = await base.GetHttpResponse<BusinessOfferViewModel>(response);

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

        public async Task<ResponseViewModel<List<BusinessOfferViewModel>>> Gets()
        {
            var returnResponse = new ResponseViewModel<List<BusinessOfferViewModel>>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_BUSINESSOFFER);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<BusinessOfferViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<List<BusinessOfferViewModel>>> Gets(long? id, TableType type)
        {
            var returnResponse = new ResponseViewModel<List<BusinessOfferViewModel>>();
            try
            {
                var url = String.Format(AppointmentUserService.GETS_BUSINESSOFFERBYIDANDTYPE, id.Value, (int)type);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<BusinessOfferViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }
       
        public async Task<ResponseViewModel<BusinessOfferViewModel>> Add(BusinessOfferViewModel model)
        {
            var returnResponse = new ResponseViewModel<BusinessOfferViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.POST_BUSINESSOFFER);
                var response = await this.appointmentUserService.httpClient.PostAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessOfferViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessOfferViewModel>> Deactive(long? id, bool status)
        {
            var returnResponse = new ResponseViewModel<BusinessOfferViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please enter a valid offer id.";
                }
                else
                {
                    var url = String.Format(AppointmentUserService.DEACTIVE_BUSINESSOFFERBYIDANDSTATUS, id.Value,status);
                    var response = await this.appointmentUserService.httpClient.DeleteAsync(url);
                    returnResponse = await base.GetHttpResponse<BusinessOfferViewModel>(response);
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessOfferViewModel>> Delete(long? id)
        {
            var returnResponse = new ResponseViewModel<BusinessOfferViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please enter a valid offer id.";
                }
                else
                {
                    var url = String.Format(AppointmentUserService.DELETE_BUSINESSOFFERBYID, id.Value);
                    var response = await this.appointmentUserService.httpClient.DeleteAsync(url);
                    returnResponse = await base.GetHttpResponse<BusinessOfferViewModel>(response);
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessOfferViewModel>> Update(BusinessOfferViewModel model)
        {
            var returnResponse = new ResponseViewModel<BusinessOfferViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.PUT_BUSINESSOFFERBYID, model.Id);
                var response = await this.appointmentUserService.httpClient.PutAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessOfferViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }
    }
}
