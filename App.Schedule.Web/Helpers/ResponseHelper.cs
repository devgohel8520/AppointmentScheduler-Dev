using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Helpers
{
    public class ResponseHelper
    {
        public ResponseViewModel<T> GetResponse<T>()
        {
            var response = new ResponseViewModel<T>()
            {
                Status = false,
                Message = "API calling error. Please try again later",
                Data = default(T)
            };
            return response;
        }
    }
}