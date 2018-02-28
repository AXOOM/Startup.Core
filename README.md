# Startup.Core

Startup.Core is a simple startup framework for .NET Core services, mimicking semantics of [ASP.NET Core startup](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/startup).

You can get it as a [NuGet Package](https://www.nuget.org/packages/Startup.Core/).

To use it first implement the `IStartup` interface like this:

```csharp
class Startup : IStartup
{
    public IConfigurationRoot Configuration { get; }

    public Startup()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddLogging()
            .AddOptions()
            .Configure<MyOptions>(Configuration.GetSection("MyOptions"))
            .AddTransient<IMyService, MyService>()
            .AddSingleton<Worker>();
    }

    public void Configure(ILoggerFactory loggerFactory, IServiceProvider provider)
    {
        loggerFactory.AddConsole(config.GetSection("Logging"));
        provider.GetService<Worker>();
    }
}
```

Then add this to your `Program.cs` file:

```csharp
public static void Main()
{
    Host.Run<Startup>();
}
```

## Building

Run `build.ps1` to compile the source code and create NuGet packages.

This script takes a version number as an input argument. The source code itself contains no version numbers. Instead version numbers should be determined at build time using [GitVersion](http://gitversion.readthedocs.io/).
