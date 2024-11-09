using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UA2GP40_MohamedAli
{
    public class Demande : Annonce
    {
        public Demande(int idAnnonce, string intitule, string diplome, int experience,
                       HashSet<string> competences, HashSet<string> langues, DateTime datePublication)
            : base(idAnnonce, "demande", intitule: intitule, diplome: diplome, experience: experience,
                   competences: competences, langues: langues, datePublication: datePublication)
        { }
    }
}
