namespace Bioworld.Secrets.Vault
{
    using System.Collections.Generic;

    public interface ILeaseService
    {
        IReadOnlyDictionary<string, LeaseData> All { get; }
        LeaseData Get(string key);
        void Set(string key, LeaseData data);
    }
}