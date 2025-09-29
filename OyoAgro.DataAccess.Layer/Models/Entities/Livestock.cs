using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Livestock : BaseEntity
    {
        public Livestock()
        {
            Livestockregistries = new HashSet<Livestockregistry>();
        }

        public int Livestocktypeid { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Livestockregistry> Livestockregistries { get; set; }
    }
}
