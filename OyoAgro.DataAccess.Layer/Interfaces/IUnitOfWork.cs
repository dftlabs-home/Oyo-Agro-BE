using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }
        IProfileAdditionalActivityRepository ProfileAdditionalActivities { get; }
        IUserProfileRepository UserProfile { get; } 
        IProfileActivityRepository ProfileActivity { get; }
        IProfileActivityParentRepository ProfileActivityParent { get; }
        IRegionRepository RegionRepository { get; }
        ILgaRepository LgaRepository { get; }
        IFarmerRepository FarmerRepository { get; }
        IFarmRepository FarmRepository { get; }
        
        int Complete();

    }
}
