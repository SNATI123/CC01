using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.WinForms
{
    public class EtudiantListPrint
    {
        private long contact;

        public string JourNaissance { get; set; }
        public string ID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }

        public EtudiantListPrint(string jourNaissance, string iD, string nom, string prenom, string contact, string email, byte[] photo)
        {
            JourNaissance = jourNaissance;
            ID = iD;
            Nom = nom;
            Prenom = prenom;
            Contact = contact;
            Email = email;
            Photo = photo;
        }

        public EtudiantListPrint(string jourNaissance, string iD, string nom, string prenom, byte[] photo, long contact, string email)
        {
            JourNaissance = jourNaissance;
            ID = iD;
            Nom = nom;
            Prenom = prenom;
            Photo = photo;
            this.contact = contact;
            Email = email;
        }
    }


}
