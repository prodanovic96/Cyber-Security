using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class WCFClient : ChannelFactory<IWCFService>, IDisposable
    {

        IWCFService factory;

        public WCFClient(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {
            
            /// cltCertCN.SubjectName should be set to the client's username. .NET WindowsIdentity class provides information about Windows user running the given process
            string cltCertCN = Common.Formatter.ParseName(WindowsIdentity.GetCurrent().Name);         
            //Console.WriteLine(cltCertCN);
            
            
            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;


            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            if (this.Credentials.ClientCertificate.Certificate == null)
            {
                Console.WriteLine("Neuspesna autentifikacija!!");
                Console.WriteLine("Korisnik nema svoj sertifikat");
                System.Threading.Thread.Sleep(3000);
                Environment.Exit(0);
            }
            factory = this.CreateChannel();
        }
        
        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }

        public string IzvadiImeSertifikata()
        {

            string cltCertCN = Common.Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            cltCertCN.ToLower();   
            X509Certificate2 cer = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);
            string povratni = cer.SubjectName.Name;       
            return povratni;
        }

        public void IzlistajFolder()
        {
            try
            {
                string imeSertifikata = IzvadiImeSertifikata();
                string lista = factory.IzlistajFolder(imeSertifikata);

                if (lista != "Greska")
                {
                    string[] a = lista.Split(',');
                    foreach (string s in a)
                    {
                        Console.WriteLine(s);
                    }
                }
                else
                {
                    Console.WriteLine("Doslo je do greske pri autorizaciji. Korisnik nema pravo da pristupi zeljenim metodama\n");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("[IzlistajFolder] ERROR = {0}", e.Message);
            }
        }

        public void KreirajFajl(string nazivFajla,string text)
        {
            try
            {
                string imeSertifikata = IzvadiImeSertifikata();

                bool pov = factory.KreirajFajl(nazivFajla,text, imeSertifikata);
                if (pov)
                {
                    Console.WriteLine("Fajl uspesno kreiran\n");
                }
                else
                {
                    Console.WriteLine("Doslo je do greske pri autorizaciji. Korisnik nema pravo da pristupi zeljenim metodama\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[KreirajFajl] ERROR = {0}", e.Message);
            }
        }

        public void KreirajFolder(string nazivFoldera)
        {

            string imeSertifikata = IzvadiImeSertifikata();
           
            try
            {
                //Debugger.Launch();
                bool pov = factory.KreirajFolder(nazivFoldera, imeSertifikata);
                if (pov)
                {
                    Console.WriteLine("Folder uspesno kreiran\n");
                }
                else
                {
                    Console.WriteLine("Doslo je do greske pri autorizaciji. Korisnik nema pravo da pristupi zeljenim metodama\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[KreirajFolder] ERROR = {0}", e.Message);
            }
        }

        public void Obrisi(string nazivFajla)
        {
            try
            {
                string imeSertifikata = IzvadiImeSertifikata();


                bool pov = factory.Obrisi(nazivFajla,imeSertifikata);
                if (pov)
                {
                    Console.WriteLine("Fajl uspesno obrisan\n");
                }
                else
                {
                    Console.WriteLine("Doslo je do greske pri autorizaciji. Korisnik nema pravo da pristupi zeljenim metodama\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[Obrisi] ERROR = {0}", e.Message);
            }
        }

        public void Premesti(string imeFajla)
        {
            try
            {
                string imeSertifikata = IzvadiImeSertifikata();

                bool pov = factory.Premesti(imeFajla, imeSertifikata);
                if (pov)
                {
                    Console.WriteLine("Fajl uspesno premesten sa lokacija na lokaciju\n");
                }
                else
                {
                    Console.WriteLine("Doslo je do greske pri autorizaciji. Korisnik nema pravo da pristupi zeljenim metodama\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[Premesti] ERROR = {0}", e.Message);
            }
        }

        public void ProcitajSadrzajFajla(string imeFajla)
        {
            try
            {
                string imeSertifikata = IzvadiImeSertifikata();
                string text = factory.ProcitajSadrzajFajla(imeFajla,imeSertifikata);
                if (text != "Greska")
                {
                    Console.WriteLine("Ovo je sadrzaj fajla: " + text + "\n");
                }
                else
                {
                    Console.WriteLine("Doslo je do greske pri autorizaciji. Korisnik nema pravo da pristupi zeljenim metodama\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[ProcitajSadrzajFajla] ERROR = {0}", e.Message);
            }
        }

        public void PromeniNaziv(string stariNaziv,string noviNaziv)
        {
            try
            {
                string imeSertifikata = IzvadiImeSertifikata();

                bool pov = factory.PromeniNaziv(stariNaziv, noviNaziv, imeSertifikata);
                if (pov)
                {
                    Console.WriteLine("Naziv uspesno promenjen\n");
                }
                else
                {
                    Console.WriteLine("Doslo je do greske pri autorizaciji. Korisnik nema pravo da pristupi zeljenim metodama\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[PromeniNaziv] ERROR = {0}", e.Message);
            }
        }
    }
}
