using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Season : BaseEntity
    {
        public Season()
        {
            Cropregistries = new HashSet<Cropregistry>();
            Livestockregistries = new HashSet<Livestockregistry>();
        }

        public int Seasonid { get; set; }
        public Guid? Tempclientid { get; set; }
        public string Name { get; set; } = null!;
        public int? Year { get; set; }
        public DateOnly? Startdate { get; set; }
        public DateOnly? Enddate { get; set; }
     
        public long? Version { get; set; }

        public virtual ICollection<Cropregistry> Cropregistries { get; set; }
        public virtual ICollection<Livestockregistry> Livestockregistries { get; set; }
    }
}
