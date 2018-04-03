using System;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Services
{
    public class RegisterService : AppointmentBaseService
    {
        public RegisterService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<string>> PostRegister(AdministratorViewModel model)
        {
            var returnResponse = new ResponseViewModel<string>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent,Encoding.UTF8,"application/json");
                var url = String.Format(AppointmentService.POST_API_ACCOUNT_REGISTER);
                var response = await this.appointmentService.httpClient.PostAsync(url,content);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<string>>(result);
                if (res.Status)
                {
                    returnResponse.Status = true;
                    returnResponse.Message = "Successfully added";
                    returnResponse.Data = res.Data;
                }
                else
                {
                    returnResponse.Status = res.Status;
                    returnResponse.Message = res.Message;
                    returnResponse.Data = res.Message;
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = ex.Message.ToString();
                returnResponse.Data = ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<string>> PutRegister(AdministratorViewModel model)
        {
            var returnResponse = new ResponseViewModel<string>();
            try
            {
                model.LoginId = model.Email;
                //model.Password = HttpContext.Current.Server.UrlEncode(model.Password);
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.PUT_API_ACCOUNT_REGISTER);
                var response = await this.appointmentService.httpClient.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<string>>(result);
                if (res.Status)
                {
                    returnResponse.Status = true;
                    returnResponse.Data = res.Data;
                    returnResponse.Message = "Successfully updated.";
                }
                else
                {
                    returnResponse.Status = res.Status;
                    returnResponse.Message = res.Message;
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<AdministratorViewModel>> DeActiveRegister(long? id, bool status)
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
                    var url = string.Format(AppointmentService.DEACTIVE_ADMIN, id.Value,status);
                    var response = await this.appointmentService.httpClient.DeleteAsync(url);
                    var result = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<ResponseViewModel<AdministratorViewModel>>(result);
                    if (res.Status)
                    {
                        returnResponse.Status = true;
                        returnResponse.Data = res.Data;
                        returnResponse.Message = "Successfully updated.";
                    }

                    else
                    {
                        returnResponse.Status = res.Status;
                        returnResponse.Message = res.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }
    }
}
