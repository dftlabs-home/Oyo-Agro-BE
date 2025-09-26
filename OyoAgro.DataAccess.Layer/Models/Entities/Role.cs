using System;
using System.Collections.Generic;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Role
    {
        public int Roleid { get; set; }
        public string Rolename { get; set; } = null!;
    }
}
