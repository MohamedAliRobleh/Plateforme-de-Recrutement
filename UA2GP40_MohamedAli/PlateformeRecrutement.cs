using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;

namespace UA2GP40_MohamedAli
{
    public class PlateformeRecrutement
    {
        private static PlateformeRecrutement instance;
        private Dictionary<string, int> idParEmail = new Dictionary<string, int>(); // Map de l'email au IdRecruteur
        private Dictionary<int, Recruteur> recruteursParId = new Dictionary<int, Recruteur>(); // Map IdRecruteur au recruteur
        private Dictionary<int, Annonce> annonces = new Dictionary<int, Annonce>();
        private Dictionary<int, Annonce> archive = new Dictionary<int, Annonce>();

        private static int compteurIdAnnonce = 1;
        private int compteurIdRecruteur = 1;

        private PlateformeRecrutement() { }

        public static PlateformeRecrutement Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlateformeRecrutement();
                }
                return instance;
            }
        }

        private int GenererIdRecruteur()
        {
            return compteurIdRecruteur++;
        }

        private int GenererIdAnnonce()
        {
            return compteurIdAnnonce++;
        }

        public int ObtenirNouvelIdAnnonce()
        {
            return GenererIdAnnonce();
        }

        public void AjouterRecruteur(Recruteur recruteur)
        {
            if (!idParEmail.ContainsKey(recruteur.Courriel))
            {
                recruteur.IdRecruteur = GenererIdRecruteur();
                recruteursParId[recruteur.IdRecruteur] = recruteur;
                idParEmail[recruteur.Courriel] = recruteur.IdRecruteur;
            }
            else
            {
                Console.WriteLine("Le recruteur avec ce courriel existe déjà.");
            }
        }

        public void AjouterAnnonce(Annonce annonce)
        {
            annonces[annonce.IdAnnonce] = annonce;

            // Si l'annonce est une offre et que le recruteur existe, ajouter l'offre au recruteur
            if (annonce is Offre offre && recruteursParId.TryGetValue(int.Parse(offre.IdRecruteur), out Recruteur recruteur))
            {
                recruteur.Offres.Add(annonce.IdAnnonce);
            }
        }

        public void SupprimerAnnonce(int idAnnonce)
        {
            if (annonces.TryGetValue(idAnnonce, out Annonce annonce))
            {
                archive[idAnnonce] = annonce;
                annonces.Remove(idAnnonce);
                Console.WriteLine($"Annonce avec ID {idAnnonce} a été déplacée dans l'archive.");

                if (annonce is Offre offre && recruteursParId.TryGetValue(int.Parse(offre.IdRecruteur), out Recruteur recruteur))
                {
                    recruteur.Offres.Remove(idAnnonce);

                    // Supprimer le recruteur si plus aucune offre
                    if (recruteur.Offres.Count == 0)
                    {
                        recruteursParId.Remove(int.Parse(offre.IdRecruteur));
                        idParEmail.Remove(recruteur.Courriel);
                        Console.WriteLine($"Recruteur avec courriel {recruteur.Courriel} a été supprimé car il n'a plus d'offres.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Aucune annonce trouvée avec l'ID {idAnnonce}.");
            }
        }

        public void RechercherOffresParRecruteur(int idRecruteur)
        {
            Console.WriteLine($"\n--- Offres publiées par le recruteur avec l'ID {idRecruteur} ---");
            bool offresTrouvees = false;

            foreach (var annonce in annonces.Values)
            {
                if (annonce is Offre offre && int.Parse(offre.IdRecruteur) == idRecruteur)
                {
                    offre.AfficherAnnonce();
                    offresTrouvees = true;
                }
            }

            if (!offresTrouvees)
            {
                Console.WriteLine($"Aucune offre trouvée pour le recruteur avec l'ID {idRecruteur}.");
            }
        }

        public int? ObtenirIdRecruteurParEmail(string email)
        {
            return idParEmail.TryGetValue(email, out int idRecruteur) ? idRecruteur : (int?)null;
        }

        public void RechercherDemandeursCompetencesLangue(HashSet<string> competencesRecherchees, HashSet<string> languesRecherchees)
        {
            Console.WriteLine("\n--- Demandeurs correspondant aux compétences et langues recherchées ---");
            bool demandeursTrouves = false;

            HashSet<string> competencesNormalisees = new HashSet<string>(competencesRecherchees.Select(c => c.Trim().ToLower()));
            HashSet<string> languesNormalisees = new HashSet<string>(languesRecherchees.Select(l => l.Trim().ToLower()));

            foreach (var annonce in annonces.Values)
            {
                if (annonce is Demande demande)
                {
                    HashSet<string> competencesDemande = new HashSet<string>(demande.Competences.Select(c => c.Trim().ToLower()));
                    HashSet<string> languesDemande = new HashSet<string>(demande.Langues.Select(l => l.Trim().ToLower()));

                    bool competencesCorrespondent = competencesNormalisees.IsSubsetOf(competencesDemande);
                    bool languesCorrespondent = languesNormalisees.IsSubsetOf(languesDemande);

                    if (competencesCorrespondent && languesCorrespondent)
                    {
                        demande.AfficherAnnonce();
                        demandeursTrouves = true;
                    }
                }
            }

            if (!demandeursTrouves)
            {
                Console.WriteLine("Aucun demandeur trouvé avec les compétences et langues spécifiées.");
            }
        }

        public void AfficherAnnoncesEntreDates(DateTime dateDebut, DateTime dateFin)
        {
            Console.WriteLine($"\n--- Annonces publiées entre {dateDebut:yyyy-MM-dd} et {dateFin:yyyy-MM-dd} ---");
            bool annoncesTrouvees = false;

            foreach (var annonce in annonces.Values.Concat(archive.Values))
            {
                if (annonce.DatePublication >= dateDebut && annonce.DatePublication <= dateFin)
                {
                    annonce.AfficherAnnonce();
                    annoncesTrouvees = true;
                }
            }

            if (!annoncesTrouvees)
            {
                Console.WriteLine("Aucune annonce trouvée dans cette période.");
            }
        }

        public void RecommanderDemandesPourOffre(int idOffre)
        {
            // Vérifie si l'offre existe et est du type Offre
            if (!annonces.TryGetValue(idOffre, out Annonce annonce) || !(annonce is Offre offre))
            {
                Console.WriteLine($"Aucune offre trouvée avec l'ID {idOffre}.");
                return;
            }

            Console.WriteLine($"\n--- Recommandations pour l'offre : {offre.Intitule} ---");
            bool recommandationsTrouvees = false;

            // Parcourt les demandes pour rechercher des correspondances
            foreach (var demandeAnnonce in annonces.Values)
            {
                if (demandeAnnonce is Demande demande)
                {
                    // Vérifie les critères de correspondance pour l'offre et la demande
                    bool experienceSuffisante = demande.Experience >= offre.Experience;
                    bool competencesCorrespondent = offre.Competences.IsSubsetOf(demande.Competences);
                    bool languesCorrespondent = offre.Langues.IsSubsetOf(demande.Langues);

                    // Affiche la demande si elle répond aux critères
                    if (experienceSuffisante && competencesCorrespondent && languesCorrespondent)
                    {
                        demande.AfficherAnnonce();
                        recommandationsTrouvees = true;
                    }
                }
            }

            if (!recommandationsTrouvees)
            {
                Console.WriteLine("Aucune demande ne correspond aux critères de cette offre.");
            }
        }




        public List<Annonce> ObtenirToutesLesAnnonces()
        {
            return annonces.Values.ToList();
        }
    }
}