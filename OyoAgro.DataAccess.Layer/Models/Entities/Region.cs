using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Region : BaseEntity
    {
        public Region()
        {
            Lgas = new HashSet<Lga>();
            Notificationtargets = new HashSet<Notificationtarget>();
            Userregions = new HashSet<Userregion>();
        }

        public int Regionid { get; set; }
        public Guid? Tempclientid { get; set; }
        public string Regionname { get; set; } = null!;
        public long? Version { get; set; }

        public virtual ICollection<Lga> Lgas { get; set; }
        public virtual ICollection<Notificationtarget> Notificationtargets { get; set; }
        public virtual ICollection<Userregion> Userregions { get; set; }
    }

}
