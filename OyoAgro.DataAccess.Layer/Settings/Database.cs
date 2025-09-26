using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Settings
{
    public class Database
    {
        public string? DBProvider { get; set; }
        public string? DBConnectionString { get; set; }
        public string? DBHangfireServer { get; set; }
        public int DBCommandTimeout { get; set; }
        public string? DBBackup { get; set; }
    }
}
