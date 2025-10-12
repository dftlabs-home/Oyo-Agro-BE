using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Livestockregistry : BaseEntity
    {
        public long Livestockregistryid { get; set; }
        public Guid? Tempclientid { get; set; }
        public long Farmid { get; set; }
        public int Seasonid { get; set; }
        public int Livestocktypeid { get; set; }
        public int? Quantity { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }      
        public long? Version { get; set; }

        public virtual Farm Farm { get; set; } = null!;
        public virtual Livestock Livestocktype { get; set; } = null!;
        public virtual Season Season { get; set; } = null!;

    }
}
