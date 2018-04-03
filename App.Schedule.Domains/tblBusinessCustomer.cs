namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblBusinessCustomer")]
    public partial class tblBusinessCustomer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblBusinessCustomer()
        {
            tblAppointments = new HashSet<tblAppointment>();
            tblAppointmentFeedbacks = new HashSet<tblAppointmentFeedback>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(250)]
        public string LastName { get; set; }

        [Column(TypeName = "image")]
        public byte[] ProfilePicture { get; set; }

        public int? StdCode { get; set; }

        [StringLength(250)]
        public string PhoneNumber { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Add1 { get; set; }

        [StringLength(250)]
        public string Add2 { get; set; }

        [StringLength(250)]
        public string City { get; set; }

        [StringLength(250)]
        public string State { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        public DateTime Created { get; set; }

        public bool IsActive { get; set; }

        public long? ServiceLocationId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAppointment> tblAppointments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAppointmentFeedback> tblAppointmentFeedbacks { get; set; }
    }
}
