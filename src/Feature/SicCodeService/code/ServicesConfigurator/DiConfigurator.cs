using Bonfire.Feature.SicCodeService.Repositories;
using Bonfire.Foundation.Kickfire.Library.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Bonfire.Feature.SicCodeService.ServicesConfigurator
{
    public class DiConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            // register each repository and service
            serviceCollection.AddScoped<ISicCodeRepository, SicCodeRepository>();

            // see what is registered at /sitecore/admin/showservicesconfig.aspx
        }
    }
}