using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Entities.Operator;

namespace OyoAgro.DataAccess.Layer.Repositories
{
    public class DashboardReportingRepository : DataRepository, IDashboardReportingRepository
    {
        public async Task<List<VwDashboardReportingBase>> GetList()
        {
            var list = await BaseRepository().FindList<VwDashboardReportingBase>();
            return list.ToList();
        }

        
    }
}
