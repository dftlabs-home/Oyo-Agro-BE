using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.DataAccess.Layer.Models.ViewModels
{
    public class FarmViewModel
    {
        public long Farmid { get; set; }
        public string? Farmer { get; set; }
        public string? Farmtype { get; set; }
        public decimal? Farmsize { get; set; }
        public string? Streetaddress { get; set; }
        public string? Town { get; set; }
        public string? Postalcode { get; set; }
        public string? Lga { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
