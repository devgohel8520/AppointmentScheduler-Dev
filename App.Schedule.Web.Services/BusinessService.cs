using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Services
{
    public class BusinessService : AppointmentUserBaseService, IAppointmentUserService<RegisterViewModel>
    {
        public BusinessService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<RegisterViewModel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<RegisterViewModel>()
            {
                Status = false,
                Message = "",
                Data = new RegisterViewModel()
            };
            try
            {
                var url = String.Format(AppointmentUserService.GET_BUSINESS_BYID, id);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                var result = await base.GetHttpResponse<BusinessViewModel>(response);

                returnResponse.Status = result.Status;
                returnResponse.Message = result.Message;
                returnResponse.Data.Business = result.Data;
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }

            return returnResponse;
        }

        public Task<ResponseViewModel<List<RegisterViewModel>>> Gets()
        {
            return null;
        }

        public async Task<ResponseViewModel<RegisterViewModel>> Add(RegisterViewModel model)
        {
            var returnResponse = new ResponseViewModel<RegisterViewModel>();
            try
            {
                var registerModel = new UserViewModel()
                {
                    UserType = UserType.BusinessAdmin,
                    BusinessAdmin = model
                };
                var jsonContent = JsonConvert.SerializeObject(registerModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.POST_API_ACCOUNT_REGISTER);
                var response = await this.appointmentUserService.httpClient.PostAsync(url, content);
                returnResponse = await base.GetHttpResponse<RegisterViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }
        
        public Task<ResponseViewModel<RegisterViewModel>> Deactive(long? id, bool status)
        {
            return null;
        }

        public Task<ResponseViewModel<RegisterViewModel>> Delete(long? id)
        {
            return null;
        }

        public Task<ResponseViewModel<RegisterViewModel>> Find(Predicate<RegisterViewModel> pridict)
        {
            return null;
        }

        public async Task<ResponseViewModel<RegisterViewModel>> Update(RegisterViewModel model)
        {
            var returnResponse = new ResponseViewModel<RegisterViewModel>()
            {
                Data = new RegisterViewModel()
            };
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model.Business);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.PUT_BUSINESS_BYTPE, model.Business.Id, FieldType.All);
                var response = await this.appointmentUserService.httpClient.PutAsync(url, content);
                var business = await base.GetHttpResponse<BusinessViewModel>(response);
                if(business != null && business.Status)
                {
                    returnResponse.Status = business.Status;
                    returnResponse.Message = business.Message;
                    returnResponse.Data.Business = business.Data;
                }
                else
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please try again later.";
                }
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessViewModel>> Update(FieldType type, BusinessViewModel model)
        {
            var returnResponse = new ResponseViewModel<BusinessViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.PUT_BUSINESS_BYTPE, model.Id, type);
                var response = await this.appointmentUserService.httpClient.PutAsync(url, content);
                var business = await base.GetHttpResponse<BusinessViewModel>(response);
                if (business != null && business.Status)
                {
                    returnResponse.Status = business.Status;
                    returnResponse.Message = business.Message;
                    returnResponse.Data = business.Data;
                }
                else
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please try again later.";
                }
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
