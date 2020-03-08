using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD1
{
    class Universite
    {
        private string nom;
        Etudiant[] listeEtudiants;
        Professeur[] listeProfs;
        private string[] listeSalles;

        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                nom = value;
            }
        }
        public Etudiant[] ListeEtudiants
        {
            get
            {
                return listeEtudiants;
            }

            set
            {
                listeEtudiants = value;
            }
        }

        public Professeur[] ListeProfs
        {
            get
            {
                return listeProfs;
            }

            set
            {
                listeProfs = value;
            }
        }
        public string[] ListeSalles
        {
            get
            {
                return listeSalles;
            }

            set
            {
                listeSalles = value;
            }
        }

        public Universite(string nom, Professeur[] liste_prof, string[] liste_salle, Etudiant[] liste_etudiant)
        {
            this.nom = nom;
            this.listeProfs = liste_prof;
            this.listeEtudiants = liste_etudiant;
            this.listeSalles = liste_salle;
        }

        public Universite()
        {
            this.nom = "";
            this.listeEtudiants = null;
            this.listeProfs = null;
            this.listeSalles = null;
        }

        public int studentIsInUni(int id)
        {
            for(int i = 0; i < listeEtudiants.Length; i++)
            {
                if(listeEtudiants[i].Id == id)
                {
                    return i;
                }
            }
            return - 1;
        }

        public int profIsInUni(int id)
        {
            for (int i = 0; i < listeProfs.Length; i++)
            {
                if (listeProfs[i].Id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
