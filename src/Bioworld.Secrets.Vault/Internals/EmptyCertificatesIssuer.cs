namespace Bioworld.Secrets.Vault.Internals
{
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    public class EmptyCertificatesIssuer : ICertificatesIssuer
    {
        public Task<X509Certificate2> IssueAsync()
            => Task.FromResult<X509Certificate2>(null);
    }
}