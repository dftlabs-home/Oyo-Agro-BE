using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Entities;

namespace OyoAgro.BusinessLogic.Layer.Services
{
    public class DashboardReportingService : IDashboardReportingService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DashboardReportingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<List<VwDashboardReportingBase>> GetDashboard()
        {
            var data = await _unitOfWork.DashboardReportingRepository.GetList();
            return data;
        }
    }
}
