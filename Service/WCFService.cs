using Common;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel;
using Manager;

namespace Service
{
    public class WCFService : IWCFService
    {

        public string IzlistajFolder(string imeSertifikata)
        {
            string username = Common.Formatter.IzvadiUsername(imeSertifikata);
            string grupe = Common.Formatter.IzvadiGrupe(imeSertifikata);

            if (!grupe.Contains("Viewer"))
            {
                Console.WriteLine("Korisnik " + username + " nema pravo pristupa ovoj metodi. Nije clan Viewer grupe!");
                return "Greska";

            }

            string lista = string.Empty;

            DirectoryInfo dinfo = new DirectoryInfo(@"C:\Users\Marko\source\repos\Blok2\Service");
            FileInfo[] Files = dinfo.GetFiles("*.txt");

            foreach (FileInfo file in Files)
            {
                string ime = file.Name + ",";
                lista += ime; 
            }

            Console.WriteLine("Korisnik " + username + " uspesno izlistao folder");
            return lista;
        }

        public bool KreirajFajl(string nazivFajla,string text, string imeSertifikata)
        {
            string username = Common.Formatter.IzvadiUsername(imeSertifikata);
            string grupe = Common.Formatter.IzvadiGrupe(imeSertifikata);


            if (!grupe.Contains("Viewer"))
            {
                Console.WriteLine("Grupa je: " + grupe);
                Console.WriteLine("Korisnik " + username + " nema pravo pristupa ovoj metodi. Nije clan Viewer grupe!");
                return false;
            }

            if (!grupe.Contains("Editor"))
            {
                Console.WriteLine("Grupa je: " + grupe);
                Console.WriteLine("Korisnik " + username + " nema pravo pristupa ovoj metodi. Nije clan Editor grupe!");
                return false;

            }

            string path = @"C:\Users\Marko\source\repos\Blok2\Service\" + nazivFajla;

            

            System.IO.File.WriteAllText(path, text);
            Console.WriteLine("Korisnik " + username + " uspesno kreirao fajl pod nazivom " + nazivFajla);
            return true;
        }

        public bool KreirajFolder(string nazivFoldera, string imeSertifikata)
        {

            string username = Common.Formatter.IzvadiUsername(imeSertifikata);
            string grupe = Common.Formatter.IzvadiGrupe(imeSertifikata);


            if (!grupe.Contains("Viewer"))
            {
                Console.WriteLine("Korisnik " + username + " nema pravo pristupa ovoj metodi. Nije clan Viewer grupe!");
                return false;

            }

            if (!grupe.Contains("Editor"))
            {
                Console.WriteLine("Korisnik " + username + " nema pravo pristupa ovoj metodi. Nije clan Editor grupe!");
                return false;

            }

            string pathString = @"C:\Users\Marko\source\repos\Blok2\Service\" + nazivFoldera;

            System.IO.Directory.CreateDirectory(pathString);
            Console.WriteLine("Korisnik " + username + " uspesno kreirao folder pod nazivom " + nazivFoldera);
            return true;

        }

        public bool Obrisi(string nazivFajla, string imeSertifikata)
        {
            string username = Common.Formatter.IzvadiUsername(imeSertifikata);
            string grupe = Common.Formatter.IzvadiGrupe(imeSertifikata);


            if (!grupe.Contains("Viewer"))
            {
                Console.WriteLine("Korisnik " + username + " nema pravo pristupa ovoj metodi. Nije clan Viewer grupe!");
                return false;

            }

            if (!grupe.Contains("Editor"))
            {
                Console.WriteLine("Korisnik " + username + " nema pravo pristupa ovoj metodi. Nije clan Editor grupe!");
                return false;

            }

            string pathString = @"C:\Users\Marko\source\repos\Blok2\Service\bin\Debug";

            if (File.Exists(Path.Combine(pathString, nazivFajla)))
            {               
                File.Delete(Path.Combine(pathString, nazivFajla));
                Console.WriteLine("Korisnik " + username + " je uspesno obrisao fajl po nazivom " + nazivFajla);
                return true;
            }
            return false;
        }

        public bool Premesti(string imeFajla,string imeSertifikata)
        {
            string username = Common.Formatter.IzvadiUsername(imeSertifikata);
            string grupe = Common.Formatter.IzvadiGrupe(imeSertifikata);


            if (!grupe.Contains("Viewer"))
            {
                Console.WriteLine("Korisnik " + username + " nema pravo pristupa ovoj metodi. Nije clan Viewer grupe!");
                return false;

            }

            if (!grupe.Contains("Editor"))
            {
                Console.WriteLine("Korisnik " + username + " nema pravo pristupa ovoj metodi. Nije clan Editor grupe!");
                return false;

            }


            string sourcePath = @"C:\Users\Marko\source\repos\Blok2\Service";
            string targetPath = @"C:\Users\Marko\source\repos\Blok2\Service\bin\Debug";

            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, imeFajla);
            string destFile = System.IO.Path.Combine(targetPath, imeFajla);

            System.IO.File.Copy(sourceFile, destFile, true);
            File.Delete(Path.Combine(sourcePath, imeFajla));
            Console.WriteLine("Korisnik " + username + " uspesno premestio fajl pod nazivom " + imeFajla);
            return true;
        }

        public string ProcitajSadrzajFajla(string imeFajla, string imeSertifikata)
        {
            string username = Common.Formatter.IzvadiUsername(imeSertifikata);
            string grupe = Common.Formatter.IzvadiGrupe(imeSertifikata);


            if (!grupe.Contains("Viewer"))
            {
                Console.WriteLine("Korisnik " + username + " nema pravo pristupa ovoj metodi. Nije clan Viewer grupe!");
                return "Greska";
            }

            string text = System.IO.File.ReadAllText(@"C:\Users\Marko\source\repos\Blok2\Service\" + imeFajla);
            Console.WriteLine("Korisnik " + username + " je uspesno procitao sadrzaj fajl pod nazivom " + imeFajla);
            return text;
        }

        public bool PromeniNaziv(string stariNaziv,string noviNaziv, string imeSertifikata)
        {
            string username = Common.Formatter.IzvadiUsername(imeSertifikata);
            string grupe = Common.Formatter.IzvadiGrupe(imeSertifikata);


            if (!grupe.Contains("Viewer"))
            {
                Console.WriteLine("Korisnik " + username + " nema pravo pristupa ovoj metodi. Nije clan Viewer grupe!");
                return false;

            }

            if (!grupe.Contains("Editor"))
            {
                Console.WriteLine("Korisnik " + username + " nema pravo pristupa ovoj metodi. Nije clan Editor grupe!");
                return false;

            }

            System.IO.File.Move(stariNaziv, noviNaziv);
            Console.WriteLine("Korisnik " + username + " uspesno promenio ime fajla " + stariNaziv + " u " + noviNaziv);
            return true;
        }
    }
}
