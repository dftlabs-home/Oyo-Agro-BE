using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Address : BaseEntity
    {

        public long Addressid { get; set; }
        public Guid? Tempclientid { get; set; }
        public string? Streetaddress { get; set; }
        public string? Town { get; set; }
        public string? Postalcode { get; set; }
        public int? Lgaid { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
       
        public long? Version { get; set; }
        public int? Userid { get; set; }
        public int? Farmerid { get; set; }
        public int? Farmid { get; set; }

        public virtual Lga? Lga { get; set; }
    }
}
