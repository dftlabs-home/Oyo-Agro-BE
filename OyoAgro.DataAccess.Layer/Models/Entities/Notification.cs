using System;
using System.Collections.Generic;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Notification
    {
        public Notification()
        {
            Notificationtargets = new HashSet<Notificationtarget>();
        }

        public long Notificationid { get; set; }
        public Guid? Tempclientid { get; set; }
        public int? Createdby { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public bool? Isread { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public DateTime? Deletedat { get; set; }
        public long? Version { get; set; }

        public virtual Useraccount? CreatedbyNavigation { get; set; }
        public virtual ICollection<Notificationtarget> Notificationtargets { get; set; }
    }
}
