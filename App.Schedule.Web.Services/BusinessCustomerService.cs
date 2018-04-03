using System;
using System.Web;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Services
{
    public class BusinessCustomerService : AppointmentUserBaseService, IAppointmentUserService<BusinessCustomerViewMdoel>
    {
        public BusinessCustomerService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<BusinessCustomerViewMdoel>> VerifyLoginCredential(string Email, string Password)
        {
            var returnResponse = new ResponseViewModel<BusinessCustomerViewMdoel>();
            try
            {
                Password = HttpContext.Current.Server.UrlEncode(Password);
                var url = String.Format(AppointmentUserService.GET_BUSINESS_CUSTOMER_BYLOGINID, Email, Password);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<BusinessCustomerViewMdoel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<string>> VerifyAngGetAccessToken(string Email, string Password)
        {
            var returnResponse = new ResponseViewModel<string>();
            try
            {
                var model = "username=" + Email + "&password=" + Password + "&grant_type=password";
                var content = new StringContent(model, Encoding.UTF8, "text/plain");
                var url = String.Format(AppointmentUserService.GET_ADMIN_TOKEN);
                var response = await this.appointmentUserService.httpClient.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                dynamic res = JsonConvert.DeserializeObject(result);
                if (res != null)
                {
                    var error = (string)res.error;
                    if (res.error != null && error.Contains("invalid"))
                    {
                        returnResponse.Status = false;
                        returnResponse.Data = null;
                        returnResponse.Message = "Please check your id and password";
                        return returnResponse;
                    }
                    returnResponse.Status = true;
                    returnResponse.Data = res.access_token;
                    returnResponse.Message = "Success";
                    return returnResponse;
                }
                else
                {
                    returnResponse.Status = false;
                    returnResponse.Data = null;
                    returnResponse.Message = "There was a problem. Please try agian later.";
                    return returnResponse;
                }
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = ex.Message.ToString();
                returnResponse.Status = false;
                return returnResponse;
            }
        }

        public Task<ResponseViewModel<BusinessCustomerViewMdoel>> Find(Predicate<BusinessCustomerViewMdoel> pridict)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel<BusinessCustomerViewMdoel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<BusinessCustomerViewMdoel>()
            {
                Status = false,
                Message = "",
                Data = new BusinessCustomerViewMdoel()
            };
            try
            {
                var url = String.Format(AppointmentUserService.GET_BUSINESS_CUSTOMERBYID, id);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                var result = await base.GetHttpResponse<BusinessCustomerViewMdoel>(response);

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

        public Task<ResponseViewModel<List<BusinessCustomerViewMdoel>>> Gets()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel<List<BusinessCustomerViewMdoel>>> Gets(long? id, TableType type)
        {
            var returnResponse = new ResponseViewModel<List<BusinessCustomerViewMdoel>>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_BUSINESS_CUSTOMERBYIDANDTYPE, id, (int)type);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    returnResponse = await base.GetHttpResponse<List<BusinessCustomerViewMdoel>>(response);
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

        public async Task<ResponseViewModel<BusinessCustomerViewMdoel>> Add(BusinessCustomerViewMdoel model)
        {
            var returnResponse = new ResponseViewModel<BusinessCustomerViewMdoel>();
            try
            {
                var registerModel = new UserViewModel()
                {
                    UserType = UserType.BusinessCustomer,
                    BusinessCustomer = model
                };
                var jsonContent = JsonConvert.SerializeObject(registerModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.POST_API_ACCOUNT_REGISTER);
                var response = await this.appointmentUserService.httpClient.PostAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessCustomerViewMdoel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessCustomerViewMdoel>> Deactive(long? id, bool status)
        {
            var returnResponse = new ResponseViewModel<BusinessCustomerViewMdoel>();
            try
            {
                var url = String.Format(AppointmentUserService.DEACTIVE_BUSINESS_CUSTOMER, id, status);
                var response = await this.appointmentUserService.httpClient.DeleteAsync(url);
                returnResponse = await base.GetHttpResponse<BusinessCustomerViewMdoel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        //Don't forgot to remove token
        public async Task<ResponseViewModel<BusinessCustomerViewMdoel>> Delete(long? id)
        {
            var returnResponse = new ResponseViewModel<BusinessCustomerViewMdoel>();
            try
            {
                var registerModel = new UserViewModel()
                {
                    UserType = UserType.BusinessCustomer,
                    BusinessCustomer = new BusinessCustomerViewMdoel() { Id = id.Value }
                };
                var jsonContent = JsonConvert.SerializeObject(registerModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.DELETE_API_ACCOUNT);
                var response = await this.appointmentUserService.httpClient.PostAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessCustomerViewMdoel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }
        
        public async Task<ResponseViewModel<BusinessCustomerViewMdoel>> Update(BusinessCustomerViewMdoel model)
        {
            var returnResponse = new ResponseViewModel<BusinessCustomerViewMdoel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.PUT_BUSINESS_CUSTOMER, model.Id);
                var response = await this.appointmentUserService.httpClient.PutAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessCustomerViewMdoel>(response);
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
