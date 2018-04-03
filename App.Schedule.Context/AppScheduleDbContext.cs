namespace App.Schedule.Context
{
    using System.Data.Entity;
    using App.Schedule.Domains;

    public partial class AppScheduleDbContext : DbContext
    {
        public AppScheduleDbContext()
           : base("name=AppScheduleDbContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<tblAdministrator> tblAdministrators { get; set; }
        public virtual DbSet<tblAppointment> tblAppointments { get; set; }
        public virtual DbSet<tblAppointmentDocument> tblAppointmentDocuments { get; set; }
        public virtual DbSet<tblAppointmentFeedback> tblAppointmentFeedbacks { get; set; }
        public virtual DbSet<tblAppointmentInvitee> tblAppointmentInvitees { get; set; }
        public virtual DbSet<tblAppointmentPayment> tblAppointmentPayments { get; set; }
        public virtual DbSet<tblBusiness> tblBusinesses { get; set; }
        public virtual DbSet<tblBusinessCategory> tblBusinessCategories { get; set; }
        public virtual DbSet<tblBusinessCustomer> tblBusinessCustomers { get; set; }
        public virtual DbSet<tblBusinessEmployee> tblBusinessEmployees { get; set; }
        public virtual DbSet<tblBusinessHoliday> tblBusinessHolidays { get; set; }
        public virtual DbSet<tblBusinessHour> tblBusinessHours { get; set; }
        public virtual DbSet<tblBusinessOffer> tblBusinessOffers { get; set; }
        public virtual DbSet<tblBusinessOfferServiceLocation> tblBusinessOfferServiceLocations { get; set; }
        public virtual DbSet<tblBusinessService> tblBusinessServices { get; set; }
        public virtual DbSet<tblCountry> tblCountries { get; set; }
        public virtual DbSet<tblDocumentCategory> tblDocumentCategories { get; set; }
        public virtual DbSet<tblMembership> tblMemberships { get; set; }
        public virtual DbSet<tblSchedule> tblSchedules { get; set; }
        public virtual DbSet<tblServiceLocation> tblServiceLocations { get; set; }
        public virtual DbSet<tblTimezone> tblTimezones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblCountries)
                .WithOptional(e => e.tblAdministrator)
                .HasForeignKey(e => e.AdministratorId);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblMemberships)
                .WithOptional(e => e.tblAdministrator)
                .HasForeignKey(e => e.AdministratorId);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblTimezones)
                .WithOptional(e => e.tblAdministrator)
                .HasForeignKey(e => e.AdministratorId);

            modelBuilder.Entity<tblAppointment>()
                .HasMany(e => e.tblAppointmentDocuments)
                .WithOptional(e => e.tblAppointment)
                .HasForeignKey(e => e.AppointmentId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblAppointment>()
                .HasMany(e => e.tblAppointmentFeedbacks)
                .WithOptional(e => e.tblAppointment)
                .HasForeignKey(e => e.AppointmentId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblAppointment>()
                .HasMany(e => e.tblAppointmentInvitees)
                .WithOptional(e => e.tblAppointment)
                .HasForeignKey(e => e.AppointmentId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblAppointment>()
                .HasMany(e => e.tblAppointmentPayments)
                .WithOptional(e => e.tblAppointment)
                .HasForeignKey(e => e.AppointmentId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblAppointmentPayment>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tblBusiness>()
                .HasMany(e => e.tblServiceLocations)
                .WithOptional(e => e.tblBusiness)
                .HasForeignKey(e => e.BusinessId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblBusinessCategory>()
                .HasMany(e => e.tblBusinesses)
                .WithOptional(e => e.tblBusinessCategory)
                .HasForeignKey(e => e.BusinessCategoryId);

            modelBuilder.Entity<tblBusinessCategory>()
                .HasMany(e => e.tblBusinessCategory1)
                .WithOptional(e => e.tblBusinessCategory2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<tblBusinessCustomer>()
                .HasMany(e => e.tblAppointments)
                .WithOptional(e => e.tblBusinessCustomer)
                .HasForeignKey(e => e.BusinessCustomerId);

            modelBuilder.Entity<tblBusinessCustomer>()
                .HasMany(e => e.tblAppointmentFeedbacks)
                .WithOptional(e => e.tblBusinessCustomer)
                .HasForeignKey(e => e.BusinessCustomerId);

            modelBuilder.Entity<tblBusinessEmployee>()
                .HasMany(e => e.tblAppointmentFeedbacks)
                .WithOptional(e => e.tblBusinessEmployee)
                .HasForeignKey(e => e.BusinessEmployeeId);

            modelBuilder.Entity<tblBusinessEmployee>()
                .HasMany(e => e.tblAppointmentInvitees)
                .WithOptional(e => e.tblBusinessEmployee)
                .HasForeignKey(e => e.BusinessEmployeeId);

            modelBuilder.Entity<tblBusinessEmployee>()
                .HasMany(e => e.tblBusinessOffers)
                .WithOptional(e => e.tblBusinessEmployee)
                .HasForeignKey(e => e.BusinessEmployeeId);

            modelBuilder.Entity<tblBusinessEmployee>()
                .HasMany(e => e.tblBusinessServices)
                .WithOptional(e => e.tblBusinessEmployee)
                .HasForeignKey(e => e.EmployeeId);

            modelBuilder.Entity<tblBusinessOffer>()
                .HasMany(e => e.tblAppointments)
                .WithOptional(e => e.tblBusinessOffer)
                .HasForeignKey(e => e.BusinessOfferId);

            modelBuilder.Entity<tblBusinessOffer>()
                .HasMany(e => e.tblBusinessOfferServiceLocations)
                .WithOptional(e => e.tblBusinessOffer)
                .HasForeignKey(e => e.BusinessOfferId);

            modelBuilder.Entity<tblBusinessService>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tblBusinessService>()
                .HasMany(e => e.tblAppointments)
                .WithOptional(e => e.tblBusinessService)
                .HasForeignKey(e => e.BusinessServiceId);

            modelBuilder.Entity<tblCountry>()
                .HasMany(e => e.tblServiceLocations)
                .WithOptional(e => e.tblCountry)
                .HasForeignKey(e => e.CountryId);

            modelBuilder.Entity<tblDocumentCategory>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<tblDocumentCategory>()
                .HasMany(e => e.tblAppointmentDocuments)
                .WithOptional(e => e.tblDocumentCategory)
                .HasForeignKey(e => e.DocumentCategoryId);

            modelBuilder.Entity<tblDocumentCategory>()
                .HasMany(e => e.tblDocumentCategory1)
                .WithOptional(e => e.tblDocumentCategory2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<tblMembership>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tblMembership>()
                .HasMany(e => e.tblBusinesses)
                .WithOptional(e => e.tblMembership)
                .HasForeignKey(e => e.MembershipId);

            modelBuilder.Entity<tblSchedule>()
                .HasMany(e => e.tblAppointments)
                .WithOptional(e => e.tblSchedule)
                .HasForeignKey(e => e.ScheduleId);

            modelBuilder.Entity<tblServiceLocation>()
                .HasMany(e => e.tblAppointments)
                .WithOptional(e => e.tblServiceLocation)
                .HasForeignKey(e => e.ServiceLocationId);

            modelBuilder.Entity<tblServiceLocation>()
                .HasMany(e => e.tblBusinessHolidays)
                .WithOptional(e => e.tblServiceLocation)
                .HasForeignKey(e => e.ServiceLocationId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblServiceLocation>()
                .HasMany(e => e.tblBusinessHours)
                .WithOptional(e => e.tblServiceLocation)
                .HasForeignKey(e => e.ServiceLocationId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<tblServiceLocation>()
                .HasMany(e => e.tblBusinessOfferServiceLocations)
                .WithOptional(e => e.tblServiceLocation)
                .HasForeignKey(e => e.ServiceLocationId);

            modelBuilder.Entity<tblTimezone>()
                .HasMany(e => e.tblBusinesses)
                .WithOptional(e => e.tblTimezone)
                .HasForeignKey(e => e.TimezoneId);

            modelBuilder.Entity<tblTimezone>()
                .HasMany(e => e.tblServiceLocations)
                .WithOptional(e => e.tblTimezone)
                .HasForeignKey(e => e.TimezoneId);
        }
    }
}
