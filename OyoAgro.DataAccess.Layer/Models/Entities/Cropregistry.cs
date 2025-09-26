using System;
using System.Collections.Generic;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Cropregistry
    {
        public long Cropregistryid { get; set; }
        public Guid? Tempclientid { get; set; }
        public long Farmid { get; set; }
        public int Seasonid { get; set; }
        public int Croptypeid { get; set; }
        public string? Cropvariety { get; set; }
        public decimal? Areaplanted { get; set; }
        public decimal? Plantedquantity { get; set; }
        public DateOnly? Plantingdate { get; set; }
        public DateOnly? Harvestdate { get; set; }
        public decimal? Areaharvested { get; set; }
        public decimal? Yieldquantity { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public DateTime? Deletedat { get; set; }
        public long? Version { get; set; }

        public virtual Crop Croptype { get; set; } = null!;
        public virtual Farm Farm { get; set; } = null!;
        public virtual Season Season { get; set; } = null!;
    }
}
