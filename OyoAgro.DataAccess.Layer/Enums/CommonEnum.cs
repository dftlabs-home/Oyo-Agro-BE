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

    public enum RoleEnum
    {

        [Description("Admin")]
        Admin = 1,

        [Description("User")]
        User = 2,

        [Description("Restricted User")]
        Restricted = 3,


        [Description("Super User")]
        Super = 4,

        [Description("General")]
        General = 5
    }

}
