using System;
using System.Security.Cryptography.X509Certificates;

namespace GdNet.Accounts
{
    /// <summary>
    /// A utility class which helps to retrieve an x509 certificate
    /// </summary>
    public class CertificateUtil
    {
        public static X509Certificate2 GetCertificate(StoreName name, StoreLocation location, string subjectName)
        {
            var store = new X509Store(name, location);
            store.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certificates = null;

            try
            {
                X509Certificate2 result = null;

                //
                // Every time we call store.Certificates property, a new collection will be returned.
                //
                certificates = store.Certificates;

                foreach (X509Certificate2 cert in certificates)
                {
                    if (!string.IsNullOrEmpty(cert.SubjectName.Name) && cert.SubjectName.Name.ToLower() == subjectName.ToLower())
                    {
                        if (result != null)
                        {
                            throw new ApplicationException(string.Format("There are multiple certificates for subject Name {0}", subjectName));
                        }

                        result = new X509Certificate2(cert);
                    }
                }

                if (result == null)
                {
                    throw new ApplicationException(string.Format("No certificate was found for subject Name {0}", subjectName));
                }

                return result;
            }
            finally
            {
                if (certificates != null)
                {
                    foreach (var cert in certificates)
                    {
                        cert.Reset();
                    }
                }

                store.Close();
            }
        }
    }
}

