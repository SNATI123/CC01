using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    public class Etablissement
    {
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }
        public byte[] Logo { get; set; }


        public Etablissement()
        {

        }

        public Etablissement(string nom,string email,string adresse, byte[] logo)
        {
            Nom = nom;
            Logo = logo;
            Email = email;
            Adresse = adresse;


        }

        public override bool Equals(object obj)
        {
            return obj is Etablissement etablissement &&
                   Nom == etablissement.Nom;
        }

        public override int GetHashCode()
        {
            return 217408413 + EqualityComparer<string>.Default.GetHashCode(Nom);
        }
    }
}
