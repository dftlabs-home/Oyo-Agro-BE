using System;
using System.Collections.Generic;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Userregion
    {
        public int Userregionid { get; set; }
        public Guid? Tempclientid { get; set; }
        public int Userid { get; set; }
        public int Regionid { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public DateTime? Deletedat { get; set; }
        public long? Version { get; set; }

        public virtual Region Region { get; set; } = null!;
        public virtual Useraccount User { get; set; } = null!;
    }
}
