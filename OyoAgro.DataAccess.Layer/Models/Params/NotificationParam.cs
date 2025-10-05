using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class NotificationParam
    {
        public long Notificationid { get; set; }
        public int? Createdby { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public bool? Isread { get; set; }
    }
}
