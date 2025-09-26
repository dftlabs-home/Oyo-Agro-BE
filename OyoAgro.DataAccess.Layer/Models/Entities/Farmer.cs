using System;
using System.Collections.Generic;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public partial class Farmer
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
        public DateOnly? Dateofbirth { get; set; }
        public string? Email { get; set; }
        public string? Phonenumber { get; set; }
        public int? Associationid { get; set; }
        public long Residentialaddressid { get; set; }
        public int? Householdsize { get; set; }
        public int? Availablelabor { get; set; }
        public string? Photourl { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public DateTime? Deletedat { get; set; }
        public long? Version { get; set; }

        public virtual Association? Association { get; set; }
        public virtual Address Residentialaddress { get; set; } = null!;
        public virtual ICollection<Farm> Farms { get; set; }
    }
}
