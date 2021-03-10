namespace Bioworld.Service.Demo
{
    using Bioworld;
    using CQRS.Commands;
    using CQRS.Queries;
    using Docs.Swagger;
    using WebApi.Swagger;
    using Bioworld.CQRS.Events;
    using Bioworld.WebApi.CQRS;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;
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
                        .AddCommandHandlers()
                        .AddEventHandlers()
                        .AddQueryHandlers()
                        .AddInMemoryCommandDispatcher()
                        .AddInMemoryEventDispatcher()
                        .AddInMemoryQueryDispatcher()
                        .AddWebApi()
                        .AddSwaggerDocs()
                        .AddWebApiSwaggerDocs()
                        .Build())
                    .Configure(app => app
                        .UseBioWorld()
                        .UseErrorHandler()
                        .UseRouting()
                        .UseCertificateAuthentication()
                        .UseEndpoints(r => r.MapControllers())
                        .UseDispatcherEndpoints(endpoints => endpoints
                            .Get("", ctx => ctx.Response.WriteAsync("Orders Service"))
                            .Get("ping", ctx => ctx.Response.WriteAsync("pong")))
                        .UseSwaggerDocs()
                    );
            });
    }
}