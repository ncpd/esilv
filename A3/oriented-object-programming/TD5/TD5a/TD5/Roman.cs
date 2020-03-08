using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD5
{
    class Roman : Livre
    {
        protected string prixLitteraire;

        public Roman(string auteur, int nbPages, string titre, int noEnreg, string prixLitt) : base(auteur, nbPages, titre, noEnreg)
        {
            this.auteur = auteur;
            this.nbPages = nbPages;
            this.titre = titre;
            this.noEnregistrement = noEnreg;
            this.prixLitteraire = prixLitt;
            this.isImprimable = false;
        }

        public override string ToString()
        {
            return base.ToString() + " Ce roman a reçu le prix " + prixLitteraire + ".";
        }
    }
}
