using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    [Serializable]
    public class Etudiant
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string ID { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string JourNaissance { get; set; }
        public string LieuNaissance { get; set; }
        public byte[] Photo { get; set; }

        public Etudiant()
        {

        }

        public Etudiant(string nom, string prenom, string iD, string email, string contact, string jourNaissance, string lieuNaissance, byte[] photo)
        {
            Nom = nom;
            Prenom = prenom;
            ID = iD;
            Email = email;
            Contact = contact;
            JourNaissance = jourNaissance;
            LieuNaissance = lieuNaissance;
            Photo = photo;
        }

        public override bool Equals(object obj)
        {
            return obj is Etudiant etudiant &&
                   ID == etudiant.ID;
        }

        public override int GetHashCode()
        {
            return 145730772 + EqualityComparer<string>.Default.GetHashCode(ID);
        }
    }
}
