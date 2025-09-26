using System;
using System.Collections.Generic;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Livestock
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
