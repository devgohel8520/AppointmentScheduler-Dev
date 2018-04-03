namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCountry")]
    public partial class tblCountry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCountry()
        {
            tblServiceLocations = new HashSet<tblServiceLocation>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(10)]
        public string ISO { get; set; }

        [StringLength(10)]
        public string ISO3 { get; set; }

        [StringLength(50)]
        public string CurrencyName { get; set; }

        [StringLength(50)]
        public string CurrencyCode { get; set; }

        public int PhoneCode { get; set; }

        public long? AdministratorId { get; set; }

        public virtual tblAdministrator tblAdministrator { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblServiceLocation> tblServiceLocations { get; set; }
    }
}
