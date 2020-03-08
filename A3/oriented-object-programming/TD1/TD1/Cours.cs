using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD1
{
    class Cours
    {
        private string matiere;
        private Professeur prof;
        private int[] liste_etudiant; // ids des étudiants
        private int[] liste_note;
        private string horaire;
        private string date;
        private string salle;
        private double moyenne;
        private Cours[] prerequis;

        public Cours(string nom, Professeur professeur, int[] listeEtudiants, int[] listeNotes, string horaire, string date, string salle, Cours[] prerequis)
        {
            this.matiere = nom;
            this.prof = professeur;
            this.liste_etudiant = listeEtudiants;
            this.liste_note = listeNotes;
            this.horaire = horaire;
            this.date = date;
            this.salle = salle;
            this.prerequis = prerequis;
        }

        #region Accesseurs
        public string Matiere
        {
            get
            {
                return matiere;
            }

            set
            {
                matiere = value;
            }
        }
        public Professeur Prof
        {
            get
            {
                return prof;
            }

            set
            {
                prof = value;
            }
        }
        public int[] Liste_etudiant
        {
            get
            {
                return liste_etudiant;
            }

            set
            {
                liste_etudiant = value;
            }
        }
        public int[] Liste_note
        {
            get
            {
                return liste_note;
            }

            set
            {
                liste_note = value;
            }
        }
        public string Horaire
        {
            get
            {
                return horaire;
            }

            set
            {
                horaire = value;
            }
        }
        public string Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }
        public string Salle
        {
            get
            {
                return salle;
            }

            set
            {
                salle = value;
            }
        }
        public double Moyenne
        {
            get
            {
                return moyenne;
            }

            set
            {
                moyenne = value;
            }
        }
        internal Cours[] Prerequis
        {
            get
            {
                return prerequis;
            }

            set
            {
                prerequis = value;
            }
        }

        #endregion

        #region Methodes

        public int getNote(Etudiant e)
        {
            if(e != null)
            {
                for(int i = 0; i < liste_etudiant.Length; i++)
                {
                    if(e.Id == liste_etudiant[i])
                    {
                        return liste_note[i];
                    }
                }
                return -1;
            } else
            {
                return -1;
            }
        }

        #endregion
    }
}