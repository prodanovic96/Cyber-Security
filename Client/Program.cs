using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {        
            string srvCertCN = "wcfservice";
        
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            /// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:12346/TextFile"),
                                      new X509CertificateEndpointIdentity(srvCert));

            using (WCFClient proxy = new WCFClient(binding, address))
            {
               
                 Console.WriteLine("Konekcija uspesno ostvarena\n");
                 while (true)
                 {
                        Console.WriteLine("Izaberite opciju:");
                        Console.WriteLine("\t1) Kreiraj folder");
                        Console.WriteLine("\t2) Kreiraj fajl");
                        Console.WriteLine("\t3) Izlistaj folder");
                        Console.WriteLine("\t4) Procitaj sadrzaj fajla");
                        Console.WriteLine("\t5) Premesti fajl");
                        Console.WriteLine("\t6) Promeni naziv fajla");
                        Console.WriteLine("\t7) Obrisi fajl");
                        Console.WriteLine("\t8) Zatvori aplikaciju");
                        
                        string s;
                        string m;

                        string broj = Console.ReadLine();


                        if (broj == "1")
                        {
                            Console.WriteLine("Unesite naziv foldera koji zelite da kreirate: ");
                            s = Console.ReadLine();

                            proxy.KreirajFolder(s);

                        }
                        else if (broj == "2")
                        {
                            Console.WriteLine("Unesite naziv fajla koji zelite da kreirate: ");
                            s = Console.ReadLine();
                            Console.WriteLine("Unesite sadrzaj fajla: ");
                            m = Console.ReadLine();

                            proxy.KreirajFajl(s, m);
                        }
                        else if (broj == "3")
                        {
                            proxy.IzlistajFolder();
                        }
                        else if (broj == "4")
                        {
                            Console.WriteLine("Unesite ime fajla koji zelite da procitate: ");
                            s = Console.ReadLine();

                            proxy.ProcitajSadrzajFajla(s);
                        }
                        else if (broj == "5")
                        {
                            Console.WriteLine("Unesite naziv fajla koji zelite da premestite: ");
                            s = Console.ReadLine();

                            proxy.Premesti(s);
                        }
                        else if (broj == "6")
                        {
                            Console.WriteLine("Unesite naziv fajla koji zelite da preimenujete: ");
                            s = Console.ReadLine();
                            Console.WriteLine("Unesite novi naziv izabranog fajla: ");
                            m = Console.ReadLine();

                            proxy.PromeniNaziv(s, m);
                        }                    
                        else if (broj == "7")
                        {
                            Console.WriteLine("Unesite naziv fajla koji zelite da obrisete: ");
                            s = Console.ReadLine();

                            proxy.Obrisi(s);
                        }
                        else if(broj == "8")
                        {
                            proxy.Dispose();
                            Environment.Exit(0);
                        }

                 } 
            }
        }
    }
}

