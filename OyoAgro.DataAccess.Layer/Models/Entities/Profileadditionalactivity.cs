using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{

    public partial class Profileadditionalactivity: BaseEntity
    {
        public int Additionalactivityid { get; set; }
        public int Userid { get; set; }
        public int Activityid { get; set; }
        public bool Canadd { get; set; }
        public bool Canedit { get; set; }
        public bool Canview { get; set; }
        public bool Candelete { get; set; }
        public bool Canapprove { get; set; }
        public DateTime? Expireon { get; set; }
       
        public virtual Profileactivity Activity { get; set; } = null!;
        public virtual Useraccount User { get; set; } = null!;
    }
}
