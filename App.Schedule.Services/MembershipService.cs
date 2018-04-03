using System;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Services
{
    public class MembershipService : AppointmentBaseService, IAppointmentService<MembershipViewModel>
    {
        public MembershipService(string token)
        {
            base.SetUpAppointmentService(token);
        }
       
        public async Task<ResponseViewModel<MembershipViewModel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<MembershipViewModel>();
            try
            {
                var url = String.Format(AppointmentService.GET_MEMBERSHIP_BYID, id.Value);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<MembershipViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<List<MembershipViewModel>>> Gets()
        {
            var returnResponse = new ResponseViewModel<List<MembershipViewModel>>();
            try
            {
                var url = String.Format(AppointmentService.GET_MEMBERSHIPS);
                var response = await this.appointmentService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<MembershipViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<MembershipViewModel>> Add(MembershipViewModel model)
        {
            var returnResponse = new ResponseViewModel<MembershipViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.POST_MEMBERSHIP);
                var response = await this.appointmentService.httpClient.PostAsync(url, content);
                returnResponse = await base.GetHttpResponse<MembershipViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<MembershipViewModel>> Update(MembershipViewModel model)
        {
            var returnResponse = new ResponseViewModel<MembershipViewModel>();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = String.Format(AppointmentService.PUT_MEMBERSHIP, model.Id);
                var response = await this.appointmentService.httpClient.PutAsync(url, content);
                returnResponse = await base.GetHttpResponse<MembershipViewModel>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<MembershipViewModel>> Delete(long? id)
        {
            var returnResponse = new ResponseViewModel<MembershipViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please enter a valid membership id.";
                }
                else
                {
                    var url = String.Format(AppointmentService.DELETE_MEMBERSHIP, id.Value,false,DeleteType.DeleteRecord);
                    var response = await this.appointmentService.httpClient.DeleteAsync(url);
                    returnResponse = await base.GetHttpResponse<MembershipViewModel>(response);
                }
            }
            catch (Exception ex)
            {
                returnResponse.Message = "There was a problem. Please try again return. reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public async Task<ResponseViewModel<MembershipViewModel>> Deactive(long? id, bool status)
        {
            var returnResponse = new ResponseViewModel<MembershipViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "Please enter a valid membership id.";
                }
                else
                {
                    var url = String.Format(AppointmentService.DEACTIVE_MEMBERSHIP, id.Value,status,DeleteType.UpdateStatus);
                    var response = await this.appointmentService.httpClient.DeleteAsync(url);
                    returnResponse = await base.GetHttpResponse<MembershipViewModel>(response);
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
