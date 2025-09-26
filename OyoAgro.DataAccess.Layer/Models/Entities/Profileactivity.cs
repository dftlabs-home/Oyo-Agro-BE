using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Profileactivity : BaseEntity
    {
        public Profileactivity()
        {
            Profileadditionalactivities = new HashSet<Profileadditionalactivity>();
        }

        public int Activityid { get; set; }
        public int Activityparentid { get; set; }
        public string Activityname { get; set; } = null!;
       
        public virtual Profileactivityparent Activityparent { get; set; } = null!;
        public virtual ICollection<Profileadditionalactivity> Profileadditionalactivities { get; set; }
    }
}
