using System.Diagnostics.CodeAnalysis;

namespace MvcTemplate.Web;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main()
    {
        new WebHostBuilder()
            .UseDefaultServiceProvider(options => options.ValidateOnBuild = true)
            .UseKestrel(options => options.AddServerHeader = false)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseStartup<Startup>()
            .UseIISIntegration()
            .UseIIS()
            .Build()
            .Run();
    }
}
