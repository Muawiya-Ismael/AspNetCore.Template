using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace MvcTemplate.Data;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main()
    {
    }

    public static IWebHost BuildWebHost(params String[] _)
    {
        return new WebHostBuilder()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseStartup<Startup>()
            .UseKestrel()
            .Build();
    }
}
