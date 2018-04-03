using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Services
{
    public class BusinessHolidayService : AppointmentUserBaseService, IAppointmentUserService<BusinessHolidayViewModel>
    {
        public BusinessHolidayService(string token)
        {
            this.SetUpAppointmentService(token);
        }

        public Task<ResponseViewModel<BusinessHolidayViewModel>> Find(Predicate<BusinessHolidayViewModel> pridict)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel<BusinessHolidayViewModel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<BusinessHolidayViewModel>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_BUSINESSHOLIDAYBYId, id.Value);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<BusinessHolidayViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<List<BusinessHolidayViewModel>>> Gets(long id, TableType type)
        {
            var returnResponse = new ResponseViewModel<List<BusinessHolidayViewModel>>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_BUSINESSHOLIDAYSBYTYPE, id, type);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<BusinessHolidayViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<List<BusinessHolidayViewModel>>> Gets()
        {
            var returnResponse = new ResponseViewModel<List<BusinessHolidayViewModel>>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_BUSINESSHOURSBYTYPE);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<BusinessHolidayViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessHolidayViewModel>> Add(BusinessHolidayViewModel model)
        {
            var returnResponse = new ResponseViewModel<BusinessHolidayViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.POST_BUSINESSHOLIDAY);
                var response = await this.appointmentUserService.httpClient.PostAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessHolidayViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public Task<ResponseViewModel<BusinessHolidayViewModel>> Deactive(long? id, bool status)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel<BusinessHolidayViewModel>> Delete(long? id)
        {
            var returnResponse = new ResponseViewModel<BusinessHolidayViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please enter a valid holiday id.";
                }
                else
                {
                    var url = String.Format(AppointmentUserService.DELETE_BUSINESSHOLIDAYBYId, id.Value);
                    var response = await this.appointmentUserService.httpClient.DeleteAsync(url);
                    returnResponse = await base.GetHttpResponse<BusinessHolidayViewModel>(response);
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessHolidayViewModel>> Update(BusinessHolidayViewModel model)
        {
            var returnResponse = new ResponseViewModel<BusinessHolidayViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.PUT_BUSINESSHOLIDAY, model.Id);
                var response = await this.appointmentUserService.httpClient.PutAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessHolidayViewModel>(response);
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
