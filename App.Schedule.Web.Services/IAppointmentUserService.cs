using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Services
{
    interface IAppointmentUserService<T>
    {
        //Get Object
        Task<ResponseViewModel<T>> Get(long? id);

        //Gets Objects
        Task<ResponseViewModel<List<T>>> Gets();

        Task<ResponseViewModel<T>> Find(Predicate<T> pridict);

        //Add Object
        Task<ResponseViewModel<T>> Add(T model);

        //Update Object
        Task<ResponseViewModel<T>> Update(T model);

        //Delete Object
        Task<ResponseViewModel<T>> Delete(long? id);

        //Deactive Object
        Task<ResponseViewModel<T>> Deactive(long? id, bool status);
    }
}
