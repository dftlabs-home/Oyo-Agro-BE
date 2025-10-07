using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Models.ViewModels
{
    public class UsersViewModel
    {
        public int Userid { get; set; }
        public string Firstname { get; set; } = null!;
        public string? Middlename { get; set; }
        public string Lastname { get; set; } = null!;
        public string? Designation { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Phonenumber { get; set; }
        public string? Photo { get; set; }
        public int? Roleid { get; set; }
        public string? Lga { get; set; }
        public int FarmCount { get; set; }
        public int FarmerCount { get; set; }
        public List<FarmerViewModel>? Farmers { get; set; }

    }

    public class FarmerViewModel
    {
        public int Farmerid { get; set; }
        public string Firstname { get; set; } = null!;
        public string? Middlename { get; set; }
        public string Lastname { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? Dateofbirth { get; set; }
        public string? Email { get; set; }
        public string? Phonenumber { get; set; }
        public string? Association { get; set; }
        public int? Householdsize { get; set; }
        public int? Availablelabor { get; set; }
        public string? Photourl { get; set; }      
        public string? Lga { get; set; }
        public int FarmCount { get; set; }

        public List<FarmsViewModel>? Farms { get; set; }

    }
     public class FarmsViewModel
    {
        public long Farmid { get; set; }
        public int Farmtypeid { get; set; }
        public decimal? Farmsize { get; set; }
        public string? Streetaddress { get; set; }
        public string? Town { get; set; }
        public string? Postalcode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }

}
