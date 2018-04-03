using System;
using System.Text;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Services
{
    public class BusinessEmployeeService : AppointmentUserBaseService, IAppointmentUserService<BusinessEmployeeViewModel>
    {
        public BusinessEmployeeService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<RegisterViewModel>> VerifyLoginCredential(string Email, string Password)
        {
            var returnResponse = new ResponseViewModel<RegisterViewModel>();
            try
            {
                Password = HttpContext.Current.Server.UrlEncode(Password);
                var url = String.Format(AppointmentUserService.GET_BUSINESS_EMP_BYLOGINID, Email, Password);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
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

        public async Task<ResponseViewModel<string>> VerifyAndGetAdminAccessToken(string Email, string Password)
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

        public async Task<ResponseViewModel<BusinessEmployeeViewModel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<BusinessEmployeeViewModel>()
            {
                Status = false,
                Message = "",
                Data = new BusinessEmployeeViewModel()
            };
            try
            {
                var url = String.Format(AppointmentUserService.GET_EMPLOYEESBYID, id);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                var result = await base.GetHttpResponse<BusinessEmployeeViewModel>(response);

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

        public async Task<ResponseViewModel<List<BusinessEmployeeViewModel>>> Gets(long? id, TableType type)
        {
            var returnResponse = new ResponseViewModel<List<BusinessEmployeeViewModel>>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_EMPLOYEESBYIDANDTYPE, id, (int) type);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    returnResponse = await base.GetHttpResponse<List<BusinessEmployeeViewModel>>(response);
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

        public Task<ResponseViewModel<List<BusinessEmployeeViewModel>>> Gets()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<BusinessEmployeeViewModel>> Find(Predicate<BusinessEmployeeViewModel> pridict)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel<BusinessEmployeeViewModel>> Add(BusinessEmployeeViewModel model)
        {
            var returnResponse = new ResponseViewModel<BusinessEmployeeViewModel>();
            try
            {
                var registerModel = new UserViewModel()
                {
                    UserType = UserType.BusinessEmployee,
                    BusinessEmployee = model
                };
                var jsonContent = JsonConvert.SerializeObject(registerModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.POST_API_ACCOUNT_REGISTER);
                var response = await this.appointmentUserService.httpClient.PostAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessEmployeeViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessEmployeeViewModel>> Update(BusinessEmployeeViewModel model)
        {
            var returnResponse = new ResponseViewModel<BusinessEmployeeViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.PUT_EMPLOYEES, model.Id);
                var response = await this.appointmentUserService.httpClient.PutAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessEmployeeViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessEmployeeViewModel>> DeleteEmployee(BusinessEmployeeViewModel model)
        {
            var returnResponse = new ResponseViewModel<BusinessEmployeeViewModel>();
            try
            {
                var registerModel = new UserViewModel()
                {
                    UserType = UserType.BusinessEmployee,
                    BusinessEmployee = model
                };
                var jsonContent = JsonConvert.SerializeObject(registerModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentUserService.DELETE_API_ACCOUNT);
                var response = await this.appointmentUserService.httpClient.PostAsync(url, content);
                returnResponse = await base.GetHttpResponse<BusinessEmployeeViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<BusinessEmployeeViewModel>> Deactive(long? id, bool status)
        {
            var returnResponse = new ResponseViewModel<BusinessEmployeeViewModel>();
            try
            {
                var url = String.Format(AppointmentUserService.DEACTIVE_EMPLOYEES, id, status);
                var response = await this.appointmentUserService.httpClient.DeleteAsync(url);
                returnResponse = await base.GetHttpResponse<BusinessEmployeeViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public Task<ResponseViewModel<BusinessEmployeeViewModel>> Delete(long? id)
        {
            throw new NotImplementedException();
        }
    }
}
