using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OyoAgro.DataAccess.Layer.Configurations;
using OyoAgro.DataAccess.Layer.Settings;

namespace OyoAgro.DataAccess.Layer
{
    public class SystemConfig
    {
        public static Configuration? Instance { get; set; }
        public static SqlConnection? Connection { get; set; }
        public static Database? Database { get; set; }
        public static string? Origins { get; set; }
        public bool Demo { get; set; } // Whether it is Demo mode        
        public bool Debug { get; set; } // Is it in debug mode
        public bool LoginMultiple { get; set; } // Allows a user to log in on multiple computers at the same time
        public string? LoginProvider { get; set; } // Login provider
        public int SnowFlakeWorkerId { get; set; } // Snowflake ID
        public string? ApiSite { get; set; } // Api address
        public string? VirtualDirectory { get; set; } // Website virtual directory
        public string? CacheProvider { get; set; } // Cache type
        public string? RedisConnectionString { get; set; } // cache string
        public static string? DefaultScheme { get; set; }
        public int DBSlowSqlLogTime { get; set; }
        public static Jwt? JWTSecret { get; set; }

    }
}
