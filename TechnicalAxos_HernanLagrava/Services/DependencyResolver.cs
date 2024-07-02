
namespace TechnicalAxos_HernanLagrava.Services
{
    public static class DependencyResolver
    {
        private static IServiceProvider _serviceProvider;

        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static TService GetService<TService>()
        {
            return _serviceProvider.GetService<TService>();
        }
    }
}
