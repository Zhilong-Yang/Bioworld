namespace Bioworld.Auth.Distributed
{
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public static  class Extensions
    {
        private const string RegistryName = "auth.distributed";

        public static IBioWorldBuilder AddDistributedAccessTokenValidator(this IBioWorldBuilder builder)
        {
            if (!builder.TryRegister(RegistryName))
            {
                return builder;
            }

            builder.Services.AddSingleton<IAccessTokenService, DistributedAccessTokenService>();
            return builder;
        }
    }
}