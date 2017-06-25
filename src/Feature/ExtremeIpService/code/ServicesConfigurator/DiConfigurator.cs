using Bonfire.Feature.ExtremeIpService.Services;
using Bonfire.Foundation.Kickfire.Library.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Bonfire.Feature.ExtremeIpService.ServicesConfigurator
{
    public class DiConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            // register each repository and service
            serviceCollection.AddScoped<ICompanyService, Company>();
            
        }
    }
}