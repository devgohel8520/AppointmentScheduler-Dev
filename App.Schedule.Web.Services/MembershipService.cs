using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Services
{
    public class MembershipService : AppointmentUserBaseService, IAppointmentUserService<MembershipViewModel>
    {
        public MembershipService(string token)
        {
            base.SetUpAppointmentService(token);
        }

        public async Task<ResponseViewModel<MembershipViewModel>> Get(long? id)
        {
            var returnResponse = new ResponseViewModel<MembershipViewModel>()
            {
                Status = false,
                Message = "",
                Data = new MembershipViewModel()
            };
            try
            {
                var url = String.Format(AppointmentUserService.GET_MEMBERSHIP_BYID, id);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                var result = await base.GetHttpResponse<MembershipViewModel>(response);

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

        public async Task<ResponseViewModel<List<MembershipViewModel>>> Gets()
        {
            var returnResponse = new ResponseViewModel<List<MembershipViewModel>>();
            try
            {
                var url = String.Format(AppointmentUserService.GET_MEMBERSHIPS);
                var response = await this.appointmentUserService.httpClient.GetAsync(url);
                returnResponse = await base.GetHttpResponse<List<MembershipViewModel>>(response);
            }
            catch (Exception ex)
            {
                returnResponse.Data = null;
                returnResponse.Message = "Reason: " + ex.Message.ToString();
                returnResponse.Status = false;
            }
            return returnResponse;
        }

        public Task<ResponseViewModel<MembershipViewModel>> Add(MembershipViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<MembershipViewModel>> Deactive(long? id, bool status)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<MembershipViewModel>> Delete(long? id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<MembershipViewModel>> Find(Predicate<MembershipViewModel> pridict)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel<MembershipViewModel>> Update(MembershipViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
