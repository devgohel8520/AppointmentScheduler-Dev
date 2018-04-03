namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblAppointmentDocument")]
    public partial class tblAppointmentDocument
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int? DocumentType { get; set; }

        public bool IsEmployeeUpload { get; set; }

        public string DocumentLink { get; set; }

        public long? DocumentCategoryId { get; set; }

        public long? AppointmentId { get; set; }

        public virtual tblAppointment tblAppointment { get; set; }

        public virtual tblDocumentCategory tblDocumentCategory { get; set; }
    }
}
