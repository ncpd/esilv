using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD5
{
    class Livre : Document
    {
        protected string auteur;
        protected int nbPages;

        public Livre(string auteur, int nbPages, string titre, int noEnreg) : base(titre, noEnreg)
        {
            this.auteur = auteur;
            this.titre = titre;
            this.nbPages = nbPages;
            this.noEnregistrement = noEnreg;
            isImprimable = true;
        }

        public override string ToString()
        {
            return titre + " a été écrit par " + auteur + ". Il comporte " + nbPages
                + " pages et son n° d'enregistrement est " + noEnregistrement + ".";
        }
    }
}
