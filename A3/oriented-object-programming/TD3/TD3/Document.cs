using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD3
{
    class Document
    {
        protected string titre;
        protected int noEnregistrement;

        public Document(string titre, int noEnreg)
        {
            this.titre = titre;
            this.noEnregistrement = noEnreg;
        }

        public override string ToString()
        {
            return "Ce document est nommé " + titre + " et son n° d'enregistrement est " + noEnregistrement + ".";
        }

        public string Titre
        {
            get
            {
                return this.titre;
            }
            set
            {
                this.titre = value;
            }
        }

        public int NoEnregistrement
        {
            get
            {
                return this.noEnregistrement;
            }
            set
            {
                this.noEnregistrement = value;
            }
        }
    }
}
