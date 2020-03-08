using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD1
{
    class Etudiant : Humain
    {
        #region Attributs

        private Cours[] listeCours;
        private Cours[] listeCoursValides;

        #endregion

        #region Constructeur

        public Etudiant(string nom, string prenom, int id, Cours[] listeCours, Cours[] listeCoursValides) : base(nom, prenom, id)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Id = id;
            this.listeCours = listeCours;
            this.listeCoursValides = listeCoursValides;
        }

        #endregion

        #region Accesseurs

        public Cours[] ListeCours
        {
            get
            {
                return this.listeCours;
            }
            set
            {
                this.listeCours = value;
            }
        }

        public Cours[] ListeCoursValides
        {
            get
            {
                return this.listeCoursValides;
            }
            set
            {
                this.listeCoursValides = value;
            }
        }

        #endregion

        #region Methodes

        public int estInscrit(string cours)
        {
            for(int i = 0; i < listeCours.Length; i++)
            {
                if(String.Equals(listeCours[i].Matiere, cours))
                {
                    return i;
                }
            }
            return -1;
        }

        #endregion
    }
}
