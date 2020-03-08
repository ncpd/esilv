using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD5
{
    class Manuel : Livre
    {
        private int niveauScolaire;
        public Manuel(string auteur, int nbPages, string titre, int noEnreg, int nvScol) : base(auteur, nbPages, titre, noEnreg)
        {
            this.auteur = auteur;
            this.nbPages = nbPages;
            this.titre = titre;
            this.noEnregistrement = noEnreg;
            this.niveauScolaire = nvScol;
            this.isImprimable = false;
        }

        public override string ToString()
        {
            return base.ToString() + " Ce manuel est de niveau " + niveauScolaire + ".";
        }
    }
}
