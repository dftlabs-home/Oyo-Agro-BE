using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class ProfileadditionalactivityParam
    {
        public int UserId { get; set; }
        public int additionalActivityId { get; set; }
        public int Activityid { get; set; }
        public bool Canadd { get; set; }
        public bool Canedit { get; set; }
        public bool Canview { get; set; }
        public bool Candelete { get; set; }
        public bool Canapprove { get; set; }
        public DateTime? Expireon { get; set; }


    }
}
