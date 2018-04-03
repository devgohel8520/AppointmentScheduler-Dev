using System;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Services
{
    public class BusinessHourService : AppointmentUserBaseService, IAppointmentUserService<BusinessHourViewModel>
    {
        public BusinessHourService(string token)
        {
            this.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<List<BusinessHourViewModel>>> Gets(long id, TableType type)
        {
            var returnResponse = new ResponseViewModel<List<BusinessHourViewModel>>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_BUSINESSHOURSBYTYPE, id, type);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<BusinessHourViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public Task<ResponseViewModel<BusinessHourViewModel>> Find(Predicate<BusinessHourViewModel> pridict)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel<BusinessHourViewModel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<BusinessHourViewModel>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_BUSINESSHOURSBYId, id);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<BusinessHourViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<List<BusinessHourViewModel>>> Gets()
        {
            var returnResponse = new ResponseViewModel<List<BusinessHourViewModel>>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_BUSINESSHOURSBYTYPE, 0, TableType.None);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<BusinessHourViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public Task<ResponseViewModel<BusinessHourViewModel>> Add(BusinessHourViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<BusinessHourViewModel>> Deactive(long? id, bool status)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<BusinessHourViewModel>> Delete(long? id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel<BusinessHourViewModel>> Update(BusinessHourViewModel model)
        {
            var returnResponse = new ResponseViewModel<BusinessHourViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.PUT_BUSINESSHOUR, model.Id);
                var response = await this.appointmentUserService.httpClient.PutAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessHourViewModel>(response);
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
