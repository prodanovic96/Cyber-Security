using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Common
{
    public class CertManager
    {
        /// <summary>
        /// Get a certificate with the specified subject name from the predefined certificate storage
        /// Only valid certificates should be considered
        /// </summary>
        /// <param name="storeName"></param>
        /// <param name="storeLocation"></param>
        /// <param name="subjectName"></param>
        /// <returns> The requested certificate. If no valid certificate is found, returns null. </returns>
        //public static X509Certificate2 GetCertificateFromStorage(StoreName storeName, StoreLocation storeLocation, string subjectName)
        //{
        //    X509Store store = new X509Store(storeName, storeLocation);
        //    store.Open(OpenFlags.ReadOnly);

        //    X509Certificate2Collection certCollection = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, true);

        //    /// Check whether the subjectName of the certificate is exactly the same as the given "subjectName"
        //    foreach (X509Certificate2 c in certCollection)
        //    {
        //        if (c.SubjectName.Name.Equals(string.Format("CN={0}", subjectName)))
        //        {
        //            return c;
        //        }
        //    }

        //    return null;
        //}

        public static X509Certificate2 GetCertificateFromStorage(StoreName storeName, StoreLocation storeLocation, string subjectName)
        {
            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);
            string certificateName = string.Empty;
            string username = string.Empty;

            foreach (X509Certificate2 certificate in store.Certificates)
            {
                certificateName = certificate.SubjectName.Name;
                username = Formatter.IzvadiUsername(certificateName);
                if (username.Equals(subjectName))
                {             
                    return certificate;
                }

            }

            return null;
        }


    }
}
