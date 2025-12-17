using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities
{
    public class VwDashboardReportingBase : BaseEntity
    {
        public string? EnterpriseType { get; set; }
        public long? Farmid { get; set; }
        public int? Farmerid { get; set; }
        public string? FarmerFullName { get; set; }
        public string? Gender { get; set; }
        public DateOnly? Dateofbirth { get; set; }
        public string? FarmerEmail { get; set; }
        public string? FarmerPhone { get; set; }
        public int? Householdsize { get; set; }
        public int? Availablelabor { get; set; }
        public int? Associationid { get; set; }
        public string? AssociationName { get; set; }
        public string? Town { get; set; }
        public int? OfficerUserid { get; set; }
        public string? OfficerName { get; set; }
        public int? Lgaid { get; set; }
        public string? Lganame { get; set; }
        public int? Regionid { get; set; }
        public string? Regionname { get; set; }
        public int? Seasonid { get; set; }
        public string? SeasonName { get; set; }
        public int? SeasonYear { get; set; }
        public DateOnly? SeasonStartDate { get; set; }
        public DateOnly? SeasonEndDate { get; set; }
        public int? ItemTypeId { get; set; }
        public string? ItemTypeName { get; set; }
        public int? BusinessTypeId { get; set; }
        public string? BusinessTypeName { get; set; }
        public int? PrimaryProductId { get; set; }
        public string? PrimaryProductName { get; set; }
        public decimal? Farmsize { get; set; }
        public decimal? Areaplanted { get; set; }
        public decimal? Areaharvested { get; set; }
        public decimal? InputQuantity { get; set; }
        public decimal? OutputQuantity { get; set; }
        public decimal? HarvestPercentage { get; set; }
        public decimal? YieldPerArea { get; set; }
        public DateOnly? Plantingdate { get; set; }
        public DateOnly? Harvestdate { get; set; }
        public DateTime? FarmerCreatedAt { get; set; }
        public DateTime? FarmerUpdatedAt { get; set; }
        public int? RecordCount { get; set; }
    }
}
