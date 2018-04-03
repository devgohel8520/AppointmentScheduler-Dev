namespace App.Schedule.Domains.ViewModel
{
    /// <summary>
    /// Class is used to hold service data information.
    /// </summary>
    /// <typeparam name="T">any type to get data response.</typeparam>

    public class ErrorViewModel
    {
        public bool HasError { get; set; }
        public string Error { get; set; }
        public bool HasMore { get; set; }
        public string ErrorDescription { get; set; }
    }

    public class ServiceDataViewModel<T> : ErrorViewModel
    {
        public T Data { get; set; }
    }
   
}
