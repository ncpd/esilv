using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuDeLaVie
{
    class Cellule
    {
        // Attributs
        private int ligne;
        private int colonne;
        private bool etat;
        private int energiepropre;
        private int age;

        #region Constructeurs
        public Cellule(int x, int y, bool c)
        {
            this.ligne = x;
            this.colonne = y;
            this.etat = c;
        } //Niveau 1

        public Cellule(int x, int y, bool e, int age, int energie)
        {
            this.ligne = x;
            this.colonne = y;
            this.etat = e;
            this.age = age;
            this.energiepropre = energie;
        } //Niveau 2
        #endregion 

        #region Propriétés
        public int Energiepropre
        {
            get
            {
                return energiepropre;
            }

            set
            {
                energiepropre = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                age = value;
            }
        }

        public bool Etat
        {
            get
            {
                return etat;
            }

            set
            {
                etat = value;
            }
        }
        
        public int Ligne
        {
            get
            {
                return ligne;
            }

            set
            {
                ligne = value;
            }
        }

        public int Colonne
        {
            get
            {
                return colonne;
            }

            set
            {
                colonne = value;
            }
        }
        #endregion

        #region Méthodes
        public void Clone(Cellule mycell)
        {
            this.ligne = mycell.ligne;
            this.colonne = mycell.colonne;
            this.etat = mycell.etat;
        }

        public string toString()
        {
            string s = "";
            if(this.etat == false)
            {
                s = "morte ";
            }
            else
            {
                s = "vivante ";
            }
            return s;
            //return ("La cellule à la ligne " + this.ligne + " et colonne " + this.colonne + " est " + s);
        }

        public void Affiche(char message)
        {
            if(this.etat == true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(message);
                Console.ResetColor();
            }
            else
            {
                Console.Write(".");
            }
        }
        #endregion
    }
}
