using System;
using System.Collections.Generic;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Farmer : BaseEntity
    {
        public Farmer()
        {
            Farms = new HashSet<Farm>();
        }

        public int Farmerid { get; set; }
        public Guid? Tempclientid { get; set; }
        public string Firstname { get; set; } = null!;
        public string? Middlename { get; set; }
        public string Lastname { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? Dateofbirth { get; set; }
        public string? Email { get; set; }
        public string? Phonenumber { get; set; }
        public int? Associationid { get; set; }
        public int? Householdsize { get; set; }
        public int? Availablelabor { get; set; }
        public int? UserId { get; set; }
        public string? Photourl { get; set; }      
        public long? Version { get; set; }

        public virtual Association? Association { get; set; }
        public virtual ICollection<Farm> Farms { get; set; }
    }
}
