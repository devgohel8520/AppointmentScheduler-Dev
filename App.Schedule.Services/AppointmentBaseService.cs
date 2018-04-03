using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace App.Schedule.Services
{
    public class AppointmentBaseService
    {
        protected AppointmentService appointmentService;
        
        protected void SetUpAppointmentService(string token)
        {
            appointmentService = new AppointmentService();
            this.appointmentService.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
