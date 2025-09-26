using System;
using System.Collections.Generic;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Livestockregistry
    {
        public long Livestockregistryid { get; set; }
        public Guid? Tempclientid { get; set; }
        public long Farmid { get; set; }
        public int Seasonid { get; set; }
        public int Livestocktypeid { get; set; }
        public int? Quantity { get; set; }
        public DateOnly? Startdate { get; set; }
        public DateOnly? Enddate { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public DateTime? Deletedat { get; set; }
        public long? Version { get; set; }

        public virtual Farm Farm { get; set; } = null!;
        public virtual Livestock Livestocktype { get; set; } = null!;
        public virtual Season Season { get; set; } = null!;
    }
}
