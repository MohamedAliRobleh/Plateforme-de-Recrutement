Portail Recrutement - Gestion d'Offres et de Demandes d'Emploi

Ce projet est une application console en C# dédiée à la gestion des annonces d'emploi pour un portail de recrutement. Le programme permet de gérer les offres d'emploi des recruteurs ainsi que les demandes des candidats, tout en proposant des fonctionnalités avancées de recherche et de recommandation.

Fonctionnalités principales

Ajout d'Annonces : Possibilité d'ajouter des annonces de type "offre" ou "demande" avec des informations détaillées sur les compétences, langues, expériences, et diplômes requis.

Recherche Avancée :
Par ID : Rechercher une annonce spécifique par son identifiant unique.

Par Période : Afficher les annonces publiées entre deux dates données.

Par Compétences et Langues : Trouver des candidats correspondant à certaines compétences et langues.

Par Recruteur : Lister toutes les offres publiées par un recruteur donné.

Recommandations de Candidats : Pour chaque offre, le système peut recommander les demandes de candidats qui répondent aux critères spécifiques de l’offre.

Gestion d'Archivage : Possibilité de supprimer une annonce, avec un archivage automatique.

Structure du Projet

Program.cs : Point d'entrée principal avec le menu de navigation, et diverses méthodes pour chaque action (ajout, suppression, recherche, etc.).

PlateformeRecrutement.cs : Gère la logique principale de l’application, incluant le stockage, la recherche et l’archivage des annonces.

Classes Annonce, Offre et Demande : Définissent les attributs et comportements de chaque type d'annonce.

Exemple d'Utilisation

Le programme initialise des exemples d'annonces pour démontrer ses fonctionnalités. Il offre un menu interactif permettant aux utilisateurs de naviguer facilement parmi les fonctionnalités proposées.

Installation et Exécution

Clonez le dépôt GitHub.

Compilez et exécutez le projet dans un environnement C# compatible (Visual Studio ou .NET Core CLI).
