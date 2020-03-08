using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium_WPF
{
    enum TypeSexe { male, femelle, autre };

    public abstract class Personnel
    {
        private string type;
        private string fonction;
        private int matricule;
        private string nom;
        private string prenom;
        private TypeSexe sexe;

        #region Getters / Setters

        public string Fonction
        {
            get
            {
                return fonction;
            }

            set
            {
                fonction = value;
            }
        }

        public int Matricule
        {
            get
            {
                return matricule;
            }

            set
            {
                matricule = value;
            }
        }

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

        public string Prenom
        {
            get
            {
                return prenom;
            }

            set
            {
                prenom = value;
            }
        }

        internal TypeSexe Sexe
        {
            get
            {
                return sexe;
            }

            set
            {
                sexe = value;
            }
        }

        public string Type
        {
            get
            {
                return this.GetType().ToString().Replace("Zombillenium_WPF.", "");
            }
        }

        #endregion

        #region Constructeur

        public Personnel(string fonction, int matricule, string nom, string prenom, string genre)
        {
            this.Fonction = fonction;
            this.Matricule = matricule;
            this.Nom = nom;
            this.Prenom = prenom;
            switch (genre)
            {
                case "male":
                    Sexe = TypeSexe.male;
                    break;
                case "femelle":
                    Sexe = TypeSexe.femelle;
                    break;
                case "autre":
                    Sexe = TypeSexe.autre;
                    break;
                default:
                    Sexe = TypeSexe.autre;
                    break;
            }
        }

        #endregion

        #region ToString

        /// <summary>
        /// Récupère le pronom à utiliser à partir du sexe du personnel actuel
        /// </summary>
        /// <returns></returns>
        public string getPronomFromSexe()
        {
            switch (Sexe)
            {
                case TypeSexe.male:
                    return "Il";
                case TypeSexe.femelle:
                    return "Elle";
                case TypeSexe.autre:
                    return "Cette chose";
                default:
                    return "Cette chose";
            }
        }

        /// <summary>
        /// Récupère la phrase représentant la fonction du personnel actuel
        /// </summary>
        /// <returns></returns>
        public string getStringFromFonction()
        {
            if (Fonction != null && !Fonction.Equals("neant"))
            {
                return getPronomFromSexe() + " est " + Fonction + ".";
            }
            else
            {
                return getPronomFromSexe() + " n'occupe pas de fonction.";
            }
        }

        public override string ToString()
        {
            switch (Sexe)
            {
                case TypeSexe.male:
                    return "Il est " + Fonction;
                case TypeSexe.femelle:
                    return "Elle est " + Fonction;
                case TypeSexe.autre:
                    return "C'est un " + Fonction;
                default:
                    return base.ToString();
            }

        }

        #endregion
    }
}
