using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD7
{
    [Serializable]
    public class Artiste
    {
        private string nom;
        private string prenom;
        private string surnom;

        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Surnom { get => surnom; set => surnom = value; }

        public Artiste(string nom, string prenom, string surnom)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.surnom = surnom;
        }

        // constructeur où le surnom n'est pas renseigné (il est donc initialisé à null)
        public Artiste(string nom, string prenom)
            : this(nom, prenom, null)
        {
        }

        public Artiste()
        {

        }

        override public string ToString()
        {
            string resultat = nom + " " + prenom;

            if (surnom != null)
            {
                resultat = surnom + " (" + resultat + ")";
            }

            return resultat;
        }
    }
}
