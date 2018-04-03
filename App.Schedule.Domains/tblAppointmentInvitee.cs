namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblAppointmentInvitee")]
    public partial class tblAppointmentInvitee
    {
        public long Id { get; set; }

        public long? BusinessEmployeeId { get; set; }

        public long? AppointmentId { get; set; }

        public virtual tblAppointment tblAppointment { get; set; }

        public virtual tblBusinessEmployee tblBusinessEmployee { get; set; }
    }
}
