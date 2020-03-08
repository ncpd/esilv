using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD1
{
    class Humain
    {
        #region Attributs

        private string nom;
        private string prenom;
        private int id;

        #endregion

        #region Constructeur

        public Humain(string nom, string prenom, int id)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.id = id;
        }

        #endregion

        #region Accesseurs

        public string Nom
        {
            get
            {
                return this.nom;
            }
            set
            {
                this.nom = value;
            }
        }

        public string Prenom
        {
            get
            {
                return this.prenom;
            }
            set
            {
                this.prenom = value;
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        #endregion
    }
}
