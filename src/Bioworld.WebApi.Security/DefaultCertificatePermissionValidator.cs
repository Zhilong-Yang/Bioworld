namespace Bioworld.WebApi.Security
{
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.AspNetCore.Http;

    public class DefaultCertificatePermissionValidator : ICertificatePermissionValidator
    {
        public bool HasAccess(X509Certificate2 certificate, IEnumerable<string> permission, HttpContext context) 
            => true;
    }
}