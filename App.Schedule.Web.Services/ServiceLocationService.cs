using System;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Services
{
    public class ServiceLocationService : AppointmentUserBaseService, IAppointmentUserService<ServiceLocationViewModel>
    {
        public ServiceLocationService(string token)
        {
            this.SetUpAppointmentService(token);
        }

        public Task<ResponseViewModel<ServiceLocationViewModel>> Find(Predicate<ServiceLocationViewModel> pridict)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel<ServiceLocationViewModel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<ServiceLocationViewModel>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_SERVICELOCATIONBYID, id);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<ServiceLocationViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<List<ServiceLocationViewModel>>> Gets(long? id, TableType type)
        {
            var returnResponse = new ResponseViewModel<List<ServiceLocationViewModel>>();
            if (id.HasValue)
            {
                try
                {
                    var url = String.Format(AppointmentUserService.GET_SERVICELOCATIONBYIDANDTYPE, id.Value, type);
                    var response = await this.appointmentUserService.httpClient.GetAsync(url);
                    returnResponse = await base.GetHttpResponse<List<ServiceLocationViewModel>>(response);
                }
                catch (Exception ex)
                {
                    returnResponse.Data = null;
                    returnResponse.Message = "Reason: " + ex.Message.ToString();
                    returnResponse.Status = false;
                }
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<List<ServiceLocationViewModel>>> Gets()
        {
            var returnResponse = new ResponseViewModel<List<ServiceLocationViewModel>>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_SERVICELOCATION);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<ServiceLocationViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<ServiceLocationViewModel>> Add(ServiceLocationViewModel model)
        {
            var returnResponse = new ResponseViewModel<ServiceLocationViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.POST_SERVICELOCATION);
                var response = await this.appointmentUserService.httpClient.PostAsync(url, content);
                returnResponse = await base.GetHttpResponse<ServiceLocationViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<ServiceLocationViewModel>> Deactive(long? id, bool status)
        {
            var returnResponse = new ResponseViewModel<ServiceLocationViewModel>();
            try
            {
                var url = String.Format(AppointmentUserService.DELETE_SERVICELOCATIONBYID, id, DeleteType.UpdateStatus);
                var response = await this.appointmentUserService.httpClient.DeleteAsync(url);
                returnResponse = await base.GetHttpResponse<ServiceLocationViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<ServiceLocationViewModel>> Delete(long? id)
        {
            var returnResponse = new ResponseViewModel<ServiceLocationViewModel>();
            try
            {
                var url = String.Format(AppointmentUserService.DELETE_SERVICELOCATIONBYID, id, DeleteType.DeleteRecord);
                var response = await this.appointmentUserService.httpClient.DeleteAsync(url);
                returnResponse = await base.GetHttpResponse<ServiceLocationViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }
        
        public async Task<ResponseViewModel<ServiceLocationViewModel>> Update(ServiceLocationViewModel model)
        {
            var returnResponse = new ResponseViewModel<ServiceLocationViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.PUT_SERVICELOCATION,model.Id);
                var response = await this.appointmentUserService.httpClient.PutAsync(url, content);
                returnResponse = await base.GetHttpResponse<ServiceLocationViewModel>(response);
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
