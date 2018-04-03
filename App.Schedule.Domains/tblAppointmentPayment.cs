namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblAppointmentPayment")]
    public partial class tblAppointmentPayment
    {
        public long Id { get; set; }

        public bool IsPaid { get; set; }

        public DateTime PaidDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public int BillingType { get; set; }

        [StringLength(250)]
        public string PurchaseOrderNo { get; set; }

        [StringLength(250)]
        public string ChequeNumber { get; set; }

        public int? CardType { get; set; }

        [StringLength(250)]
        public string CCFirstName { get; set; }

        [StringLength(250)]
        public string CCLastName { get; set; }

        [StringLength(250)]
        public string CCardNumber { get; set; }

        [StringLength(250)]
        public string CCSecurityCode { get; set; }

        public DateTime? CCExpirationDate { get; set; }

        public DateTime Created { get; set; }

        public long? AppointmentId { get; set; }

        public virtual tblAppointment tblAppointment { get; set; }
    }
}
