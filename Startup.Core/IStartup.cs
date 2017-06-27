using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Startup.Core
{
    /// <summary>
    /// Controls an application's startup process using dependency injection.
    /// </summary>
    public interface IStartup
    {
        /// <summary>
        /// The application's configuration.
        /// </summary>
        IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Called to register services.
        /// </summary>
        /// <param name="services">Used to register services with the dependency injection container.</param>
        void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// Called to configure services after they have been registered.
        /// </summary>
        /// <param name="loggerFactory">Used to configure the logging framework.</param>
        /// <param name="provider">Used to instantiate and configure services.</param>
        void Configure(ILoggerFactory loggerFactory, IServiceProvider provider);
    }
}