using Microsoft.Extensions.DependencyInjection;

namespace RazorWebLibrary
{
    public static class MyFeatureStartup
    {        
        public static void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureOptions(typeof(ViewConfigureOptions));
            services.ConfigureOptions(typeof(ContentConfigureOptions));
        }
    }
}
