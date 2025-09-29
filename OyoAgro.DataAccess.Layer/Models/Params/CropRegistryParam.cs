using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class CropRegistryParam
    {
        public long Cropregistryid { get; set; }
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

    }
}
