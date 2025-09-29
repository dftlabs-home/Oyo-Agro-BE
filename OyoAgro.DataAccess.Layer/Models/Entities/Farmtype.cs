using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Farmtype : BaseEntity
    {
        public Farmtype()
        {
            Farms = new HashSet<Farm>();
        }

        public int Farmtypeid { get; set; }
        public string Typename { get; set; } = null!;

        public virtual ICollection<Farm> Farms { get; set; }
    }
}
