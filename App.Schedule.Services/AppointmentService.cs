using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Services
{
    public class AppointmentService
    {
        public HttpClient httpClient;
        //private const string baseUrl = "http://appointment.why-fi.com";
        public static string baseUrl = "http://localhost/appointmentapi/";
        //public static string baseUrl = "http://localhost:57433/";

        //Admin Identity Token API
        public const string POST_API_ACCOUNT_REGISTER = "api/account/register";
        public const string PUT_API_ACCOUNT_REGISTER = "api/account/updateuser";
        public const string GET_ADMIN_TOKEN = "token";

        //Administrator API
        public const string GET_ADMINS = "api/administrator";
        public const string GET_ADMIN_BYID = "api/administrator?Id={0}";
        public const string GET_ADMIN_BYEMAIL = "api/administrator?email={0}&password={1}";
        public const string POST_ADMIN = "api/administrator";
        public const string DELETE_ADMIN = "api/administrator?id={0}&status={1}&type={2}";
        public const string DEACTIVE_ADMIN = "api/administrator?id={0}&status={1}&type={2}";

        //Country API
        public const string GET_COUNTRIES = "api/country";
        public const string POST_COUNTRY = "api/country";
        public const string GET_COUNTRY_BYID = "api/country?id={0}";
        public const string PUT_COUNTRY = "api/country?id={0}";
        public const string DELETE_COUNTRY = "api/country?id={0}";

        //Timezone API
        public const string GET_TIMEZONES = "api/timezone";
        public const string POST_TIMEZONE = "api/timezone";
        public const string GET_TIMEZONE_BYID = "api/timezone?id={0}";
        public const string PUT_TIMEZONE = "api/timezone?id={0}";
        public const string DELETE_TIMEZONE = "api/timezone?id={0}";

        //MEMBERSHIP API
        public const string GET_MEMBERSHIPS = "api/membership";
        public const string POST_MEMBERSHIP = "api/membership";
        public const string GET_MEMBERSHIP_BYID = "api/membership?id={0}";
        public const string PUT_MEMBERSHIP = "api/membership?id={0}";
        public const string DELETE_MEMBERSHIP = "api/membership?id={0}&status={1}&type={2}";
        public const string DEACTIVE_MEMBERSHIP = "api/membership?id={0}&status={1}&type={2}";

        //BUSINESSCATEGORY API
        public const string GET_BUSINESSCATEGORIES = "api/businesscategory";
        public const string POST_BUSINESSCATEGORY = "api/businesscategory";
        public const string GET_BUSINESSCATEGORY_BYID = "api/businesscategory?id={0}";
        public const string PUT_BUSINESSCATEGORY_BYID = "api/businesscategory?id={0}";
        public const string PUT_BUSINESSCATEGORY = "api/businesscategory?id={0}&status={1}&type={2}";
        public const string DELETE_BUSINESSCATEGORY = "api/businesscategory?id={0}&status={1}&type={2}";

        public AppointmentService()
        {
            this.httpClient = new HttpClient() { BaseAddress = new Uri(baseUrl) };
            this.httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }

    public class AppointmentBaseService
    {
        protected AppointmentService appointmentService;

        protected void SetUpAppointmentService(string token)
        {
            appointmentService = new AppointmentService();
            this.appointmentService.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<ResponseViewModel<T>> GetHttpResponse<T>(HttpResponseMessage response)
        {
            var returnResponse = new ResponseViewModel<T>();
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    dynamic res = JsonConvert.DeserializeObject<ResponseViewModel<T>>(result, settings);
                    if (res != null)
                    {
                        returnResponse.Status = res.Status;
                        returnResponse.Message = res.Message;
                        returnResponse.Data = res.Data;
                    }
                    else
                    {
                        returnResponse.Status = false;
                        returnResponse.Message = "There was a problem. Please try again later.";
                    }
                }
                else
                {
                    returnResponse.Status = false;
                    returnResponse.Message = "There was a problem. Please try agian later. reason:" + response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                returnResponse.Status = false;
                returnResponse.Message = "There was a problem. Please try agian later. reason:" + ex.Message.ToString();
            }
            return returnResponse;
        }

        public async Task<List<T>> GetHttpResponseList<T>(HttpResponseMessage response)
        {
            var model = new List<T>();
            try
            {
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResponseViewModel<List<T>>>(result);
                if (res != null)
                    if (res.Status)
                        model = res.Data;
            }
            catch
            {
                model = null;
            }
            return model;
        }
    }
}
