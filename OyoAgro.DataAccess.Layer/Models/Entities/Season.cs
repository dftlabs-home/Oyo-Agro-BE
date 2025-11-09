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
            AgroAlliedRegistries = new HashSet<AgroAlliedRegistry>();
        }

        public int Seasonid { get; set; }
        public Guid? Tempclientid { get; set; }
        public string Name { get; set; } = null!;
        public int? Year { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
     
        public long? Version { get; set; }

        public virtual ICollection<Cropregistry> Cropregistries { get; set; }
        public virtual ICollection<Livestockregistry> Livestockregistries { get; set; }
        public virtual ICollection<AgroAlliedRegistry> AgroAlliedRegistries { get; set; }
    }
}
