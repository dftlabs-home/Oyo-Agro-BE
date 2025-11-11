using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public class PrimaryProduct : BaseEntity
    {
        public PrimaryProduct()
        {
            AgroAlliedRegistries = new HashSet<AgroAlliedRegistry>();
        }

        public int PrimaryProductTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<AgroAlliedRegistry> AgroAlliedRegistries { get; set; }
    }
}
