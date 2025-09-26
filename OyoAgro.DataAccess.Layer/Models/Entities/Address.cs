using System;
using System.Collections.Generic;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Address
    {
        public Address()
        {
            Farmers = new HashSet<Farmer>();
            Farms = new HashSet<Farm>();
            Userprofiles = new HashSet<Userprofile>();
        }

        public long Addressid { get; set; }
        public Guid? Tempclientid { get; set; }
        public string? Streetaddress { get; set; }
        public string? Town { get; set; }
        public string? Postalcode { get; set; }
        public int? Lgaid { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public DateTime? Deletedat { get; set; }
        public long? Version { get; set; }

        public virtual Lga? Lga { get; set; }
        public virtual ICollection<Farmer> Farmers { get; set; }
        public virtual ICollection<Farm> Farms { get; set; }
        public virtual ICollection<Userprofile> Userprofiles { get; set; }
    }
}
