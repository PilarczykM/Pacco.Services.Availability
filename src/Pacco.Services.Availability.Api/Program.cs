using System.Threading.Tasks;

using Convey;
using Convey.WebApi;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Pacco.Services.Availability.Application;
using Pacco.Services.Availability.Infrastructure;

namespace Pacco.Services.Availability.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await CreateWebHostBuilder(args)
                .Build()
                .RunAsync();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => WebHost.CreateDefaultBuilder(args)
            .ConfigureServices(services => services
                .AddConvey()
                .AddWebApi()
                .AddApplication()
                .AddInfrastructure()
                .Build())
            .Configure(app => app
                .UseInfrastructure()
                .UseEndpoints(e => e.MapControllers()))
            .UseStartup<Startup>();
    }
}