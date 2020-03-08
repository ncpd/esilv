using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD3
{
    class Dictionnaire : Document
    {
        private string langue;

        public Dictionnaire(string titre, int noEnreg, string langue) : base(titre, noEnreg)
        {
            this.titre = titre;
            this.noEnregistrement = noEnreg;
            this.langue = langue;
        }

        public override string ToString()
        {
            return "Ce dictionnaire est nommé " + titre + " et son n° d'enregistrement est " + noEnregistrement + ". C'est un dictionnaire " + langue + ".";
        }
    }
}
