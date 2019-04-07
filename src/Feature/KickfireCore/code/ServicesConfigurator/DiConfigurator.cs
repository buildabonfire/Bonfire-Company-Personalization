using Bonfire.Feature.KickfireCore.Repository;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Bonfire.Feature.KickfireCore.ServicesConfigurator
{
    public class DiConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            // register each repository and service
            serviceCollection.AddScoped<ISicCodeGroupRepository, SicCodeGroupGroupRepository>();
        }
    }
}