using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Models.Dtos
{
    public class OperatorInfo
    {
        public int userid { get; set; } 
        public int logincount { get; set; } 
        public int status { get; set; } 
        public string? username { get; set; } 
        public string? apitoken { get; set; }
        //public int IsSystem { get; set; } 
    }
}
