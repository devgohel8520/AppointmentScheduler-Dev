namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblDocumentCategory")]
    public partial class tblDocumentCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblDocumentCategory()
        {
            tblAppointmentDocuments = new HashSet<tblAppointmentDocument>();
            tblDocumentCategory1 = new HashSet<tblDocumentCategory>();
        }

        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int OrderNo { get; set; }

        public string PictureLink { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public bool IsParent { get; set; }

        public long? ParentId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAppointmentDocument> tblAppointmentDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblDocumentCategory> tblDocumentCategory1 { get; set; }

        public virtual tblDocumentCategory tblDocumentCategory2 { get; set; }
    }
}
