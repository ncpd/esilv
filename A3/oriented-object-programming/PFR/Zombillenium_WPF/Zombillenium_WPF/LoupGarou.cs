using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium_WPF
{
    class LoupGarou : Monstre, IComparable<LoupGarou>
    {
        private double indiceCruaute;

        #region Getters / Setters

        public double IndiceCruaute
        {
            get
            {
                return indiceCruaute;
            }

            set
            {
                indiceCruaute = value;
            }
        }

        #endregion

        #region Constructeurs

        public LoupGarou(int matricule, string nom, string prenom, string genre, string fonction, int cagnotte, Attraction affectation, double cruaute) : base(matricule, nom, prenom, genre, fonction, cagnotte, affectation)
        {
            this.IndiceCruaute = cruaute;
        }

        public LoupGarou(int matricule, string nom, string prenom, string genre, string fonction, int cagnotte, string affectation, double cruaute) : base(matricule, nom, prenom, genre, fonction, cagnotte, affectation)
        {
            this.IndiceCruaute = cruaute;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            String pronom = getPronomFromSexe();
            if (Affectation != null)
            {
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un loup-garou qui possède " + Cagnotte + " points dans sa cagnotte et qui est affecté à " + Affectation.Nom + ". "
                    + pronom + " a un indice de cruauté de " + IndiceCruaute + ". " + getStringFromFonction();
            }
            else if (AffectationAutre != null)
            {
                string aff = (AffectationAutre == "parc") ? "est affecté au parc" : "ne peut pas être affecté";
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un loup-garou qui possède " + Cagnotte + " points dans sa cagnotte et qui " + aff + ". "
                    + pronom + " a un indice de cruauté de " + IndiceCruaute + ". " + getStringFromFonction();
            }
            else
            {
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un loup-garou qui possède " + Cagnotte + " points dans sa cagnotte et qui n'est affecté à aucune attraction. "
                    + pronom + " a un indice de cruauté de " + IndiceCruaute + ". " + getStringFromFonction();
            }
        }

        #endregion

        #region Export CSV

        /// <summary>
        /// Récupère la ligne à écrire dans un fichier CSV qui représente l'objet actuel
        /// </summary>
        /// <returns></returns>
        public override string GetCSVline()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("LoupGarou")
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
                         .Append(Cagnotte)
                         .Append(";");
            if (Affectation != null)
            {
                stringBuilder.Append(Affectation.Identifiant).Append(";");
            }
            else if (AffectationAutre != null)
            {
                stringBuilder.Append(AffectationAutre).Append(";");
            }
            else
            {
                stringBuilder.Append("").Append(";");
            }
            stringBuilder.Append(IndiceCruaute).Append(";");

            return stringBuilder.ToString();
        }

        #endregion

        #region Tris

        /// <summary>
        /// Tri par défaut - par indice de cruauté
        /// </summary>
        /// <param name="other">Autre LoupGarou à comparer</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        public int CompareTo(LoupGarou other)
        {
            return indiceCruaute.CompareTo(other.indiceCruaute);
        }

        /// <summary>
        /// Trie une liste de LoupGarous
        /// </summary>
        /// <param name="liste">Liste de LoupGarou à trier</param>
        public static void TrierListeLoupsGarous(List<LoupGarou> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort();
            }
        }

        #endregion
    }
}
