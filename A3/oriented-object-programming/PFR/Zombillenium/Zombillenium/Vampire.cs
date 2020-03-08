using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium
{
    class Vampire: Monstre, IComparable<Vampire>
    {
        private double indiceLuminosite;

        #region Getters / Setters

        public double IndiceLuminosite
        {
            get
            {
                return indiceLuminosite;
            }

            set
            {
                indiceLuminosite = value;
            }
        }

        #endregion

        #region Constructeurs

        public Vampire(int matricule, string nom, string prenom, string genre, string fonction, int cagnotte, Attraction affectation, float luminosite) : base(matricule, nom, prenom, genre, fonction, cagnotte, affectation)
        {
            this.IndiceLuminosite = luminosite;
        }

        public Vampire(int matricule, string nom, string prenom, string genre, string fonction, int cagnotte, string affectation, float luminosite) : base(matricule, nom, prenom, genre, fonction, cagnotte, affectation)
        {
            this.IndiceLuminosite = luminosite;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            String pronom = getPronomFromSexe();
            if (Affectation != null)
            {
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un vampire qui possède " + Cagnotte + " points dans sa cagnotte et qui est affecté à " + Affectation.Nom + ". "
                    + pronom + " a un degré de luminosité de " + IndiceLuminosite + ". " + getStringFromFonction();
            }
            else if (AffectationAutre != null)
            {
                string aff = (AffectationAutre == "parc") ? "est affecté au parc" : "ne peut pas être affecté";
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un vampire qui possède " + Cagnotte + " points dans sa cagnotte et qui " + aff + ". "
                    + pronom + " a un degré de luminosité de " + IndiceLuminosite + ". " + getStringFromFonction();
            }
            else
            {
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un vampire qui possède " + Cagnotte + " points dans sa cagnotte et qui n'est affecté à aucune attraction. "
                    + pronom + " a un degré de luminosité de " + IndiceLuminosite + ". " + getStringFromFonction();
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
            stringBuilder.Append("Vampire")
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
            stringBuilder.Append(IndiceLuminosite).Append(";");

            return stringBuilder.ToString();
        }

        #endregion

        #region Tris

        /// <summary>
        /// Tri par défaut - par Indice de Luminosité
        /// </summary>
        /// <param name="other">Vampire à comparer</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        public int CompareTo(Vampire other)
        {
            return indiceLuminosite.CompareTo(other.indiceLuminosite);
        }

        /// <summary>
        /// Trie une liste de Vampires par leur indice de luminosité
        /// </summary>
        /// <param name="liste">Liste de Vampires à trier</param>
        public static void TrierListeVampires(List<Vampire> liste)
        {
            if(liste != null && liste.Count > 0)
            {
                liste.Sort();
            }
        }

        #endregion
    }
}
