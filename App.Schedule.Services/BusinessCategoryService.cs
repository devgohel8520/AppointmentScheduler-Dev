using System;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Services
{
    public class BusinessCategoryService : AppointmentBaseService, IAppointmentService<BusinessCategoryViewModel>
    {
        public BusinessCategoryService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<BusinessCategoryViewModel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<BusinessCategoryViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_BUSINESSCATEGORY_BYID, id);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<BusinessCategoryViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<List<BusinessCategoryViewModel>>> Gets()
        {
            var returnResponse = new ResponseViewModel<List<BusinessCategoryViewModel>>();
            try
            {
                var url = String.Format(AppointmentService.GET_BUSINESSCATEGORIES);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<BusinessCategoryViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessCategoryViewModel>> Add(BusinessCategoryViewModel model)
        {
            var returnResponse = new ResponseViewModel<BusinessCategoryViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.POST_BUSINESSCATEGORY);
                var response = await this.appointmentService.httpClient.PostAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessCategoryViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessCategoryViewModel>> Update(BusinessCategoryViewModel model)
        {
            var returnResponse = new ResponseViewModel<BusinessCategoryViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.PUT_BUSINESSCATEGORY_BYID, model.Id);
                var response = await this.appointmentService.httpClient.PutAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessCategoryViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessCategoryViewModel>> Delete(long? id)
        {
            var returnResponse = new ResponseViewModel<BusinessCategoryViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please provide a valid admin id.";
                }
                else
                {
                    var url = String.Format(AppointmentService.DELETE_BUSINESSCATEGORY, id.Value,false,DeleteType.DeleteRecord);
                    var response = await this.appointmentService.httpClient.DeleteAsync(url);
                    returnResponse = await base.GetHttpResponse<BusinessCategoryViewModel>(response);
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessCategoryViewModel>> Deactive(long? id, bool status)
        {
            var returnResponse = new ResponseViewModel<BusinessCategoryViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please provide a valid admin id.";
                }
                else
                {
                    var url = String.Format(AppointmentService.DELETE_BUSINESSCATEGORY, id.Value,status,DeleteType.UpdateStatus);
                    var response = await this.appointmentService.httpClient.DeleteAsync(url);
                    returnResponse = await base.GetHttpResponse<BusinessCategoryViewModel>(response);
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }
    }
}
