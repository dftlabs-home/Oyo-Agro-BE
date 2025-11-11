using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class PrimaryProductParam
    {
        public int PrimaryProductTypeId { get; set; }
        public string Name { get; set; } = null!;

    }
}
