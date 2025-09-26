using System;
using System.Collections.Generic;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Synclog
    {
        public long Synclogid { get; set; }
        public string Tablename { get; set; } = null!;
        public Guid? Tempclientid { get; set; }
        public long? Serverid { get; set; }
        public DateTime? Changedat { get; set; }
        public string? Clientid { get; set; }
        public bool? Processed { get; set; }
    }
}
