using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UA2GP40_MohamedAli
{
    public class Recruteur
    {
        public int IdRecruteur { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Courriel { get; set; }
        public string Telephone { get; set; }
        public List<int> Offres { get; set; } = new List<int>(); // Liste des offres de l'ID du recruteur

        public Recruteur(int idRecruteur, string nom, string adresse, string courriel, string telephone)
        {
            IdRecruteur = idRecruteur;
            Nom = nom;
            Adresse = adresse;
            Courriel = courriel;
            Telephone = telephone;
        }
    }


}
