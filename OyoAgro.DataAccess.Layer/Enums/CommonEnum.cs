using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Enums
{
    public enum StatusEnum
    {
        [Description("Enable")]
        Yes = 1,

        [Description("Disable")]
        No = 0
    }
}
