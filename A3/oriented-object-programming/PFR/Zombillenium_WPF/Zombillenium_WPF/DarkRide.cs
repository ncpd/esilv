using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium_WPF
{
    class DarkRide : Attraction, IComparable<DarkRide>
    {
        private TimeSpan duree;
        private bool vehicule;

        #region Getters / Setters

        public TimeSpan Duree
        {
            get
            {
                return duree;
            }
            set
            {
                duree = value;
            }
        }

        public bool Vehicule
        {
            get
            {
                return vehicule;
            }
            set
            {
                vehicule = value;
            }
        }

        #endregion

        #region Constructeur

        public DarkRide(int id, string nom, int nbMinMonstres, bool besoinSpec, string typeBesoin, string duree, bool vehicule) : base(id, nom, nbMinMonstres, besoinSpec, typeBesoin)
        {
            this.duree = TimeSpan.FromMinutes(Double.Parse(duree));
            this.vehicule = vehicule;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            string vehic = (vehicule) ? "est véhiculé." : "n'est pas véhiculé.";
            string monster = (NbMinMonstres > 1) ? "monstres" : "monstre";

            if (Maintenance)
            {
                return Nom + " (id n°" + Identifiant + ") est un Darkride qui dure " + duree.Minutes.ToString() + " minutes et " + vehic + getStringFromStatus()
                    + " Il a besoin de " + NbMinMonstres + " " + monster + " pour fonctionner. "
                    + getStringFromBesoin() + getStringFromEquipe() + " Il est en maintenance pour " + NatureMaintenance + ".";
            }
            else
            {
                return Nom + " (id n°" + Identifiant + ") est un Darkride qui dure " + duree.Minutes.ToString() + " minutes et " + vehic + getStringFromStatus()
                    + " Il a besoin de " + NbMinMonstres + " " + monster + " pour fonctionner. "
                    + getStringFromBesoin() + getStringFromEquipe();
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
            stringBuilder.Append("DarkRide")
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
                         .Append(duree.Minutes.ToString())
                         .Append(";")
                         .Append(vehicule)
                         .Append(";");
            return stringBuilder.ToString();
        }

        #endregion

        #region Tris

        /// <summary>
        /// Tri par défaut - par durée
        /// </summary>
        /// <param name="other">DarkRide à comparer au DarkRide actuel</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        public int CompareTo(DarkRide other)
        {
            return duree.CompareTo(other.Duree);
        }

        /// <summary>
        /// Tri par véhicule
        /// </summary>
        /// <param name="d1">DarkRide à comparer au second DarkRide</param>
        /// <param name="d2">DarkRide à comparer au premier DarkRide</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        private static int TriParVehicule(DarkRide d1, DarkRide d2)
        {
            return d1.vehicule.CompareTo(d2.Vehicule);
        }

        #endregion

        #region Méthodes de Tri appelées à l'extérieur

        /// <summary>
        /// Trie une liste de DarkRides par durée
        /// </summary>
        /// <param name="liste">Liste de DarkRides à trier</param>
        public static void TrierListeDarkRidesParDuree(List<DarkRide> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort();
            }
        }

        /// <summary>
        /// Trie une liste de DarkRides par Véhicule
        /// </summary>
        /// <param name="liste">Liste de DarkRides à trier</param>
        public static void TrierListeDarkRidesParVehicule(List<DarkRide> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort(TriParVehicule);
            }
        }

        #endregion

        #region Filtrage

        /// <summary>
        /// Filtre une liste de darkrides par Besoin Spécifique ou par Véhicule (booléen)
        /// </summary>
        /// <param name="darkRides">Liste de darkrides à filtrer</param>
        /// <param name="critere">Critere de tri</param>
        /// <returns>Liste de darkrides ordonnée</returns>
        public static List<DarkRide> FiltrerDarkRidesParBesoinSpecOuVehicule(List<DarkRide> darkRides, string critere)
        {
            switch (critere)
            {
                case "besoinSpec":
                    return darkRides.Where(b => b.BesoinSpecifique).ToList();
                case "vehicule":
                    return darkRides.Where(b => b.Vehicule).ToList();
                default:
                    return darkRides;
            }
        }

        /// <summary>
        /// Filtre une liste de darkrides par Nb Min Monstres ou par Durée (entier)
        /// </summary>
        /// <param name="darkRides">Liste de darkrides à filtrer</param>
        /// <param name="critere">Critere de tri</param>
        /// <param name="operateur">Opérateur de comparaison</param>
        /// <param name="value">Valeur de comparaison</param>
        /// <returns>Liste de darkrides ordonnée</returns>
        public static List<DarkRide> FiltrerDarkRidesParNbMinMonstresOuDuree(List<DarkRide> darkRides, string critere, string operateur, int value)
        {
            switch (critere)
            {
                case "nbMinMonstres":
                    switch (operateur)
                    {
                        case ">":
                            return darkRides.Where(d => d.NbMinMonstres > value).ToList();
                        case ">=":
                            return darkRides.Where(d => d.NbMinMonstres >= value).ToList();
                        case "<":
                            return darkRides.Where(d => d.NbMinMonstres < value).ToList();
                        case "<=":
                            return darkRides.Where(d => d.NbMinMonstres <= value).ToList();
                        case "=":
                            return darkRides.Where(d => d.NbMinMonstres == value).ToList();
                        default:
                            break;
                    }
                    return darkRides;
                case "duree":
                    switch (operateur)
                    {
                        case ">":
                            return darkRides.Where(d => d.duree.Minutes > value).ToList();
                        case ">=":
                            return darkRides.Where(d => d.duree.Minutes >= value).ToList();
                        case "<":
                            return darkRides.Where(d => d.duree.Minutes < value).ToList();
                        case "<=":
                            return darkRides.Where(d => d.duree.Minutes <= value).ToList();
                        case "=":
                            return darkRides.Where(d => d.duree.Minutes == value).ToList();
                        default:
                            break;
                    }
                    return darkRides;
                default:
                    return darkRides;
            }
        }

        /// <summary>
        /// Filtre une liste de darkrides par Type de Besoin
        /// </summary>
        /// <param name="darkRides">Liste de darkrides à filtrer</param>
        /// <param name="critere">Critere de tri</param>
        /// <param name="value">Chaine de comparaison</param>
        /// <returns>Liste de darkrides ordonnée</returns>
        public static List<DarkRide> FiltrerDarkRidesParTypeDeBesoin(List<DarkRide> darkRides, string critere, string value)
        {
            switch (critere)
            {
                case "typeBesoin":
                    return darkRides.Where(b => b.TypeDeBesoin.Equals(value)).ToList();
                default:
                    return darkRides;
            }
        }

        #endregion
    }
}
