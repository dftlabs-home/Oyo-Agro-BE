using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class SeasonParam
    {
        public int Seasonid { get; set; }
        public string Name { get; set; } = null!;
        public int? Year { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }

    }
}
