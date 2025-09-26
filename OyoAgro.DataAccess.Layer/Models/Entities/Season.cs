using System;
using System.Collections.Generic;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Season
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
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public DateTime? Deletedat { get; set; }
        public long? Version { get; set; }

        public virtual ICollection<Cropregistry> Cropregistries { get; set; }
        public virtual ICollection<Livestockregistry> Livestockregistries { get; set; }
    }
}
