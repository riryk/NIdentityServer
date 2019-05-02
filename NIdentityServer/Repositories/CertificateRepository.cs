using System;
using System.Security.Cryptography.X509Certificates;

namespace NIdentityServer
{
    public static class CertificateRepository
    {
        public static X509Certificate2 GetTokenSigningCertificate(string certificate)
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                var issuedTo = certificate;
                var found = store.Certificates.Find(X509FindType.FindByIssuerName, issuedTo, validOnly: false);

                if (found.Count == 0)
                {
                    throw new Exception($"Certificate issued to {issuedTo} is not found");
                }

                return found[0];
            }
            finally
            {
                store.Close();
            }
        } 
    }
}