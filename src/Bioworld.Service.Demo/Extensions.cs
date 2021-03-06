namespace Bioworld.Service.Demo
{
    using WebApi.Security;

    public static class Extensions
    {
        public static IBioWorldBuilder AddServices(this IBioWorldBuilder builder)
        {
            builder.AddCertificateAuthentication();
            //builder.Services.AddSingleton<IPricingServiceClient, PricingServiceClient>();
            return builder;
        }
    }
}