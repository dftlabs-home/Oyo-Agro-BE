using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.BusinessLogic.Layer.Services;

namespace OyoAgro.Api.Authorizations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserProfileServie, UserProfileServie>();
            services.AddScoped<IProfileActivityService, ProfileActivityService>();
            services.AddScoped<IProfileActivityParentService, ProfileActivityParentService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<ILgaServices, LgaServices>();
            services.AddScoped<IFarmerSevice, FarmerSevice>();
            services.AddScoped<IFarmService, FarmService>();
            services.AddScoped<IAssociationService, AssociationService>();
            services.AddScoped<ICropRegistryService, CropRegistryService>();
            services.AddScoped<ILiveStockRegistryService, LiveStockRegistryService>();
            services.AddScoped<ICropService, CropService>();
            services.AddScoped<IFarmTypeService, FarmTypeService>();
            services.AddScoped<ILiveStockService, LiveStockService>();
            services.AddScoped<INotificationTargetService, NotificationTargetService>();

            return services;
        }
    }
}
