namespace Bioworld.Service.Demo
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;
    using Bioworld;
    using WebApi;
    using WebApi.Security;
    using Microsoft.AspNetCore.Builder;

    public class Program
    {
        public static Task Main(string[] args)
            => CreateHostBuilder(args).Build().RunAsync();

        // public static IHostBuilder CreateHostBuilder(string[] args) 
        //     => Host.CreateDefaultBuilder(args)
        //         .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services => services
                        .AddBioWorld()
                        .AddErrorHandler<ExceptionToResponseMapper>()
                        .AddServices()
                        .AddWebApi()
                        .Build())
                    .Configure(app => app
                        .UseBioWorld()
                        .UseErrorHandler()
                        .UseRouting()
                        .UseCertificateAuthentication()
                        .UseEndpoints(r => r.MapControllers())
                    );
            });
    }
}