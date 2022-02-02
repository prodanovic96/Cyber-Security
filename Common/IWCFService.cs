using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IWCFService
    {
        [OperationContract]
        string IzlistajFolder(string imeSertifikata);

        [OperationContract]
        string ProcitajSadrzajFajla(string imeFajla,string imeSertifikata);

        [OperationContract]
        bool KreirajFolder(string nazivFoldera, string imeSertifikata);

        [OperationContract]
        bool KreirajFajl(string nazivFajla,string text, string imeSertifikata);

        [OperationContract]
        bool PromeniNaziv(string stariNaziv, string noviNaziv, string imeSertifikata);

        [OperationContract]
        bool Premesti(string imeFajla,string imeSertifikata);

        [OperationContract]
        bool Obrisi(string nazivFajla,string imeSertifikata);

    }

}
