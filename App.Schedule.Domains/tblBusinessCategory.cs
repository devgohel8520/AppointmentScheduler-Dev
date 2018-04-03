namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblBusinessCategory")]
    public partial class tblBusinessCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblBusinessCategory()
        {
            tblBusinesses = new HashSet<tblBusiness>();
            tblBusinessCategory1 = new HashSet<tblBusinessCategory>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(250)]
        public string Type { get; set; }

        public string PictureLink { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public int? OrderNumber { get; set; }

        public long? AdministratorId { get; set; }

        public int? ParentId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBusiness> tblBusinesses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBusinessCategory> tblBusinessCategory1 { get; set; }

        public virtual tblBusinessCategory tblBusinessCategory2 { get; set; }
    }
}
