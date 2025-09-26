using System;
using System.Collections.Generic;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Association
    {
        public Association()
        {
            Farmers = new HashSet<Farmer>();
        }

        public int Associationid { get; set; }
        public Guid? Tempclientid { get; set; }
        public string Name { get; set; } = null!;
        public string? Registrationno { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public DateTime? Deletedat { get; set; }
        public long? Version { get; set; }

        public virtual ICollection<Farmer> Farmers { get; set; }
    }
}
