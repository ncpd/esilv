using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium
{
    class Spectacle : Attraction, IComparable<Spectacle>
    {
        private List<DateTime> horaires;
        private int nombrePlaces;
        private string nomSalle;

        #region Constructeur

        public Spectacle(int id, string nom, int nbMinMonstres, bool besoinSpec, string typeBesoin, string nomSalle, int nbPlaces, string horairesStr) : base(id, nom, nbMinMonstres, besoinSpec, typeBesoin)
        {
            horaires = new List<DateTime>();
            String[] horairesSplitted = horairesStr.Split(' ');
            for(int i = 0; i < horairesSplitted.Length; i++)
            {
                DateTime hor = DateTime.ParseExact(horairesSplitted[i], "HH:mm",
                                       System.Globalization.CultureInfo.InvariantCulture);
                horaires.Add(hor);
            }
            this.nombrePlaces = nbPlaces;
            this.nomSalle = nomSalle;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            StringBuilder strHoraires = new StringBuilder();
            for(int i = 0; i < horaires.Count; i++)
            {
                if(i == horaires.Count - 1)
                {
                    strHoraires.Append(horaires.ElementAt(i).ToString("HH:mm")).Append(".");
                } else
                {
                    strHoraires.Append(horaires.ElementAt(i).ToString("HH:mm")).Append(" - ");
                }
            }
            string monster = (NbMinMonstres > 1) ? "monstres" : "monstre";
            if (Maintenance)
            {
                return Nom + " (id n°" + Identifiant + ") est un spectacle qui se joue dans la salle " + nomSalle + " (" + nombrePlaces + " places) et qui se joue aux horaires suivants : " + strHoraires
                    + " Il a besoin de " + NbMinMonstres + " " + monster + " pour fonctionner. " + 
                    getStringFromStatus() + getStringFromBesoin() + getStringFromEquipe() + " Il est en maintenance pour " + NatureMaintenance + ".";
            }
            else
            {
                return Nom + " (id n°" + Identifiant + ") est un spectacle qui se joue dans la salle " + nomSalle + " (" + nombrePlaces + " places) et qui se joue aux horaires suivants : " + strHoraires
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
            StringBuilder times = new StringBuilder();
            for (int i = 0; i < horaires.Count; i++)
            {
                if (i == horaires.Count - 1)
                {
                    times.Append(horaires.ElementAt(i).ToString("HH:mm"));
                }
                else
                {
                    times.Append(horaires.ElementAt(i).ToString("HH:mm")).Append(" ");
                }
            }
            stringBuilder.Append("Spectacle")
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
                         .Append(nomSalle)
                         .Append(";")
                         .Append(nombrePlaces)
                         .Append(";")
                         .Append(times)
                         .Append(";");
            return stringBuilder.ToString();
        }

        #endregion

        #region Tris

        /// <summary>
        /// Tri par défaut - par nombre de places
        /// </summary>
        /// <param name="other">Autre Spectacle à comparer</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        public int CompareTo(Spectacle other)
        {
            return nombrePlaces.CompareTo(other.nombrePlaces);
        }

        /// <summary>
        /// Tri par Nom de Salle 
        /// </summary>
        /// <param name="first">Spectacle à comparer au second</param>
        /// <param name="other">Spectacle à comparer au premier</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        private static int TriParNomSalle(Spectacle first, Spectacle other)
        {
            return first.nomSalle.CompareTo(other.nomSalle);
        }

        #endregion

        #region Méthodes de Tri appelées à l'extérieur

        /// <summary>
        /// Trie une liste de Spectacles par leur nombre de places
        /// </summary>
        /// <param name="liste">Liste de Spectacles à trier</param>
        public static void TrierListeSpectaclesParNbPlaces(List<Spectacle> liste)
        {
            if(liste != null && liste.Count > 0)
            {
                liste.Sort();
            }
        }

        /// <summary>
        /// Trie une liste de Spectacles par leur nom de salle
        /// </summary>
        /// <param name="liste">Liste de Spectacles à trier</param>
        public static void TrierListeSpectaclesParNomSalle(List<Spectacle> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort(TriParNomSalle);
            }
        }

        #endregion

        #region Filtrage

        /// <summary>
        /// Filtre une liste de spectacles par Besoin Spécifique (booléen)
        /// </summary>
        /// <param name="spectacles">Liste de spectacles à filtrer</param>
        /// <param name="critere">Critere de tri</param>
        /// <returns>Liste de spectacles ordonnée</returns>
        public static List<Spectacle> FiltrerSpectaclesParBesoinSpec(List<Spectacle> spectacles, string critere)
        {
            switch (critere)
            {
                case "besoinSpec":
                    return spectacles.Where(b => b.BesoinSpecifique).ToList();
                default:
                    return spectacles;
            }
        }

        /// <summary>
        /// Filtre une liste de spectacles par Nb Min Monstres ou par Nombre de Places
        /// </summary>
        /// <param name="spectacles">Liste de spectacles à filtrer</param>
        /// <param name="critere">Critere de tri</param>
        /// <param name="operateur">Opérateur de comparaison</param>
        /// <param name="value">Valeur de comparaison</param>
        /// <returns>Liste de spectacles ordonnée</returns>
        public static List<Spectacle> FiltrerSpectaclesParNbMinMonstresPlaces(List<Spectacle> spectacles, string critere, string operateur, int value)
        {
            switch (critere)
            {
                case "nbMinMonstres":
                    switch (operateur)
                    {
                        case ">":
                            return spectacles.Where(d => d.NbMinMonstres > value).ToList();
                        case ">=":
                            return spectacles.Where(d => d.NbMinMonstres >= value).ToList();
                        case "<":
                            return spectacles.Where(d => d.NbMinMonstres < value).ToList();
                        case "<=":
                            return spectacles.Where(d => d.NbMinMonstres <= value).ToList();
                        case "=":
                            return spectacles.Where(d => d.NbMinMonstres == value).ToList();
                        default:
                            break;
                    }
                    return spectacles;
                case "places":
                    switch (operateur)
                    {
                        case ">":
                            return spectacles.Where(d => d.nombrePlaces > value).ToList();
                        case ">=":
                            return spectacles.Where(d => d.nombrePlaces >= value).ToList();
                        case "<":
                            return spectacles.Where(d => d.nombrePlaces < value).ToList();
                        case "<=":
                            return spectacles.Where(d => d.nombrePlaces <= value).ToList();
                        case "=":
                            return spectacles.Where(d => d.nombrePlaces == value).ToList();
                        default:
                            break;
                    }
                    return spectacles;
                default:
                    return spectacles;
            }
        }

        /// <summary>
        /// Filtre une liste de spectacles par Nom de Salle ou par Type de Besoin (string)
        /// </summary>
        /// <param name="spectacles">Liste de spectacles à filtrer</param>
        /// <param name="critere">Critere de tri</param>
        /// <param name="value">Chaine de comparaison</param>
        /// <returns>Liste de spectacles ordonnée</returns>
        public static List<Spectacle> FiltrerSpectaclesParSalleOuBesoin(List<Spectacle> spectacles, string critere, string value)
        {
            switch (critere)
            {
                case "typeBesoin":
                    return spectacles.Where(b => b.TypeDeBesoin.Equals(value)).ToList();
                case "nomSalle":
                    return spectacles.Where(b => b.nomSalle.Equals(value)).ToList();
                default:
                    return spectacles;
            }
        }

        #endregion
    }
}
