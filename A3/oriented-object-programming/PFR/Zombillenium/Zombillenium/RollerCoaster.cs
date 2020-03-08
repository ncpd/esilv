using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium
{
    enum TypeCategorie { assise, bobsleigh, inversee };
    class RollerCoaster : Attraction, IComparable<RollerCoaster>
    {
        private int ageMin;
        private TypeCategorie categorie;
        private double tailleMin;

        #region Getters / Setters

        public int AgeMin
        {
            get
            {
                return ageMin;
            }

            set
            {
                ageMin = value;
            }
        }

        internal TypeCategorie Categorie
        {
            get
            {
                return categorie;
            }

            set
            {
                categorie = value;
            }
        }

        public double TailleMin
        {
            get
            {
                return tailleMin;
            }

            set
            {
                tailleMin = value;
            }
        }

        #endregion

        #region Constructeur

        public RollerCoaster(int id, string nom, int nbMinMonstres, bool besoinSpec, string typeBesoin, string categ, int ageMin, double tailleMin) : base(id, nom, nbMinMonstres, besoinSpec, typeBesoin)
        {
            this.AgeMin = ageMin;
            switch (categ)
            {
                case "assise":
                    Categorie = TypeCategorie.assise;
                    break;
                case "inversee":
                    Categorie = TypeCategorie.inversee;
                    break;
                case "bobsleigh":
                    Categorie = TypeCategorie.bobsleigh;
                    break;
                default:
                    break;
            }
            this.TailleMin = tailleMin;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            string monster = (NbMinMonstres > 1) ? "monstres" : "monstre";
            if (Maintenance)
            {
                return Nom + " (id n°" + Identifiant + ") est un RollerCoaster où il faut être agé de " + AgeMin + " ans et faire " + TailleMin.ToString() + "m minimum pour y rentrer. Sa catégorie est " + Categorie.ToString() + ". "
                    + " Il a besoin de " + NbMinMonstres + " " + monster + " pour fonctionner. "
                    + getStringFromStatus() + getStringFromBesoin() + getStringFromEquipe() + " Il est en maintenance pour " + NatureMaintenance + ".";
            }
            else
            {
                return Nom + " (id n°" + Identifiant + ") est un RollerCoaster où il faut être agé de " + AgeMin + " ans et faire " + TailleMin.ToString() + "m minimum pour y rentrer. Sa catégorie est " + Categorie.ToString() + ". "
                    + " Il a besoin de " + NbMinMonstres + " " + monster + " pour fonctionner. "
                    + getStringFromStatus() + getStringFromBesoin() + getStringFromEquipe();
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
            stringBuilder.Append("RollerCoaster")
                         .Append(";")
                         .Append(Identifiant)
                         .Append(";")
                         .Append(Nom)
                         .Append(";")
                         .Append(NbMinMonstres)
                         .Append(";")
                         .Append(BesoinSpecifique)
                         .Append(";")
                         .Append(TypeDeBesoin)
                         .Append(";")
                         .Append(Categorie.ToString())
                         .Append(";")
                         .Append(AgeMin)
                         .Append(";")
                         .Append(TailleMin)
                         .Append(";");
            return stringBuilder.ToString();
        }

        #endregion

        #region Tris

        /// <summary>
        /// Tri par défaut - par age minimum
        /// </summary>
        /// <param name="other">Autre RollerCoaster à comparer</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        public int CompareTo(RollerCoaster other)
        {
            return this.AgeMin.CompareTo(other.AgeMin);
        }

        /// <summary>
        /// Tri par Catégorie 
        /// </summary>
        /// <param name="first">RollerCoaster à comparer au second</param>
        /// <param name="other">RollerCoaster à comparer au premier</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        private static int TriParCategorie(RollerCoaster first, RollerCoaster other)
        {
            return first.Categorie.CompareTo(other.Categorie);
        }

        /// <summary>
        /// Tri par Taille minimale 
        /// </summary>
        /// <param name="first">RollerCoaster à comparer au second</param>
        /// <param name="other">RollerCoaster à comparer au premier</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        private static int TriParTailleMin(RollerCoaster first, RollerCoaster other)
        {
            return first.TailleMin.CompareTo(other.TailleMin);
        }

        #endregion

        #region Méthodes de Tri appelées à l'extérieur

        /// <summary>
        /// Trie une liste de RollerCoasters par Age minimum
        /// </summary>
        /// <param name="liste">Liste de RollerCoasters à trier</param>
        public static void TrierListeRollerCoastersParAgeMin(List<RollerCoaster> liste)
        {
            if(liste != null && liste.Count > 0)
            {
                liste.Sort();
            }
        }

        /// <summary>
        /// Trie une liste de RollerCoasters par Categorie
        /// </summary>
        /// <param name="liste">Liste de RollerCoasters à trier</param>
        public static void TrierListeRollerCoastersParCategorie(List<RollerCoaster> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort(TriParCategorie);
            }
        }

        /// <summary>
        /// Trie une liste de RollerCoasters par Taille minimum
        /// </summary>
        /// <param name="liste">Liste de RollerCoasters à trier</param>
        public static void TrierListeRollerCoastersParTailleMin(List<RollerCoaster> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort(TriParTailleMin);
            }
        }

        #endregion

        #region Filtrage

        /// <summary>
        /// Filtre une liste de rollercoasters par Besoin Spécifique (booléen)
        /// </summary>
        /// <param name="rollerCoasters">Liste de rollercoasters à filtrer</param>
        /// <param name="critere">Critere de tri</param>
        /// <returns>Liste de rollercoasters ordonnée</returns>
        public static List<RollerCoaster> FiltrerRollerCoastersParBesoinSpec(List<RollerCoaster> rollerCoasters, string critere)
        {
            switch (critere)
            {
                case "besoinSpec":
                    return rollerCoasters.Where(b => b.BesoinSpecifique).ToList();
                default:
                    return rollerCoasters;
            }
        }

        /// <summary>
        /// Filtre une liste de rollercoasters par Nb Min Monstres ou par Taille ou par Age minimum
        /// </summary>
        /// <param name="rollerCoasters">Liste de rollerCoasters à filtrer</param>
        /// <param name="critere">Critere de tri</param>
        /// <param name="operateur">Opérateur de comparaison</param>
        /// <param name="value">Valeur de comparaison</param>
        /// <returns>Liste de rollercoasters ordonnée</returns>
        public static List<RollerCoaster> FiltrerRollerCoastersParNbMinMonstresTailleAgeMin(List<RollerCoaster> rollerCoasters, string critere, string operateur, double value)
        {
            switch (critere)
            {
                case "nbMinMonstres":
                    value = (int)value;
                    switch (operateur)
                    {
                        case ">":
                            return rollerCoasters.Where(d => d.NbMinMonstres > value).ToList();
                        case ">=":
                            return rollerCoasters.Where(d => d.NbMinMonstres >= value).ToList();
                        case "<":
                            return rollerCoasters.Where(d => d.NbMinMonstres < value).ToList();
                        case "<=":
                            return rollerCoasters.Where(d => d.NbMinMonstres <= value).ToList();
                        case "=":
                            return rollerCoasters.Where(d => d.NbMinMonstres == value).ToList();
                        default:
                            break;
                    }
                    return rollerCoasters;
                case "age":
                    value = (int)value;
                    switch (operateur)
                    {
                        case ">":
                            return rollerCoasters.Where(d => d.AgeMin > value).ToList();
                        case ">=":
                            return rollerCoasters.Where(d => d.AgeMin >= value).ToList();
                        case "<":
                            return rollerCoasters.Where(d => d.AgeMin < value).ToList();
                        case "<=":
                            return rollerCoasters.Where(d => d.AgeMin <= value).ToList();
                        case "=":
                            return rollerCoasters.Where(d => d.AgeMin == value).ToList();
                        default:
                            break;
                    }
                    return rollerCoasters;
                case "taille":
                    switch (operateur)
                    {
                        case ">":
                            return rollerCoasters.Where(d => d.TailleMin > value).ToList();
                        case ">=":
                            return rollerCoasters.Where(d => d.TailleMin >= value).ToList();
                        case "<":
                            return rollerCoasters.Where(d => d.TailleMin < value).ToList();
                        case "<=":
                            return rollerCoasters.Where(d => d.TailleMin <= value).ToList();
                        case "=":
                            return rollerCoasters.Where(d => d.TailleMin == value).ToList();
                        default:
                            break;
                    }
                    return rollerCoasters;
                default:
                    return rollerCoasters;
            }
        }

        /// <summary>
        /// Filtre une liste de rollercoasters par Type de Besoin ou par Categorie
        /// </summary>
        /// <param name="rollercoasters">Liste de rollercoasters à filtrer</param>
        /// <param name="critere">Critere de tri</param>
        /// <param name="value">Chaine de comparaison</param>
        /// <returns>Liste de rollercoasters ordonnée</returns>
        public static List<RollerCoaster> FiltrerRollerCoastersParCategorieOuBesoin(List<RollerCoaster> rollerCoasters, string critere, string value)
        {
            switch (critere)
            {
                case "typeBesoin":
                    return rollerCoasters.Where(b => b.TypeDeBesoin.Equals(value)).ToList();
                case "categorie":
                    if (value.Equals("assise"))
                    {
                        return rollerCoasters.Where(r => r.Categorie == TypeCategorie.assise).ToList();
                    }
                    else if (value.Equals("inversee"))
                    {
                        return rollerCoasters.Where(r => r.Categorie == TypeCategorie.inversee).ToList();
                    }
                    else if (value.Equals("bobsleigh"))
                    {
                        return rollerCoasters.Where(r => r.Categorie == TypeCategorie.bobsleigh).ToList();
                    }
                    else
                    {
                        return rollerCoasters;
                    }
                default:
                    return rollerCoasters;
            }
        }

        #endregion
    }
}
