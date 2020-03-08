using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD3
{
    class Revue : Document
    {
        private string mois;
        private int annee;

        public Revue(string titre, int noEnreg, string mois, int annee) : base(titre, noEnreg)
        {
            this.titre = titre;
            this.noEnregistrement = noEnreg;
            this.mois = mois;
            this.annee = annee;
        }

        public string Mois
        {
            get
            {
                return this.mois;
            }
            set
            {
                this.mois = value;
            }
        }

        public int Annee
        {
            get
            {
                return this.annee;
            }
            set
            {
                this.annee = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() + " Cette revue est sortie en " + this.mois + " " + this.annee + ".";
        }
    }
}
