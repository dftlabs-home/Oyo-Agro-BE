using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Cropregistry : BaseEntity
    {
        public long Cropregistryid { get; set; }
        public Guid? Tempclientid { get; set; }
        public long Farmid { get; set; }
        public int Seasonid { get; set; }
        public int Croptypeid { get; set; }
        public string? Cropvariety { get; set; }
        public decimal? Areaplanted { get; set; }
        public decimal? Plantedquantity { get; set; }
        public DateTime? Plantingdate { get; set; }
        public DateTime? Harvestdate { get; set; }
        public decimal? Areaharvested { get; set; }
        public decimal? Yieldquantity { get; set; }
       
        public long? Version { get; set; }

        public virtual Crop Croptype { get; set; } = null!;
        public virtual Farm Farm { get; set; } = null!;
        public virtual Season Season { get; set; } = null!;
    }
}
