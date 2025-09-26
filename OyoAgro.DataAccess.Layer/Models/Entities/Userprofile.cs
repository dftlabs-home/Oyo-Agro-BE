using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Userprofile : BaseEntity
    {
        public int Userprofileid { get; set; }
        public Guid? Tempclientid { get; set; }
        public int Userid { get; set; }
        public string Firstname { get; set; } = null!;
        public string? Middlename { get; set; }
        public string Lastname { get; set; } = null!;
        public string? Designation { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Phonenumber { get; set; }
        public string? Photo { get; set; }
        public long? Residentialaddressid { get; set; }      
        public long? Version { get; set; }
        public int? Roleid { get; set; }
        public int? Lgaid { get; set; }
        public string? Address{ get; set; }

        public virtual Address? Residentialaddress { get; set; }
        public virtual Useraccount User { get; set; } = null!;
    }
}
