using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class LiveStockRegistryParam
    {
        public long Livestockregistryid { get; set; }
        public long Farmid { get; set; }
        public int Seasonid { get; set; }
        public int Livestocktypeid { get; set; }
        public int? Quantity { get; set; }
        public DateOnly? Startdate { get; set; }
        public DateOnly? Enddate { get; set; }

    }
}
