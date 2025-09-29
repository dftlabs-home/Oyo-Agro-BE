using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Crop : BaseEntity
    {
        public Crop()
        {
            Cropregistries = new HashSet<Cropregistry>();
        }

        public int Croptypeid { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Cropregistry> Cropregistries { get; set; }
    }
}
