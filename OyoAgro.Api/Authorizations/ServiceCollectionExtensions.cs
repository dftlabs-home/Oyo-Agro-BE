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

            return services;
        }
    }
}
