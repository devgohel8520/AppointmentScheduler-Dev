namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblTimezone")]
    public partial class tblTimezone
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblTimezone()
        {
            tblBusinesses = new HashSet<tblBusiness>();
            tblServiceLocations = new HashSet<tblServiceLocation>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        public int UtcOffset { get; set; }

        public bool IsDST { get; set; }

        public int? CountryId { get; set; }

        public long? AdministratorId { get; set; }

        public virtual tblAdministrator tblAdministrator { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBusiness> tblBusinesses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblServiceLocation> tblServiceLocations { get; set; }
    }
}
