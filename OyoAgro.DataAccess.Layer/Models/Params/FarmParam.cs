using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class FarmParam
    {
        public long Farmid { get; set; }
        public int Farmerid { get; set; }
        public int Farmtypeid { get; set; }
        public decimal? Farmsize { get; set; }
        public long? Farmaddressid { get; set; }

    }
}
