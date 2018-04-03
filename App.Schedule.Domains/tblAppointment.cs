namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblAppointment")]
    public partial class tblAppointment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblAppointment()
        {
            tblAppointmentDocuments = new HashSet<tblAppointmentDocument>();
            tblAppointmentFeedbacks = new HashSet<tblAppointmentFeedback>();
            tblAppointmentInvitees = new HashSet<tblAppointmentInvitee>();
            tblAppointmentPayments = new HashSet<tblAppointmentPayment>();
        }

        public long Id { get; set; }

        public long GlobalAppointmentId { get; set; }

        public long? BusinessServiceId { get; set; }

        public string Title { get; set; }

        public int PatternType { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool IsRecuring { get; set; }

        public bool IsAllDayEvent { get; set; }

        public int? TextColor { get; set; }

        public int? BackColor { get; set; }

        public int? RecureEvery { get; set; }

        public int? EndAfter { get; set; }

        public DateTime? EndAfterDate { get; set; }

        public int? StatusType { get; set; }

        [Column(TypeName = "ntext")]
        public string CancelReason { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "date")]
        public DateTime Created { get; set; }

        public long? BusinessOfferId { get; set; }

        public long? ServiceLocationId { get; set; }

        public long? BusinessEmployeeId { get; set; }

        public long? BusinessCustomerId { get; set; }

        public long? ScheduleId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAppointmentDocument> tblAppointmentDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAppointmentFeedback> tblAppointmentFeedbacks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAppointmentInvitee> tblAppointmentInvitees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAppointmentPayment> tblAppointmentPayments { get; set; }

        public virtual tblBusinessCustomer tblBusinessCustomer { get; set; }

        public virtual tblBusinessOffer tblBusinessOffer { get; set; }

        public virtual tblBusinessService tblBusinessService { get; set; }

        public virtual tblServiceLocation tblServiceLocation { get; set; }

        public virtual tblSchedule tblSchedule { get; set; }
    }
}
