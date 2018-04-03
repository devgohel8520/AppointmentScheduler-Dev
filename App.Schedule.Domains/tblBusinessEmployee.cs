namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblBusinessEmployee")]
    public partial class tblBusinessEmployee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblBusinessEmployee()
        {
            tblAppointmentFeedbacks = new HashSet<tblAppointmentFeedback>();
            tblAppointmentInvitees = new HashSet<tblAppointmentInvitee>();
            tblBusinessOffers = new HashSet<tblBusinessOffer>();
            tblBusinessServices = new HashSet<tblBusinessService>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(250)]
        public string LastName { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        public int? STD { get; set; }

        [StringLength(250)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public bool IsAdmin { get; set; }

        public long? ServiceLocationId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAppointmentFeedback> tblAppointmentFeedbacks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAppointmentInvitee> tblAppointmentInvitees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBusinessOffer> tblBusinessOffers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBusinessService> tblBusinessServices { get; set; }
    }
}
