using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class FarmerParam
    {
        public int Farmerid { get; set; }
        public string Firstname { get; set; } = null!;
        public string? Middlename { get; set; }
        public string Lastname { get; set; } = null!;
        public string? Gender { get; set; }
        public DateOnly? Dateofbirth { get; set; }
        public string? Email { get; set; }
        public string? Phonenumber { get; set; }
        public int? Associationid { get; set; }
        public int? Householdsize { get; set; }
        public int? Availablelabor { get; set; }
        public string? Photourl { get; set; }
        public string? Streetaddress { get; set; }
        public string? Town { get; set; }
        public string? Postalcode { get; set; }
        public int? Lgaid { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public long? Version { get; set; }
        public int UserId{ get; set; }

    }
}
