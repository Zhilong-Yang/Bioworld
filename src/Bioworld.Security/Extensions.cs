namespace Bioworld.Security
{
    using Internals;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extensions
    {
        public static IBioWorldBuilder AddSecurity(this IBioWorldBuilder builder)
        {
            builder.Services
                .AddSingleton<IEncryptor, Encryptor>()
                .AddSingleton<IHasher, Hasher>()
                .AddSingleton<ISigner, Signer>();

            return builder;
        }
    }
}