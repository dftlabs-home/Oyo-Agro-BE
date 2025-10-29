using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models;
using OyoAgro.DataAccess.Layer.Repositories;

namespace OyoAgro.DataAccess.Layer
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        private IUserRepository? _users;
        private IProfileAdditionalActivityRepository? _profileadditionalactivity;
        private IProfileActivityRepository? _profileactivity;
        private IUserProfileRepository? _userprofile;
        private IProfileActivityParentRepository? _profileactivityparent;
        private IRegionRepository? _regionRepository;
        private ILgaRepository? _lgaRepository;
        private IFarmerRepository? _farmerRepository;
        private IFarmRepository? _farmRepository;
        private IAddressRepository? _addressRepository;
        private IUserRegionRepository? _userRegionRepository;
        private IAssociationRepository? _associationRepository;
        private ICropRepository? _cropRepository;
        private IFarmTypeRepository? _farmTypeRepository;
        private ISeasonRepository? _seasonRepository;
        private ICropRegistryRepository? _cropRegistryRepository;
        private ILiveStockRegistryRepository? _liveStockRegistryRepository;
        private ILivestockRepository? _livestockRepository;
        private INotificationTargetRepository? _notificationTargetRepository;
        private INotificationRepository? _notificationRepository;
        private IDashboardMetricsRepository? _dashboardMetricsRepository;
        
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
      
        public IUserRepository Users =>
         _users ??= new UserRepository();

         public IProfileAdditionalActivityRepository ProfileAdditionalActivities =>
         _profileadditionalactivity ??= new ProfileAdditionalActivityRepository();

           public IUserProfileRepository UserProfile =>
         _userprofile ??= new UserProfileRepository();

           public IProfileActivityRepository ProfileActivity =>
         _profileactivity ??= new ProfileActivityRepository();

          public IProfileActivityParentRepository ProfileActivityParent =>
         _profileactivityparent ??= new ProfileActivityParentRepository();

         public IRegionRepository RegionRepository =>
         _regionRepository ??= new RegionRepository();

         public ILgaRepository LgaRepository =>
         _lgaRepository ??= new LgaRepository();

           public IFarmerRepository FarmerRepository =>
         _farmerRepository ??= new FarmerRepository();

          public IFarmRepository FarmRepository =>
         _farmRepository ??= new FarmRepository();
            public IAddressRepository AddressRepository =>
         _addressRepository ??= new AddressRepository();

         public IUserRegionRepository UserRegionRepository =>
         _userRegionRepository ??= new UserRegionRepository();

           public IAssociationRepository AssociationRepository=>
         _associationRepository ??= new AssociationRepository();

          public ICropRepository CropRepository=>
         _cropRepository ??= new CropRepository();

         public IFarmTypeRepository FarmTypeRepository=>
         _farmTypeRepository ??= new FarmTypeRepository();

         public ISeasonRepository SeasonRepository=>
         _seasonRepository ??= new SeasonRepository();

        public ICropRegistryRepository CropRegistryRepository=>
         _cropRegistryRepository ??= new CropRegistryRepository();

          public ILiveStockRegistryRepository LiveStockRegistryRepository=>
         _liveStockRegistryRepository ??= new LiveStockRegistryRepository();

          public ILivestockRepository LivestockRepository=>
         _livestockRepository ??= new LivestockRepository();

        public INotificationTargetRepository NotificationTargetRepository=>
         _notificationTargetRepository ??= new NotificationTargetRepository();

           public INotificationRepository NotificationRepository=>
         _notificationRepository ??= new NotificationRepository();

        //public IDashboardMetricsRepository DashboardMetricsRepository =>
        // _dashboardMetricsRepository ??= new DashboardMetricsRepository(_context);


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
