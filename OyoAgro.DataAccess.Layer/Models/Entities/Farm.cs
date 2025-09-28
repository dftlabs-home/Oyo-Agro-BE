using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Farm : BaseEntity
    {
        public Farm()
        {
            Cropregistries = new HashSet<Cropregistry>();
            Livestockregistries = new HashSet<Livestockregistry>();
        }

        public long Farmid { get; set; }
        public Guid? Tempclientid { get; set; }
        public int Farmerid { get; set; }
        public int Farmtypeid { get; set; }
        public decimal? Farmsize { get; set; }
        public long? Farmaddressid { get; set; }
        
        public long? Version { get; set; }

        public virtual Address? Farmaddress { get; set; }
        public virtual Farmer Farmer { get; set; } = null!;
        public virtual Farmtype Farmtype { get; set; } = null!;
        public virtual ICollection<Cropregistry> Cropregistries { get; set; }
        public virtual ICollection<Livestockregistry> Livestockregistries { get; set; }
    }
}
