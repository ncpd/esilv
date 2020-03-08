using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium_WPF
{
    enum TypeBoutique { barbeAPapa, nourriture, souvenir };

    class Boutique : Attraction, IExportable, IComparable<Boutique>
    {
        private TypeBoutique typeBoutique;
        private string typeBoutiqueStr;

        #region Getter

        internal TypeBoutique TypeBoutique
        {
            get
            {
                return typeBoutique;
            }
        }

        public string TypeBoutiqueStr
        {
            get
            {
                switch(TypeBoutique)
                {
                    case TypeBoutique.barbeAPapa:
                        return "Barbe à Papa";
                    case TypeBoutique.nourriture:
                        return "Nourriture";
                    case TypeBoutique.souvenir:
                        return "Souvenirs";
                    default:
                        return "";
                }
            }
            set => typeBoutiqueStr = value;
        }

        #endregion

        #region Constructeur

        public Boutique(int id, string nom, int nbMinMonstres, bool besoinSpec, string typeBesoin, string typeBoutique) : base(id, nom, nbMinMonstres, besoinSpec, typeBesoin)
        {
            switch (typeBoutique)
            {
                case "souvenir":
                    this.typeBoutique = TypeBoutique.souvenir;
                    break;
                case "barbeAPapa":
                    this.typeBoutique = TypeBoutique.barbeAPapa;
                    break;
                case "nourriture":
                    this.typeBoutique = TypeBoutique.nourriture;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            String items = "";
            switch (TypeBoutique)
            {
                case TypeBoutique.souvenir:
                    items = " vend des souvenirs";
                    break;
                case TypeBoutique.barbeAPapa:
                    items = " vend des barbes à papa";
                    break;
                case TypeBoutique.nourriture:
                    items = " vend de la nourriture";
                    break;
                default:
                    items = " ne vend rien";
                    break;
            }
            string monster = (NbMinMonstres > 1) ? "monstres" : "monstre";
            return Nom + " (id n°" + Identifiant + ") est une boutique qui" + items + ". Elle a besoin de " + NbMinMonstres + " " + monster + " pour fonctionner. "
                    + getStringFromStatus() + getStringFromBesoin() + getStringFromEquipe();
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
            stringBuilder.Append("Boutique")
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
                         .Append(TypeBoutique.ToString())
                         .Append(";");
            return stringBuilder.ToString();
        }

        #endregion

        #region Tris

        /// <summary>
        /// Tri par défaut - par type de boutique
        /// </summary>
        /// <param name="other">Boutique à comparer à la boutique actuelle</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        public int CompareTo(Boutique other)
        {
            return typeBoutique.CompareTo(other.TypeBoutique);
        }

        /// <summary>
        /// Trie une liste de Boutiques par Type de boutique
        /// </summary>
        /// <param name="liste">Liste de Boutiques à trier</param>
        public static void TrierListeBoutiquesParType(List<Boutique> liste)
        {
            if (liste != null && liste.Count > 0)
            {
                liste.Sort();
            }
        }

        #endregion

        #region Filtrage

        /// <summary>
        /// Filtre une liste de boutiques par Besoin Spécifique (booléen)
        /// </summary>
        /// <param name="boutiques">Liste de boutiques à filtrer</param>
        /// <param name="critere">Critere de tri</param>
        /// <returns>Liste de boutiques ordonnée</returns>
        public static List<Boutique> FiltrerBoutiquesParBesoinSpec(List<Boutique> boutiques, string critere)
        {
            switch (critere)
            {
                case "besoinSpec":
                    return boutiques.Where(b => b.BesoinSpecifique).ToList();
                default:
                    return boutiques;
            }
        }

        /// <summary>
        /// Filtre une liste de boutiques par Nb Min Monstres
        /// </summary>
        /// <param name="boutiques">Liste de boutiques à filtrer</param>
        /// <param name="critere">Critere de tri</param>
        /// <param name="operateur">Opérateur de comparaison</param>
        /// <param name="value">Valeur à utiliser lors du filtrage</param>
        /// <returns></returns>
        public static List<Boutique> FiltrerBoutiquesParNbMinMonstres(List<Boutique> boutiques, string critere, string operateur, int value)
        {
            switch (critere)
            {
                case "nbMinMonstres":
                    switch (operateur)
                    {
                        case ">":
                            return boutiques.Where(b => b.NbMinMonstres > value).ToList();
                        case ">=":
                            return boutiques.Where(b => b.NbMinMonstres >= value).ToList();
                        case "<":
                            return boutiques.Where(b => b.NbMinMonstres < value).ToList();
                        case "<=":
                            return boutiques.Where(b => b.NbMinMonstres <= value).ToList();
                        case "=":
                            return boutiques.Where(b => b.NbMinMonstres == value).ToList();
                        default:
                            break;
                    }
                    return boutiques;
                default:
                    return boutiques;
            }
        }

        /// <summary>
        /// Filtre une liste de boutiques par Type ou par Besoin
        /// </summary>
        /// <param name="boutiques">Liste de boutiques à filtrer</param>
        /// <param name="critere">Critere de tri</param>
        /// <param name="value">Valeur à utiliser lors du filtrage</param>
        /// <returns></returns>
        public static List<Boutique> FiltrerBoutiquesParTypeOuBesoin(List<Boutique> boutiques, string critere, string value)
        {
            switch (critere)
            {
                case "typeBesoin":
                    return boutiques.Where(b => b.TypeDeBesoin.Equals(value)).ToList();
                case "typeBoutique":
                    if (value.Equals("barbeAPapa"))
                    {
                        return boutiques.Where(b => b.TypeBoutique == TypeBoutique.barbeAPapa).ToList();
                    }
                    else if (value.Equals("nourriture"))
                    {
                        return boutiques.Where(b => b.TypeBoutique == TypeBoutique.nourriture).ToList();
                    }
                    else if (value.Equals("souvenir"))
                    {
                        return boutiques.Where(b => b.TypeBoutique == TypeBoutique.souvenir).ToList();
                    }
                    else
                    {
                        return boutiques;
                    }
                default:
                    return boutiques;
            }
        }

        #endregion
    }
}
