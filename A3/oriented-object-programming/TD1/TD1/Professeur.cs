using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD1
{
    class Professeur : Humain
    {
        #region Attributs

        private Cours[] listeCours;

        #endregion

        #region Constructeur
        public Professeur(string nom, string prenom, int id, Cours[] listeCours) : base(nom, prenom, id)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Id = id;
            this.listeCours = listeCours;
        }

        #endregion

        #region Accesseurs

        public Cours[] ListeCours
        {
            get
            {
                return listeCours;
            }
            set
            {
                this.listeCours = value;
            }
        }

        #endregion
    }
}
