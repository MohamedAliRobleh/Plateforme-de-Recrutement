using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UA2GP40_MohamedAli
{
    
    public class Annonce
    {
        public int IdAnnonce { get; private set; }
        public string TypeAnnonce { get; set; }
        public string IdRecruteur { get; set; }
        public string Intitule { get; set; }
        public string Diplome { get; set; }
        public int? Experience { get; set; }
        public HashSet<string> Competences { get; set; } = new HashSet<string>();
        public HashSet<string> Langues { get; set; } = new HashSet<string>();
        public DateTime DatePublication { get; set; }
        public DateTime? Delai { get; set; }

        public Annonce(int idAnnonce, string typeAnnonce, string idRecruteur = null, string intitule = null,
                       string diplome = null, int? experience = null, HashSet<string> competences = null,
                       HashSet<string> langues = null, DateTime? datePublication = null, DateTime? delai = null)
        {
            IdAnnonce = idAnnonce;
            TypeAnnonce = typeAnnonce;
            IdRecruteur = idRecruteur;
            Intitule = intitule;
            Diplome = diplome;
            Experience = experience;
            Competences = competences ?? new HashSet<string>();
            Langues = langues ?? new HashSet<string>();
            DatePublication = datePublication ?? DateTime.Now;
            Delai = delai;
        }

        public virtual void AfficherAnnonce()
        {
            Console.WriteLine("\n--- Détails de l'annonce ---");
            Console.WriteLine($"ID de l'annonce : {IdAnnonce}");
            Console.WriteLine($"Type d'annonce : {TypeAnnonce}");
            Console.WriteLine($"ID du recruteur : {IdRecruteur}");
            Console.WriteLine($"Intitulé du poste : {Intitule}");
            Console.WriteLine($"Diplôme requis : {Diplome}");
            Console.WriteLine($"Années d'expérience : {Experience}");
            Console.WriteLine("Compétences requises : " + string.Join(", ", Competences.ToArray()));
            Console.WriteLine("Langues exigées : " + string.Join(", ", Langues.ToArray()));
            Console.WriteLine($"Date de publication : {DatePublication:yyyy-MM-dd}");
            Console.WriteLine($"Délai d'envoi des candidatures : {Delai?.ToString("yyyy-MM-dd")}");
            Console.WriteLine("---------------------------\n");
        }
    }
}
