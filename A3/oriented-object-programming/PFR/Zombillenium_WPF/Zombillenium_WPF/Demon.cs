using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium_WPF
{
    class Demon : Monstre, IExportable, IComparable<Demon>
    {
        private int force;

        #region Getters / Setters

        public int Force
        {
            get
            {
                return force;
            }

            set
            {
                force = value;
            }
        }

        #endregion

        #region Constructeurs

        public Demon(int matricule, string nom, string prenom, string genre, string fonction, int cagnotte, Attraction affectation, int force) : base(matricule, nom, prenom, genre, fonction, cagnotte, affectation)
        {
            this.Force = force;
        }

        public Demon(int matricule, string nom, string prenom, string genre, string fonction, int cagnotte, string affectation, int force) : base(matricule, nom, prenom, genre, fonction, cagnotte, affectation)
        {
            this.Force = force;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            String pronom = getPronomFromSexe();
            if (Affectation != null)
            {
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un démon qui possède " + Cagnotte + " points dans sa cagnotte et qui est affecté à " + Affectation.Nom + ". "
                    + pronom + " a une force de " + Force + ". " + getStringFromFonction();
            }
            else if (AffectationAutre != null)
            {
                string aff = (AffectationAutre == "parc") ? "est affecté au parc" : "ne peut pas être affecté";
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un démon qui possède " + Cagnotte + " points dans sa cagnotte et qui " + aff + ". "
                    + pronom + " a une force de " + Force + ". " + getStringFromFonction();
            }
            else
            {
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un démon qui possède " + Cagnotte + " points dans sa cagnotte et qui n'est affecté à aucune attraction. "
                    + pronom + " a une force de " + Force + ". " + getStringFromFonction();
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
            stringBuilder.Append("Demon")
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
            stringBuilder.Append(Force).Append(";");

            return stringBuilder.ToString();
        }

        #endregion

        #region Tris

        /// <summary>
        /// Tri par défaut - par Force
        /// </summary>
        /// <param name="other">Démon à comparer</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        public int CompareTo(Demon other)
        {
            return this.force.CompareTo(other.force);
        }

        /// <summary>
        /// Trie une liste de démons
        /// </summary>
        /// <param name="liste">Liste de démons à trier</param>
        public static void TrierListeDemons(List<Demon> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort();
            }
        }

        #endregion
    }
}
