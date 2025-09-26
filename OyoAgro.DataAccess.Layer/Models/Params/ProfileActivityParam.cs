using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class ProfileActivityParam
    {
        public int Activityid { get; set; }
        public int Activityparentid { get; set; }
        public string Activityname { get; set; } = null!;

    }
}
