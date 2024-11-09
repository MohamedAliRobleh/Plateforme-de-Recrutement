using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UA2GP40_MohamedAli
{
    public class Offre : Annonce
    {
        public Offre(int idAnnonce, string idRecruteur, string intitule, string diplome, int experience,
                     HashSet<string> competences, HashSet<string> langues, DateTime datePublication, DateTime delai)
            : base(idAnnonce, "offre", idRecruteur, intitule, diplome, experience, competences, langues, datePublication, delai) { }
    }
}
