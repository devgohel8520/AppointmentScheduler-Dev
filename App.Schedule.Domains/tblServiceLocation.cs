namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblServiceLocation")]
    public partial class tblServiceLocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblServiceLocation()
        {
            tblAppointments = new HashSet<tblAppointment>();
            tblBusinessHolidays = new HashSet<tblBusinessHoliday>();
            tblBusinessHours = new HashSet<tblBusinessHour>();
            tblBusinessOfferServiceLocations = new HashSet<tblBusinessOfferServiceLocation>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public string Description { get; set; }

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

        public int? CountryId { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public int? TimezoneId { get; set; }

        public long? BusinessId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAppointment> tblAppointments { get; set; }

        public virtual tblBusiness tblBusiness { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBusinessHoliday> tblBusinessHolidays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBusinessHour> tblBusinessHours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBusinessOfferServiceLocation> tblBusinessOfferServiceLocations { get; set; }

        public virtual tblCountry tblCountry { get; set; }

        public virtual tblTimezone tblTimezone { get; set; }
    }
}
