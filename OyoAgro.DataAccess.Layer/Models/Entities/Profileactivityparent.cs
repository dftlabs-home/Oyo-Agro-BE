using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Profileactivityparent :BaseEntity
    {
        public Profileactivityparent()
        {
            Profileactivities = new HashSet<Profileactivity>();
        }

        public int Activityparentid { get; set; }
        public string Activityparentname { get; set; } = null!;
       
        public virtual ICollection<Profileactivity> Profileactivities { get; set; }
    }
}
