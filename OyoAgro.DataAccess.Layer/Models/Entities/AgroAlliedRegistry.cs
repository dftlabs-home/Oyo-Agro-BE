using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Enums;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public class AgroAlliedRegistry: BaseEntity
    {
        public int AgroAlliedRegistryid { get; set; }
        public Guid? Tempclientid { get; set; }

        public long Farmid { get; set; }
        public int Seasonid { get; set; }

        public int BusinessTypeId { get; set; }
        public int PrimaryProductTypeId { get; set; }
        public decimal ProductionCapacity { get; set; }
        public virtual Farm Farm { get; set; } = null!;
        public virtual Season Season { get; set; } = null!;
        public virtual BusinessType BusinessType { get; set; } = null!;
        public virtual PrimaryProduct PrimaryProduct{ get; set; } = null!;


    }
}
