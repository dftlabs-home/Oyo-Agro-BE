using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;

namespace OyoAgro.BusinessLogic.Layer.Interfaces
{
    public interface IDashboardReportingService
    {
        Task<List<VwDashboardReportingBase>> GetDashboard();
    }
}
