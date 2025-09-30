using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Models.Dtos
{
    public class OperatorInfo
    {
        public int UserId { get; set; } 
        public int LoginCount { get; set; } 
        public int UserStatus { get; set; } 
        public string? UserName { get; set; } 
        public string? ApiToken { get; set; }
        public int IsSystem { get; set; } 
    }
}
