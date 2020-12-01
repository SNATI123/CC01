using CC01.BO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.DAL
{
    public class EtablissementDAO
    {
        private static List<Etablissement> etablissements;
        private const string FILE_NAME = @"Etablissement.json";
        private readonly string dbFolder;
        private FileInfo file;


        public EtablissementDAO(string dbFolder)
        {
            this.dbFolder = dbFolder;
            file = new FileInfo(Path.Combine(this.dbFolder, FILE_NAME));
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            if (!file.Exists)
            {
                file.Create().Close();
                file.Refresh();
            }
            if (file.Length > 0)
            {
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    string json = sr.ReadToEnd();
                    etablissements = JsonConvert.DeserializeObject<List<Etablissement>>(json);
                }
            }
            if (etablissements == null)
            {
                etablissements = new List<Etablissement>();
            }

        }

        public void Set(Etablissement oldEtablissement, Etablissement newEtablissement)
        {
            var oldIndex = etablissements.IndexOf(oldEtablissement);
            var newIndex = etablissements.IndexOf(newEtablissement);
            if (oldIndex < 0)
                throw new KeyNotFoundException("L'ecole si n'existe pas !");
            if (newIndex >= 0 && oldIndex != newIndex)
                throw new DuplicateNameException("Cette reference existe deja !");
            etablissements[oldIndex] = newEtablissement;
            Save();
        }

        public void Add(Etablissement etablissement)
        {
            var index = etablissements.IndexOf(etablissement);
            if (index >= 0)
                throw new DuplicateNameException("This school reference already exists !");
            etablissements.Add(etablissement);
            Save();
        }

        private void Save()
        {
            using (StreamWriter sw = new StreamWriter(file.FullName, false))
            {
                string json = JsonConvert.SerializeObject(etablissements);
                sw.WriteLine(json);
            }
        }

        public void Remove(Etablissement etablissement)
        {
            etablissements.Remove(etablissement);
            Save();
        }

        public IEnumerable<Etablissement> Find()
        {
            return new List<Etablissement>(etablissements);
        }

        public IEnumerable<Etablissement> Find(Func<Etablissement, bool> predicate)
        {
            return new List<Etablissement>(etablissements.Where(predicate).ToArray());
        }
    }
}
