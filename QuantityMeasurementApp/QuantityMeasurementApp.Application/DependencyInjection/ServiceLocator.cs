using Microsoft.Extensions.DependencyInjection;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Service.Services;
using QuantityMeasurementApp.Repository.Interfaces;
using QuantityMeasurementApp.Repository.Implementations;

namespace QuantityMeasurementApp.Application.DependencyInjection
{
    /// <summary>
    /// Uses Microsoft's built-in DI container with proper lifetime management
    /// </summary>
    public static class ServiceLocator
    {
        private static IServiceProvider _serviceProvider;

        /// <summary>
        /// Configures services with appropriate lifetimes
        /// </summary>
        public static void ConfigureServices(IQuantityHistoryRepository repository)
        {
            var services = new ServiceCollection();

              // Register repository
            services.AddSingleton(repository);

            // Register services with appropriate lifetimes
            
            // Singleton - One instance for entire application
            // Good for stateless services with no dependencies that change
            services.AddSingleton<IConversionService, ConversionService>();
            
            // Scoped - One instance per scope/operation
            // In console app, we create one scope for the entire run
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IEqualityService, EqualityService>();
            services.AddScoped<IArithmeticService, ArithmeticService>();
            services.AddScoped<IQuantityService, QuantityService>();

            // Build the service provider
            _serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Gets a service from the current scope
        /// </summary>
        public static T GetService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();

        }
    }
}