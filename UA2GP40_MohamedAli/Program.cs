using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace UA2GP40_MohamedAli
{

    internal class Program
    {
        static void Main(string[] args)
        {
            PlateformeRecrutement plateforme = PlateformeRecrutement.Instance;

            // Ajouter des annonces exemples
            AjouterAnnoncesExemples(plateforme);

            bool continuer = true;

            while (continuer)
            {
                Console.WriteLine("\n--- Portail Recrutement ---");
                Console.WriteLine("1. Ajouter une annonce");
                Console.WriteLine("2. Supprimer une annonce");
                Console.WriteLine("3. Afficher toutes les annonces");
                Console.WriteLine("4. Afficher uniquement les offres");
                Console.WriteLine("5. Afficher uniquement les demandes");
                Console.WriteLine("6. Rechercher une annonce par ID");
                Console.WriteLine("7. Afficher les annonces entre deux dates");
                Console.WriteLine("8. Rechercher des demandeurs par compétences et langues");
                Console.WriteLine("9. Rechercher les offres d'un recruteur par ID");
                Console.WriteLine("10. Recommander des demandes pour une offre donnée");
                Console.WriteLine("11. Quitter");
                Console.Write("Veuillez sélectionner une option (1-11) : ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        ChoisirTypeAnnonce(plateforme);
                        break;
                    case "2":
                        SupprimerAnnonce(plateforme);
                        break;
                    case "3":
                        AfficherToutesLesAnnonces(plateforme);
                        break;
                    case "4":
                        AfficherToutesLesOffres(plateforme);
                        break;
                    case "5":
                        AfficherToutesLesDemandes(plateforme);
                        break;
                    case "6":
                        RechercherAnnonce(plateforme);
                        break;
                    case "7":
                        AfficherAnnoncesEntreDeuxDates(plateforme);
                        break;
                    case "8":
                        RechercherDemandeursCompetencesEtLangues(plateforme);
                        break;
                    case "9":
                        RechercherOffresParRecruteur(plateforme);
                        break;
                    case "10":
                        RecommanderDemandesPourOffre(plateforme);
                        break;
                    case "11":
                        continuer = false;
                        Console.WriteLine("Au revoir!");
                        break;
                    default:
                        Console.WriteLine("Option invalide. Veuillez sélectionner une option valide.");
                        break;
                }
            }
        }

        static void RechercherOffresParRecruteur(PlateformeRecrutement plateforme)
        {
            Console.Write("Entrez l'ID du recruteur : ");
            if (int.TryParse(Console.ReadLine(), out int idRecruteur))
            {
                plateforme.RechercherOffresParRecruteur(idRecruteur);
            }
            else
            {
                Console.WriteLine("ID invalide. Veuillez entrer un nombre entier.");
            }
        }

        static void ChoisirTypeAnnonce(PlateformeRecrutement plateforme)
        {
            Console.WriteLine("\n--- Ajouter une Annonce ---");
            Console.WriteLine("Choisissez le type d'annonce à ajouter :");
            Console.WriteLine("1. Offre");
            Console.WriteLine("2. Demande");
            Console.Write("Veuillez sélectionner une option (1-2) : ");
            string typeChoix = Console.ReadLine();

            if (typeChoix == "1")
            {
                AjouterOffre(plateforme);
            }
            else if (typeChoix == "2")
            {
                AjouterDemande(plateforme);
            }
            else
            {
                Console.WriteLine("Option invalide. Retour au menu principal.");
            }
        }

        static void AjouterOffre(PlateformeRecrutement plateforme)
        {
            Console.WriteLine("\n--- Ajouter une Offre ---");

            int idAnnonce = plateforme.ObtenirNouvelIdAnnonce();

            Console.Write("Nom du recruteur : ");
            string nomRecruteur = Console.ReadLine();
            Console.Write("Adresse du recruteur : ");
            string adresseRecruteur = Console.ReadLine();
            Console.Write("Courriel du recruteur : ");
            string courrielRecruteur = Console.ReadLine();
            Console.Write("Téléphone du recruteur : ");
            string telephoneRecruteur = Console.ReadLine();

            Recruteur recruteur = new Recruteur(
                idRecruteur: 0, // temporaire, l'ID est généré dans la plateforme
                nom: nomRecruteur,
                adresse: adresseRecruteur,
                courriel: courrielRecruteur,
                telephone: telephoneRecruteur
            );
            plateforme.AjouterRecruteur(recruteur);

            Console.Write("Intitulé du poste : ");
            string intitule = Console.ReadLine();
            Console.Write("Diplôme requis : ");
            string diplome = Console.ReadLine();
            Console.Write("Années d'expérience requises : ");
            int experience;
            while (!int.TryParse(Console.ReadLine(), out experience))
            {
                Console.WriteLine("Veuillez entrer un nombre valide pour l'expérience.");
            }

            Console.WriteLine("Compétences requises (séparées par des virgules) : ");
            HashSet<string> competences = new HashSet<string>(Console.ReadLine().Split(',').Select(c => c.Trim()));

            Console.WriteLine("Langues exigées (séparées par des virgules) : ");
            HashSet<string> langues = new HashSet<string>(Console.ReadLine().Split(',').Select(l => l.Trim()));

            Console.Write("Délai d'envoi des candidatures (AAAA-MM-JJ) : ");
            DateTime delai;
            while (!DateTime.TryParse(Console.ReadLine(), out delai))
            {
                Console.WriteLine("Veuillez entrer une date valide au format AAAA-MM-JJ.");
            }

            Offre annonceOffre = new Offre(
                idAnnonce: idAnnonce,
                idRecruteur: recruteur.IdRecruteur.ToString(),
                intitule: intitule,
                diplome: diplome,
                experience: experience,
                competences: competences,
                langues: langues,
                datePublication: DateTime.Now,
                delai: delai
            );

            plateforme.AjouterAnnonce(annonceOffre);
            Console.WriteLine("Annonce d'offre ajoutée avec succès.");
            annonceOffre.AfficherAnnonce();
        }

        static void AjouterDemande(PlateformeRecrutement plateforme)
        {
            Console.WriteLine("\n--- Ajouter une Demande ---");

            int idAnnonce = plateforme.ObtenirNouvelIdAnnonce();

            Console.Write("Nom du demandeur : ");
            string nomDemandeur = Console.ReadLine();
            Console.Write("Diplôme du demandeur : ");
            string diplomeDemande = Console.ReadLine();
            Console.Write("Années d'expérience : ");
            int experienceDemande;
            while (!int.TryParse(Console.ReadLine(), out experienceDemande))
            {
                Console.WriteLine("Veuillez entrer un nombre valide pour l'expérience.");
            }

            Console.WriteLine("Compétences (séparées par des virgules) : ");
            HashSet<string> competencesDemande = new HashSet<string>(Console.ReadLine().Split(',').Select(c => c.Trim()));

            Console.WriteLine("Langues maîtrisées (séparées par des virgules) : ");
            HashSet<string> languesDemande = new HashSet<string>(Console.ReadLine().Split(',').Select(l => l.Trim()));

            Demande annonceDemande = new Demande(
                idAnnonce: idAnnonce,
                intitule: nomDemandeur,
                diplome: diplomeDemande,
                experience: experienceDemande,
                competences: competencesDemande,
                langues: languesDemande,
                datePublication: DateTime.Now
            );

            plateforme.AjouterAnnonce(annonceDemande);
            Console.WriteLine("Annonce de demande ajoutée avec succès.");
            annonceDemande.AfficherAnnonce();
        }

        static void SupprimerAnnonce(PlateformeRecrutement plateforme)
        {
            Console.Write("Entrez l'ID de l'annonce à supprimer : ");
            if (int.TryParse(Console.ReadLine(), out int idAnnonce))
            {
                plateforme.SupprimerAnnonce(idAnnonce);
            }
            else
            {
                Console.WriteLine("ID invalide. Veuillez entrer un nombre entier.");
            }
        }

        static void RechercherAnnonce(PlateformeRecrutement plateforme)
        {
            Console.Write("Entrez l'ID de l'annonce à rechercher : ");
            if (int.TryParse(Console.ReadLine(), out int idAnnonce))
            {
                Annonce annonce = plateforme.ObtenirToutesLesAnnonces().FirstOrDefault(a => a.IdAnnonce == idAnnonce);
                if (annonce != null)
                {
                    annonce.AfficherAnnonce();
                }
                else
                {
                    Console.WriteLine("Aucune annonce trouvée avec cet ID.");
                }
            }
            else
            {
                Console.WriteLine("ID invalide. Veuillez entrer un nombre entier.");
            }
        }

        static void RechercherDemandeursCompetencesEtLangues(PlateformeRecrutement plateforme)
        {
            Console.WriteLine("\n--- Rechercher des Demandeurs par Compétences et Langues ---");

            Console.Write("Entrez les compétences recherchées (séparées par des virgules) : ");
            HashSet<string> competencesRecherchees = new HashSet<string>(Console.ReadLine().Split(',').Select(c => c.Trim()));

            Console.Write("Entrez les langues recherchées (séparées par des virgules) : ");
            HashSet<string> languesRecherchees = new HashSet<string>(Console.ReadLine().Split(',').Select(l => l.Trim()));

            plateforme.RechercherDemandeursCompetencesLangue(competencesRecherchees, languesRecherchees);
        }

        static void AfficherAnnoncesEntreDeuxDates(PlateformeRecrutement plateforme)
        {
            try
            {
                Console.Write("Entrez la date de début (AAAA-MM-JJ) : ");
                DateTime dateDebut = DateTime.Parse(Console.ReadLine());

                Console.Write("Entrez la date de fin (AAAA-MM-JJ) : ");
                DateTime dateFin = DateTime.Parse(Console.ReadLine());

                plateforme.AfficherAnnoncesEntreDates(dateDebut, dateFin);
            }
            catch (FormatException)
            {
                Console.WriteLine("Format de date invalide. Veuillez entrer les dates au format AAAA-MM-JJ.");
            }
        }

        static void AfficherToutesLesAnnonces(PlateformeRecrutement plateforme)
        {
            Console.WriteLine("\n--- Toutes les Annonces ---");
            foreach (var annonce in plateforme.ObtenirToutesLesAnnonces())
            {
                annonce.AfficherAnnonce();
            }
        }

        static void AfficherToutesLesOffres(PlateformeRecrutement plateforme)
        {
            Console.WriteLine("\n--- Toutes les Offres ---");
            foreach (var annonce in plateforme.ObtenirToutesLesAnnonces().Where(a => a.TypeAnnonce == "offre"))
            {
                annonce.AfficherAnnonce();
            }
        }

        static void AfficherToutesLesDemandes(PlateformeRecrutement plateforme)
        {
            Console.WriteLine("\n--- Toutes les Demandes ---");
            foreach (var annonce in plateforme.ObtenirToutesLesAnnonces().Where(a => a.TypeAnnonce == "demande"))
            {
                annonce.AfficherAnnonce();
            }
        }

        static void RecommanderDemandesPourOffre(PlateformeRecrutement plateforme)
        {
            Console.Write("Entrez l'ID de l'offre pour laquelle vous souhaitez obtenir des recommandations : ");
            if (int.TryParse(Console.ReadLine(), out int idOffre))
            {
                plateforme.RecommanderDemandesPourOffre(idOffre);
            }
            else
            {
                Console.WriteLine("ID invalide. Veuillez entrer un nombre entier.");
            }
        }



        static void AjouterAnnoncesExemples(PlateformeRecrutement plateforme)
        {
            // Offre 1
            int id1 = plateforme.ObtenirNouvelIdAnnonce();
            Offre offre1 = new Offre(
                idAnnonce: id1,
                idRecruteur: "1", // Exemple d'ID de recruteur
                intitule: "Développeur de logiciels",
                diplome: "Licence en Informatique",
                experience: 5,
                competences: new HashSet<string> { "Développement de logiciels", "Génie logiciel", "Cybersécurité" },
                langues: new HashSet<string> { "Français", "Anglais" },
                datePublication: DateTime.Parse("2024-11-08"),
                delai: DateTime.Parse("2024-11-20")
            );
            plateforme.AjouterAnnonce(offre1);

            // Offre 2
            int id2 = plateforme.ObtenirNouvelIdAnnonce();
            Offre offre2 = new Offre(
                idAnnonce: id2,
                idRecruteur: "2", // Exemple d'ID de recruteur
                intitule: "Spécialiste en sécurité des réseaux",
                diplome: "Master en Cybersécurité",
                experience: 7,
                competences: new HashSet<string> { "Sécurité des réseaux", "Administration réseau", "Cybersécurité" },
                langues: new HashSet<string> { "Français" },
                datePublication: DateTime.Parse("2024-11-10"),
                delai: DateTime.Parse("2024-11-30")
            );
            plateforme.AjouterAnnonce(offre2);

            // Offre 3
            int id3 = plateforme.ObtenirNouvelIdAnnonce();
            Offre offre3 = new Offre(
                idAnnonce: id3,
                idRecruteur: "3", // Exemple d'ID de recruteur
                intitule: "Analyste de données",
                diplome: "Master en Data Science",
                experience: 3,
                competences: new HashSet<string> { "Data Analysis", "Big Data", "Machine Learning" },
                langues: new HashSet<string> { "Français", "Anglais" },
                datePublication: DateTime.Parse("2024-11-12"),
                delai: DateTime.Parse("2024-11-25")
            );
            plateforme.AjouterAnnonce(offre3);

            // Offre 4
            int id4 = plateforme.ObtenirNouvelIdAnnonce();
            Offre offre4 = new Offre(
                idAnnonce: id4,
                idRecruteur: "4", // Exemple d'ID de recruteur
                intitule: "Administrateur de bases de données",
                diplome: "Licence en Système d'information",
                experience: 4,
                competences: new HashSet<string> { "Administration de bases de données", "Data Analysis", "Big Data" },
                langues: new HashSet<string> { "Français" },
                datePublication: DateTime.Parse("2024-11-20"),
                delai: DateTime.Parse("2024-12-10")
            );
            plateforme.AjouterAnnonce(offre4);

            // Demande 1
            int id5 = plateforme.ObtenirNouvelIdAnnonce();
            Demande demande1 = new Demande(
                idAnnonce: id5,
                intitule: "Ingénieur DevOps junior",
                diplome: "Licence en Informatique",
                experience: 1,
                competences: new HashSet<string> { "Cloud Computing", "Administration de bases de données", "Sécurité des réseaux" },
                langues: new HashSet<string> { "Français" },
                datePublication: DateTime.Parse("2024-11-15")
            );
            plateforme.AjouterAnnonce(demande1);

            // Demande 2
            int id6 = plateforme.ObtenirNouvelIdAnnonce();
            Demande demande2 = new Demande(
                idAnnonce: id6,
                intitule: "Développeur Web",
                diplome: "Licence en Informatique",
                experience: 2,
                competences: new HashSet<string> { "Développement Web", "Médias sociaux", "Génie logiciel" },
                langues: new HashSet<string> { "Français", "Anglais" },
                datePublication: DateTime.Parse("2024-11-18")
            );
            plateforme.AjouterAnnonce(demande2);

            // Demande 3
            int id7 = plateforme.ObtenirNouvelIdAnnonce();
            Demande demande3 = new Demande(
                idAnnonce: id7,
                intitule: "Développeur Python",
                diplome: "Licence en Informatique",
                experience: 3,
                competences: new HashSet<string> { "Développement de logiciels", "Python", "Machine Learning" },
                langues: new HashSet<string> { "Français", "Anglais" },
                datePublication: DateTime.Parse("2024-11-11")
            );
            plateforme.AjouterAnnonce(demande3);

            // Demande 4
            int id8 = plateforme.ObtenirNouvelIdAnnonce();
            Demande demande4 = new Demande(
                idAnnonce: id8,
                intitule: "Analyste Big Data",
                diplome: "Master en Data Science",
                experience: 4,
                competences: new HashSet<string> { "Big Data", "Data Analysis", "Machine Learning" },
                langues: new HashSet<string> { "Anglais" },
                datePublication: DateTime.Parse("2024-11-17")
            );
            plateforme.AjouterAnnonce(demande4);

            int id9 = plateforme.ObtenirNouvelIdAnnonce();
            Demande demande5 = new Demande(
                idAnnonce: id9,
                intitule: "Expert en Cybersécurité",
                diplome: "Master en Cybersécurité",
                experience: 8, // Supérieur ou égal à l'expérience requise de l'offre
                competences: new HashSet<string> { "Sécurité des réseaux", "Administration réseau", "Cybersécurité" },
                langues: new HashSet<string> { "Français" },
                datePublication: DateTime.Parse("2024-11-22")
            );
            plateforme.AjouterAnnonce(demande5);

            int id10 = plateforme.ObtenirNouvelIdAnnonce();
            Demande demande6 = new Demande(
                idAnnonce: id10,
                intitule: "Développeur de logiciels confirmé",
                diplome: "Licence en Informatique",
                experience: 5,
                competences: new HashSet<string> { "Développement de logiciels", "Génie logiciel", "Cybersécurité" },
                langues: new HashSet<string> { "Français", "Anglais" },
                datePublication: DateTime.Parse("2024-11-09")
            );
            plateforme.AjouterAnnonce(demande6);

            int id11 = plateforme.ObtenirNouvelIdAnnonce();
            Demande demande7 = new Demande(
                idAnnonce: id11,
                intitule: "Analyste de données junior",
                diplome: "Master en Data Science",
                experience: 3,
                competences: new HashSet<string> { "Data Analysis", "Big Data", "Machine Learning" },
                langues: new HashSet<string> { "Français", "Anglais" },
                datePublication: DateTime.Parse("2024-11-13")
            );
            plateforme.AjouterAnnonce(demande7);

            int id12 = plateforme.ObtenirNouvelIdAnnonce();
            Demande demande8 = new Demande(
                idAnnonce: id12,
                intitule: "Administrateur de bases de données",
                diplome: "Licence en Système d'information",
                experience: 4,
                competences: new HashSet<string> { "Administration de bases de données", "Data Analysis", "Big Data" },
                langues: new HashSet<string> { "Français" },
                datePublication: DateTime.Parse("2024-11-21")
            );
            plateforme.AjouterAnnonce(demande8);

            Console.WriteLine("Annonces exemples ajoutées au programme.");
        }
    } 
}
