namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblBusinessHoliday
    {
        public long Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime OnDate { get; set; }

        public int Type { get; set; }

        public long? ServiceLocationId { get; set; }

        public virtual tblServiceLocation tblServiceLocation { get; set; }
    }
}
