using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Userregion : BaseEntity
    {
        public int Userregionid { get; set; }
        public Guid? Tempclientid { get; set; }
        public int Userid { get; set; }
        public int Regionid { get; set; }      
        public long? Version { get; set; }

        public virtual Region Region { get; set; } = null!;
        public virtual Useraccount User { get; set; } = null!;
    }
}
