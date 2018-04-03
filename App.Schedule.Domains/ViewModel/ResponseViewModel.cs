
namespace App.Schedule.Domains.ViewModel
{
    public class ResponseViewModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// Class is used to hold response information.
    /// </summary>
    /// <typeparam name="T">Any data type to get data values.</typeparam>
    public class ResponseViewModel<T> : ResponseViewModel
    {
        public T Data { get; set; }
    }

    //public class ModelState
    //{
    //    public List<string> Message { get; set; }
    //}

    //public class AspIdentityModelStateViewModel
    //{
    //    public string Message { get; set; }
    //    public ModelState ModelState { get; set; }
    //}
}
