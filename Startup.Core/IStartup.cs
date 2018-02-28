using System;
using Microsoft.Extensions.DependencyInjection;

namespace Startup.Core
{
    /// <summary>
    /// Controls an application's startup process using dependency injection.
    /// </summary>
    public interface IStartup
    {
        /// <summary>
        /// Called to register services.
        /// </summary>
        /// <param name="services">Used to register services with the dependency injection container.</param>
        IServiceProvider ConfigureServices(IServiceCollection services);

        /// <summary>
        /// Called to configure services after they have been registered.
        /// </summary>
        /// <param name="provider">Used to instantiate and configure services.</param>
        void Configure(IServiceProvider provider);
    }
}