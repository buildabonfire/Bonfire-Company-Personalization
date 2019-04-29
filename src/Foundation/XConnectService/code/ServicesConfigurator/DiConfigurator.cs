using Bonfire.Foundation.XConnectService.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Bonfire.Foundation.XConnectService.ServicesConfigurator
{
    public class DiConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            // register each repository and service
            serviceCollection.AddTransient<IEventTrackerService, EventTrackerService>();
        }
    }
}