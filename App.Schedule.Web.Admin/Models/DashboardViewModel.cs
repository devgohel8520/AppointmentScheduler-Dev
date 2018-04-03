using App.Schedule.Domains.ViewModel;

namespace App.Schedule.Web.Admin.Models
{
    public class DashboardViewModel : ErrorViewModel
    {
        public long AdminsCount { get; set; }
        public long CountryCount { get; set; }
        public long TimezonCount { get; set; }
        public long MembershipCount { get; set; }
        public long BusinessCategoryCount { get; set; }
    }
}