using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class NotificationTargetParam
    {
        public long Notificationid { get; set; }
        public int? Regionid { get; set; }
        public int? Lgaid { get; set; }
        public int? Userid { get; set; }

    }
}
