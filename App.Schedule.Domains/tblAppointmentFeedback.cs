namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblAppointmentFeedback")]
    public partial class tblAppointmentFeedback
    {
        public long Id { get; set; }

        public bool IsEmployee { get; set; }

        public long? BusinessEmployeeId { get; set; }

        public long? BusinessCustomerId { get; set; }

        public int? Rating { get; set; }

        [Column(TypeName = "ntext")]
        public string Feedback { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }

        public long? AppointmentId { get; set; }

        public virtual tblAppointment tblAppointment { get; set; }

        public virtual tblBusinessCustomer tblBusinessCustomer { get; set; }

        public virtual tblBusinessEmployee tblBusinessEmployee { get; set; }
    }
}
