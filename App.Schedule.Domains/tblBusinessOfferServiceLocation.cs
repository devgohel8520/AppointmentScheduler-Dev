namespace App.Schedule.Domains
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblBusinessOfferServiceLocation")]
    public partial class tblBusinessOfferServiceLocation
    {
        public long Id { get; set; }

        public long? BusinessOfferId { get; set; }

        public long? ServiceLocationId { get; set; }

        public virtual tblBusinessOffer tblBusinessOffer { get; set; }

        public virtual tblServiceLocation tblServiceLocation { get; set; }
    }
}
