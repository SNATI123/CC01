using CC01.BO;
using CC01.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BLL
{
    public class EtablissementBLO
    {
        EtablissementDAO etablissementRepo;
        public EtablissementBLO(string dbFolder)
        {
            etablissementRepo = new EtablissementDAO(dbFolder);
        }
        public void CreateEcole(Etablissement etablissement)
        {
            etablissementRepo.Add(etablissement);
        }

        public void DeleteProduct(Etablissement etablissement)
        {
            etablissementRepo.Remove(etablissement);
        }

        public IEnumerable<Etablissement> GetAllProducts()
        {
            return etablissementRepo.Find();
        }


        public IEnumerable<Etablissement> GetByPostalCode(string adresse)
        {
            return etablissementRepo.Find(x => x.Adresse == adresse);
        }

        public IEnumerable<Etablissement> GetBy(Func<Etablissement, bool> predicate)
        {
            return etablissementRepo.Find(predicate);
        }

        public void EditEtablissement(Etablissement oldEtablissement, Etablissement newEtablissement)
        {
            etablissementRepo.Set(oldEtablissement, newEtablissement);
        }

    }
}
