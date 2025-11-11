using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Enums;
using OyoAgro.DataAccess.Layer.Models.Entities;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class AgroAlliedRegistryParam
    {
        public int AgroAlliedRegistryid { get; set; }
        public long Farmid { get; set; }
        public int Seasonid { get; set; }

        public int BusinessTypeId { get; set; }
        public int PrimaryProductId { get; set; }
        public decimal ProductionCapacity { get; set; }
    }
}
