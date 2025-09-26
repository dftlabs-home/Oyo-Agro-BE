using System;
using System.Collections.Generic;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Notificationtarget
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
