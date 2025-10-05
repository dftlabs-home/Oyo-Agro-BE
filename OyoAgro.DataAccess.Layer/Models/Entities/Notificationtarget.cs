using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Notificationtarget : BaseEntity
    {
        public long Targetid { get; set; }
        public long Notificationid { get; set; }
        public int? Regionid { get; set; }
        public int? Lgaid { get; set; }
        public int? Userid { get; set; }

        public virtual Lga? Lga { get; set; }
        public virtual Notification Notification { get; set; } = null!;
        public virtual Region? Region { get; set; }
        public virtual Useraccount? User { get; set; }
    }
}
