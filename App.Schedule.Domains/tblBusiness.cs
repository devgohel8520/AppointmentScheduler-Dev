namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblBusiness")]
    public partial class tblBusiness
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblBusiness()
        {
            tblServiceLocations = new HashSet<tblServiceLocation>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(50)]
        public string ShortName { get; set; }

        public string Logo { get; set; }

        [StringLength(250)]
        public string PhoneNumbers { get; set; }

        [StringLength(250)]
        public string FaxNumbers { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Website { get; set; }

        [StringLength(250)]
        public string Add1 { get; set; }

        [StringLength(250)]
        public string Add2 { get; set; }

        [StringLength(250)]
        public string City { get; set; }

        [StringLength(250)]
        public string State { get; set; }

        [StringLength(50)]
        public string Zip { get; set; }

        public int? CountryId { get; set; }

        public bool IsInternational { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public int? MembershipId { get; set; }

        public int? BusinessCategoryId { get; set; }

        public int? TimezoneId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblServiceLocation> tblServiceLocations { get; set; }

        public virtual tblBusinessCategory tblBusinessCategory { get; set; }

        public virtual tblMembership tblMembership { get; set; }

        public virtual tblTimezone tblTimezone { get; set; }
    }
}
