using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Lga : BaseEntity
    {
        public Lga()
        {
            Addresses = new HashSet<Address>();
            Notificationtargets = new HashSet<Notificationtarget>();
        }

        public int Lgaid { get; set; }
        public Guid? Tempclientid { get; set; }
        public string Lganame { get; set; } = null!;
        public int Regionid { get; set; }      
        public long? Version { get; set; }

        public virtual Region Region { get; set; } = null!;
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Notificationtarget> Notificationtargets { get; set; }
    }
}
