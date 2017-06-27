using System;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Startup.Core
{
    /// <summary>
    /// Provides application hosting / lifecycle management.
    /// </summary>
    public static class Host
    {
        /// <summary>
        /// Runs a startup process and then blocks until SIGTERM or SIGINT is raised.
        /// </summary>
        /// <typeparam name="TStartup">The class used to control the application's startup process.</typeparam>
        public static void Run<TStartup>()
            where TStartup : IStartup, new()
        {
            var startup = new TStartup();

            var serviceCollection = new ServiceCollection();
            startup.ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            startup.Configure(serviceProvider.GetService<ILoggerFactory>(), serviceProvider);

            var wait = new ManualResetEventSlim(initialState: false);

            var disposable = serviceProvider as IDisposable;
            AssemblyLoadContext.GetLoadContext(typeof(TStartup).GetTypeInfo().Assembly).Unloading += context =>
            {
                disposable?.Dispose();
                wait.Set();
            };
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                disposable?.Dispose();
                wait.Set();
                eventArgs.Cancel = true;
            };

            Console.WriteLine("Application started. Press Ctrl+C to shut down.");
            wait.Wait();
        }
    }
}