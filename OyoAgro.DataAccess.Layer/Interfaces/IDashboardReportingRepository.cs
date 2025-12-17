using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IDashboardReportingRepository
    {
        Task<List<VwDashboardReportingBase>> GetList();
    }
}
