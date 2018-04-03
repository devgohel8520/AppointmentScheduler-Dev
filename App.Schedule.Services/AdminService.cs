using System;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using App.Schedule.Domains.ViewModel;
using System.Collections.Generic;

namespace App.Schedule.Web.Admin.Services
{
    public class AdminService : AppointmentBaseService, IAppointmentService<AdministratorViewModel>
    {
        public AdminService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<AdministratorViewModel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<AdministratorViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_ADMIN_BYID, id.Value);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<AdministratorViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<List<AdministratorViewModel>>> Gets()
        {
            var returnResponse = new ResponseViewModel<List<AdministratorViewModel>>();
            try
            {
                var url = String.Format(AppointmentService.GET_ADMINS);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<AdministratorViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<AdministratorViewModel>> Add(AdministratorViewModel model)
        {
            var returnResponse = new ResponseViewModel<AdministratorViewModel>();
            try
            {
                var registerModel = new UserViewModel()
                {
                    UserType = UserType.SiteAdmin,
                    SiteAdmin = model
                };
                var jsonContent = JsonConvert.SerializeObject(registerModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.POST_API_ACCOUNT_REGISTER);
                var response = await this.appointmentService.httpClient.PostAsync(url, content);
                var result = await base.GetHttpResponse<UserViewModel>(response);
                returnResponse.Status = result.Status;
                returnResponse.Message = result.Message;
                returnResponse.Data = result.Data.SiteAdmin;
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<AdministratorViewModel>> Update(AdministratorViewModel model)
        {
            var returnResponse = new ResponseViewModel<AdministratorViewModel>();
            try
            {
                //model.Password = HttpContext.Current.Server.UrlEncode(model.Password);
                var registerModel = new UserViewModel()
                {
                    UserType = UserType.SiteAdmin,
                    SiteAdmin = model
                };
                var jsonContent = JsonConvert.SerializeObject(registerModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.PUT_API_ACCOUNT_REGISTER);
                var response = await this.appointmentService.httpClient.PutAsync(url, content);
                var result = await base.GetHttpResponse<UserViewModel>(response);
                returnResponse.Status = result.Status;
                returnResponse.Message = result.Message;
                returnResponse.Data = null;
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<AdministratorViewModel>> Delete(long? id)
        {
            var returnResponse = new ResponseViewModel<AdministratorViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please provide a valid admin id.";
                }
                else
                {
                    var url = string.Format(AppointmentService.DELETE_ADMIN, id.Value, false, DeleteType.DeleteRecord);
                    var response = await this.appointmentService.httpClient.DeleteAsync(url);
                    returnResponse = await base.GetHttpResponse<AdministratorViewModel>(response);
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<AdministratorViewModel>> Deactive(long? id, bool status)
        {
            var returnResponse = new ResponseViewModel<AdministratorViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please provide a valid admin id.";
                }
                else
                {
                    var url = string.Format(AppointmentService.DEACTIVE_ADMIN, id.Value, status, DeleteType.UpdateStatus);
                    var response = await this.appointmentService.httpClient.DeleteAsync(url);
                    returnResponse = await base.GetHttpResponse<AdministratorViewModel>(response);
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<AdministratorViewModel>> VerifyLoginCredential(string Email, string Password)
        {
            var returnResponse = new ResponseViewModel<AdministratorViewModel>();
            try
            {
                Password = HttpContext.Current.Server.UrlEncode(Password);
                var url = String.Format(AppointmentService.GET_ADMIN_BYEMAIL, Email, Password);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<AdministratorViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<string>> VerifyAndGetAdminAccessToken(string Email, string Password)
        {
            var returnResponse = new ResponseViewModel<string>();
            try
            {
                //Password = HttpContext.Current.Server.UrlEncode(Password);
                var model = "username=" + Email + "&password=" + Password + "&grant_type=password";
                var content = new StringContent(model, Encoding.UTF8, "text/plain");
                var url = String.Format(AppointmentService.GET_ADMIN_TOKEN);
                var response = await this.appointmentService.httpClient.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                dynamic res = JsonConvert.DeserializeObject(result);
                if (res != null)
                {
                    try
                    {
                        returnResponse.Status = true;
                        returnResponse.Data = res.access_token;
                        returnResponse.Message = "Success";
                    }
                    catch
                    {
                        returnResponse.Status = false;
                        returnResponse.Data = null;
                        returnResponse.Message = "Please check your id and password";
                    }
                }
                else
                {
                    returnResponse.Status = false;
                    returnResponse.Data = null;
                    returnResponse.Message = "There was a problem. Please try agian later.";
                }
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }
    }
}
