namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblBusinessHour
    {
        public long Id { get; set; }

        public int WeekDayId { get; set; }

        public bool IsStartDay { get; set; }

        public bool IsHoliday { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public bool? IsSplit1 { get; set; }

        public DateTime? FromSplit1 { get; set; }

        public DateTime? ToSplit1 { get; set; }

        public bool? IsSplit2 { get; set; }

        public DateTime? FromSplit2 { get; set; }

        public DateTime? ToSplit2 { get; set; }

        public long? ServiceLocationId { get; set; }

        public virtual tblServiceLocation tblServiceLocation { get; set; }
    }
}
