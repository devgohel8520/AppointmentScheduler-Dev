namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblMembership")]
    public partial class tblMembership
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblMembership()
        {
            tblBusinesses = new HashSet<tblBusiness>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Benifits { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public bool IsUnlimited { get; set; }

        public int TotalEmployee { get; set; }

        public int TotalCustomer { get; set; }

        public int TotalAppointment { get; set; }

        public int TotalLocation { get; set; }

        public int TotalOffers { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public long? AdministratorId { get; set; }

        public virtual tblAdministrator tblAdministrator { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBusiness> tblBusinesses { get; set; }
    }
}
