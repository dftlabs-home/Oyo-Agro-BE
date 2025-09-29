using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Association : BaseEntity
    {
        public Association()
        {
            Farmers = new HashSet<Farmer>();
        }

        public int Associationid { get; set; }
        public Guid? Tempclientid { get; set; }
        public string Name { get; set; } = null!;
        public string? Registrationno { get; set; }
       
        public long? Version { get; set; }

        public virtual ICollection<Farmer> Farmers { get; set; }
    }
}
