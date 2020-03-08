using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium
{
    enum Grade { novice, mega, giga, strata };
    class Sorcier : Personnel, IExportable, IComparable<Sorcier>
    {
        private Grade tatouage;
        private List<string> pouvoirs;

        #region Getters / Setters

        internal Grade Tatouage
        {
            get
            {
                return tatouage;
            }

            set
            {
                tatouage = value;
            }
        }

        public List<string> Pouvoirs
        {
            get
            {
                return pouvoirs;
            }

            set
            {
                pouvoirs = value;
            }
        }

        #endregion

        #region Constructeur

        public Sorcier(int matricule, string nom, string prenom, string genre, string fonction, string tatoo, List<string> pouvoirs) : base(fonction, matricule, nom, prenom, genre)
        {
            switch (tatoo)
            {
                case "novice":
                    Tatouage = Grade.novice;
                    break;
                case "mega":
                    Tatouage = Grade.mega;
                    break;
                case "giga":
                    Tatouage = Grade.giga;
                    break;
                case "strata":
                    Tatouage = Grade.strata;
                    break;
                default:
                    Tatouage = Grade.novice;
                    break;
            }
            this.Pouvoirs = pouvoirs;
        }

        #endregion

        #region Export CSV

        /// <summary>
        /// Récupère la ligne à écrire dans un fichier CSV qui représente l'objet actuel
        /// </summary>
        /// <returns></returns>
        public string GetCSVline()
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder pvrs = new StringBuilder();
            for (int i = 0; i < Pouvoirs.Count; i++)
            {
                if (i == Pouvoirs.Count - 1)
                {
                    pvrs.Append(Pouvoirs.ElementAt(i));
                }
                else
                {
                    pvrs.Append(Pouvoirs.ElementAt(i)).Append("-");
                }
            }
            stringBuilder.Append("Sorcier")
                         .Append(";")
                         .Append(Matricule)
                         .Append(";")
                         .Append(Nom)
                         .Append(";")
                         .Append(Prenom)
                         .Append(";")
                         .Append(Sexe.ToString())
                         .Append(";")
                         .Append(Fonction)
                         .Append(";")
                         .Append(Tatouage.ToString())
                         .Append(";")
                         .Append(pvrs)
                         .Append(";");
            return stringBuilder.ToString();
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            Boolean possedePouvoirs = false;
            StringBuilder stringBuilder = new StringBuilder();
            if (Pouvoirs != null)
            {
                possedePouvoirs = true;
                for (int i = 0; i < Pouvoirs.Count; i++)
                {
                    if(i == Pouvoirs.Count - 1)
                    {
                        stringBuilder.Append(Pouvoirs.ElementAt(i).ToString()).Append(". ");
                    } else
                    {
                        stringBuilder.Append(Pouvoirs.ElementAt(i).ToString()).Append(", ");
                    }
                }
            }
            String pronom = getPronomFromSexe();
            String wiz = (Sexe == TypeSexe.male) ? "un sorcier" : "une sorcière";
            if (possedePouvoirs)
            {
                return Prenom + " " + Nom + " (n°" + Matricule + ") est " + wiz + " de grade " + tatouage.ToString() + ". " + pronom + " possède les pouvoirs suivants : " + stringBuilder + getStringFromFonction();
            }
            else
            {
                return Prenom + " " + Nom + " (n°" + Matricule + ") est " + wiz + " de grade " + tatouage.ToString() + ". " + pronom + " ne possède aucun pouvoir. " + getStringFromFonction();
            }
        }

        #endregion

        #region Tris

        /// <summary>
        /// Tri par défaut - par Grade
        /// </summary>
        /// <param name="other">Autre Sorcier à comparer</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        public int CompareTo(Sorcier other)
        {
            return tatouage.CompareTo(other.tatouage);
        }

        /// <summary>
        /// Trie une liste de Sorciers par leur Grade
        /// </summary>
        /// <param name="liste">Liste de Sorciers à trier</param>
        public static void TrierListeSorciersParGrade(List<Sorcier> liste)
        {
            if(liste != null && liste.Count > 0)
            {
                liste.Sort();
            }
        }

        #endregion
    }
}
