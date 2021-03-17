﻿using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Bioworld.Secrets.Vault
{
    public interface ICertificatesIssuer
    {
        Task<X509Certificate2> IssueAsync();
    }
}