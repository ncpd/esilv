using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium_WPF
{
    public abstract class Attraction : IExportable, IComparable<Attraction>
    {
        private string type;
        private int identifiant;
        private string nom;
        private int nbMinMonstres;
        private bool besoinSpecifique;
        private string typeDeBesoin;
        private TimeSpan dureeMaintenance;
        private List<Monstre> equipe;
        private bool maintenance;
        private string natureMaintenance;
        private bool ouvert;

        #region Getters / Setters

        public string Type
        {
            get
            {
                return this.GetType().ToString().Replace("Zombillenium_WPF.", "");
            }
        }

        public string Nom
        {
            get
            {
                return nom;
            }
        }

        public int Identifiant
        {
            get
            {
                return identifiant;
            }
        }

        public int NbMinMonstres
        {
            get
            {
                return nbMinMonstres;
            }

            set
            {
                nbMinMonstres = value;
            }
        }

        public bool BesoinSpecifique
        {
            get
            {
                return besoinSpecifique;
            }

            set
            {
                besoinSpecifique = value;
            }
        }

        public string TypeDeBesoin
        {
            get
            {
                return typeDeBesoin;
            }

            set
            {
                typeDeBesoin = value;
            }
        }

        public TimeSpan DureeMaintenance
        {
            get
            {
                return dureeMaintenance;
            }
            set
            {
                dureeMaintenance = value;
            }
        }

        public List<Monstre> Equipe
        {
            get
            {
                return equipe;
            }
            set
            {
                this.equipe = value;
            }
        }

        public bool Maintenance
        {
            get
            {
                return maintenance;
            }

            set
            {
                maintenance = value;
            }
        }

        public string NatureMaintenance
        {
            get
            {
                return natureMaintenance;
            }

            set
            {
                natureMaintenance = value;
            }
        }

        public bool Ouvert
        {
            get
            {
                return ouvert;
            }

            set
            {
                ouvert = value;
            }
        }

        #endregion

        #region Constructeur

        public Attraction(int id, string nom, int nbMinMonstres, bool besoinSpec, string typeBesoin)
        {
            this.identifiant = id;
            this.nom = nom;
            this.nbMinMonstres = nbMinMonstres;
            this.besoinSpecifique = besoinSpec;
            this.typeDeBesoin = typeBesoin;
            this.dureeMaintenance = TimeSpan.Zero;
            this.equipe = new List<Monstre>();
            this.maintenance = false;
            this.natureMaintenance = null;
            this.ouvert = true;
        }

        #endregion

        #region Export CSV

        public virtual string GetCSVline()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Attraction")
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
                         .Append(";");
            return stringBuilder.ToString();
        }

        #endregion

        #region Strings depuis l'état de l'attraction

        /// <summary>
        /// Récupère un string représentant la durée de la maintenance
        /// </summary>
        /// <returns></returns>
        public string getStringFromDureeMaintenance()
        {
            StringBuilder phrase = new StringBuilder();
            int days = dureeMaintenance.Days;
            int hours = dureeMaintenance.Hours;
            int minutes = dureeMaintenance.Minutes;
            phrase.Append((days == 0) ? "" : days + " jours ");
            phrase.Append((hours == 0) ? "" : hours + " heures ");
            phrase.Append((minutes == 0) ? "" : minutes + " minutes ");

            return phrase.ToString();
        }

        /// <summary>
        /// Récupère une phrase selon l'état de l'attraction
        /// </summary>
        /// <returns>Phrase en string</returns>
        public string getStringFromStatus()
        {
            if (ouvert)
            {
                if (this is Boutique)
                {
                    // nom féminin
                    return " Elle est ouverte.";

                }
                else
                {
                    // nom masculin
                    return " Il est ouvert.";
                }
            }
            else if (maintenance)
            {
                if (this is Boutique)
                {
                    // nom féminin
                    return " Elle est en maintenance pour " + natureMaintenance + " pour une durée de " + getStringFromDureeMaintenance() + ".";

                }
                else
                {
                    // nom masculin
                    return " Il est en maintenance pour " + natureMaintenance + " pour une durée de " + getStringFromDureeMaintenance() + ".";
                }
            }
            else
            {
                if (this is Boutique)
                {
                    // nom féminin
                    return " Elle est fermée.";

                }
                else
                {
                    // nom masculin
                    return " Il est fermé.";
                }
            }
        }

        /// <summary>
        /// Récupère une phrase selon le besoin de l'attraction
        /// </summary>
        /// <returns>Phrase en string</returns>
        public string getStringFromBesoin()
        {
            if (BesoinSpecifique)
            {
                if (this is Boutique)
                {
                    return " Elle a besoin de " + TypeDeBesoin + "s pour fonctionner.";
                }
                else
                {
                    return " Il a besoin de " + TypeDeBesoin + "s pour fonctionner.";
                }
            }
            else
            {
                if (this is Boutique)
                {
                    return " Elle n'a pas de besoin spécifique.";
                }
                else
                {
                    return " Il n'a pas de besoin spécifique.";
                }
            }
        }

        /// <summary>
        /// Récupère une phrase selon l'équipe de l'attraction
        /// </summary>
        /// <returns>Phrase en string</returns>
        public string getStringFromEquipe()
        {
            StringBuilder builder = new StringBuilder();
            if (Equipe != null && Equipe.Count > 0)
            {
                for (int i = 0; i < Equipe.Count; i++)
                {
                    if (i == Equipe.Count - 1)
                    {
                        builder.Append(Equipe.ElementAt(i).Prenom + " " + Equipe.ElementAt(i).Nom).Append(".");
                    }
                    else
                    {
                        builder.Append(Equipe.ElementAt(i).Prenom + " " + Equipe.ElementAt(i).Nom).Append(", ");
                    }
                }
                return " Son équipe est composée de : " + builder;
            }
            else
            {
                return " Personne n'y travaille pour le moment.";
            }
        }

        #endregion

        #region Tris

        /// <summary>
        /// Tri par défaut - par Identifiant
        /// </summary>
        /// <param name="other">Attraction à comparer à l'attraction actuelle</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        public int CompareTo(Attraction other)
        {
            return this.identifiant.CompareTo(other.Identifiant);
        }

        /// <summary>
        /// Tri par Nom
        /// </summary>
        /// <param name="first">Attraction à comparer à la seconde</param>
        /// <param name="other">Attraction à comparer à la première</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        private static int TriParNom(Attraction first, Attraction other)
        {
            return first.Nom.CompareTo(other.Nom);
        }

        /// <summary>
        /// Tri par Nombre Minimum de Monstres
        /// </summary>
        /// <param name="first">Attraction à comparer à la seconde</param>
        /// <param name="other">Attraction à comparer à la première</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        private static int TriParNbMinMonstres(Attraction first, Attraction other)
        {
            return first.NbMinMonstres.CompareTo(other.NbMinMonstres);
        }

        /// <summary>
        /// Tri par Besoin Spécifique
        /// </summary>
        /// <param name="first">Attraction à comparer à la seconde</param>
        /// <param name="other">Attraction à comparer à la première</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        private static int TriParBesoinSpec(Attraction first, Attraction other)
        {
            return first.BesoinSpecifique.CompareTo(other.BesoinSpecifique);
        }

        /// <summary>
        /// Tri par Maintenance
        /// </summary>
        /// <param name="first">Attraction à comparer à la seconde</param>
        /// <param name="other">Attraction à comparer à la première</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        private static int TriParMaintenance(Attraction first, Attraction other)
        {
            return first.Maintenance.CompareTo(other.Maintenance);
        }

        /// <summary>
        /// Tri par Ouverture
        /// </summary>
        /// <param name="first">Attraction à comparer à la seconde</param>
        /// <param name="other">Attraction à comparer à la première</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        private static int TriParOuverture(Attraction first, Attraction other)
        {
            return first.ouvert.CompareTo(other.ouvert);
        }

        #endregion

        #region Méthodes de Tri appelées à l'extérieur

        /// <summary>
        /// Trie une liste d'attractions par Identifiant
        /// </summary>
        /// <param name="liste">Liste d'attractions à trier</param>
        public static void TrierListeAttractionsParId(List<Attraction> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort();
            }
        }

        /// <summary>
        /// Trie une liste d'attractions par Nom
        /// </summary>
        /// <param name="liste">Liste d'attractions à trier</param>
        public static void TrierListeAttractionsParNom(List<Attraction> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort(TriParNom);
            }
        }

        /// <summary>
        /// Trie une liste d'attractions par Nombre minimum de monstres
        /// </summary>
        /// <param name="liste">Liste d'attractions à trier</param>
        public static void TrierListeAttractionsParNbMinMonstres(List<Attraction> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort(TriParNbMinMonstres);
            }
        }

        /// <summary>
        /// Trie une liste d'attractions par Besoin Specifique
        /// </summary>
        /// <param name="liste">Liste d'attractions à trier</param>
        public static void TrierListeAttractionsParBesoinSpecifique(List<Attraction> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort(TriParBesoinSpec);
            }
        }

        /// <summary>
        /// Trie une liste d'attractions par Maintenance
        /// </summary>
        /// <param name="liste">Liste d'attractions à trier</param>
        public static void TrierListeAttractionsParMaintenance(List<Attraction> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort(TriParMaintenance);
            }
        }

        /// <summary>
        /// Trie une liste d'attractions par Ouverture
        /// </summary>
        /// <param name="liste">Liste d'attractions à trier</param>
        public static void TrierListeAttractionsParOuverture(List<Attraction> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort(TriParOuverture);
            }
        }

        #endregion
    }
}
