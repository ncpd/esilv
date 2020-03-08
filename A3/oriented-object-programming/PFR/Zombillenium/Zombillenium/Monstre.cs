using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium
{
    class Monstre : Personnel, IExportable, IComparable<Monstre>
    {
       private Attraction affectation;
       private string affectationAutre;
       private int cagnotte;

        #region Getters / Setters
        public Attraction Affectation
        {
            get
            {
                return affectation;
            }

            set
            {
                affectation = value;
            }
        }

        public int Cagnotte
        {
            get
            {
                return cagnotte;
            }

            set
            {
                cagnotte = value;
            }
        }

        public string AffectationAutre
        {
            get
            {
                return affectationAutre;
            }
            set
            {
                this.affectationAutre = value;
            }
        }
        #endregion

        #region Constructeurs

        public Monstre(int matricule, string nom, string prenom, string genre, string fonction, int cagnotte, Attraction affectation) : base(fonction, matricule, nom, prenom, genre)
        {
            this.affectationAutre = null;
            this.affectation = affectation;
            if(affectation != null)
            {
                affectation.Equipe.Add(this);
            }
            this.cagnotte = cagnotte;
        }

        public Monstre(int matricule, string nom, string prenom, string genre, string fonction, int cagnotte, string affectation) : base(fonction, matricule, nom, prenom, genre)
        {
            this.affectation = null;
            this.affectationAutre = affectation;
            this.cagnotte = cagnotte;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            if (Affectation != null)
            {
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un monstre qui possède " + Cagnotte + " points dans sa cagnotte et qui est affecté à " + Affectation.Nom + ". "
                    + getStringFromFonction();
            }
            else if (AffectationAutre != null)
            {
                string aff = (affectationAutre == "parc") ? "est affecté au parc" : "ne peut pas être affecté";
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un monstre qui possède " + Cagnotte + " points dans sa cagnotte et qui " + aff + ". "
                    + getStringFromFonction();
            }
            else
            {
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un monstre qui possède " + Cagnotte + " points dans sa cagnotte et qui n'est affecté à aucune attraction. "
                    + getStringFromFonction();
            }
        }

        #endregion

        #region Export CSV

        /// <summary>
        /// Récupère la ligne à écrire dans un fichier CSV qui représente l'objet actuel
        /// </summary>
        /// <returns></returns>
        public virtual string GetCSVline()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Monstre")
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

            return stringBuilder.ToString();
        }

        #endregion

        #region Tris

        /// <summary>
        /// Tri par défaut - par cagnotte
        /// </summary>
        /// <param name="other">Autre Monstre à comparer</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        public int CompareTo(Monstre other)
        {
            return cagnotte.CompareTo(other.cagnotte);
        }

        /// <summary>
        /// Tri par Nom d'Affectation (de type Attraction)
        /// </summary>
        /// <param name="first">Monstre à comparer au second</param>
        /// <param name="other">Monstre à comparer au premier</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        private static int TriParAffectationAttraction(Monstre first, Monstre other)
        {
            if(first.Affectation != null && other.Affectation != null)
            {
                return first.Affectation.Nom.CompareTo(other.Affectation.Nom);
            }
            return 0;
        }

        /// <summary>
        /// Tri par Affectation (de type string, ie. parc, neant..)
        /// </summary>
        /// <param name="first">Monstre à comparer au second</param>
        /// <param name="other">Monstre à comparer au premier</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        private static int TriParAffectationString(Monstre first, Monstre other)
        {
            if (first.AffectationAutre != null && other.AffectationAutre != null)
            {
                return first.AffectationAutre.CompareTo(other.AffectationAutre);
            }
            return 0;
        }

        #endregion

        #region Méthodes de Tri appelées à l'extérieur

        /// <summary>
        /// Trie une liste de Monstres par Cagnotte
        /// </summary>
        /// <param name="liste">Liste de Monstre à trier</param>
        public static void TrierListeMonstresParCagnotte(List<Monstre> liste)
        {
            if(liste != null && liste.Count > 0)
            {
                liste.Sort();
            }
        }

        /// <summary>
        /// Trie une liste de Monstres par Affectation de type Attraction
        /// </summary>
        /// <param name="liste">Liste de Monstre à trier</param>
        public static void TrierListeMonstresParAffectationAttraction(List<Monstre> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort(TriParAffectationAttraction);
            }
        }

        /// <summary>
        /// Trie une liste de Monstres par Affectation de type String
        /// </summary>
        /// <param name="liste">Liste de Monstre à trier</param>
        public static void TrierListeMonstresParAffectationString(List<Monstre> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort(TriParAffectationString);
            }
        }

        #endregion
    }
}
