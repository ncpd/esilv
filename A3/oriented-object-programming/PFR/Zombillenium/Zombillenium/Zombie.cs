using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium
{
    enum CouleurZ { bleuatre, grisatre };

    class Zombie : Monstre, IComparable<Zombie>
    {
        private int degreDecomposition;
        private CouleurZ teint;

        #region Getters / Setters

        public int DegreDecomposition
        {
            get
            {
                return degreDecomposition;
            }

            set
            {
                degreDecomposition = value;
            }
        }

        public CouleurZ Teint
        {
            get
            {
                return teint;
            }
        }

        #endregion

        #region Constructeurs

        public Zombie(int matricule, string nom, string prenom, string genre, string fonction, int cagnotte, Attraction affectation, string teintZombie, int decomposition) : base(matricule, nom, prenom, genre, fonction, cagnotte, affectation)
        {
            this.DegreDecomposition = decomposition;
            switch(teintZombie)
            {
                case "bleuatre":
                    teint = CouleurZ.bleuatre;
                    break;
                case "grisatre":
                    teint = CouleurZ.grisatre;
                    break;
                default:
                    break;
            }
        }

        public Zombie(int matricule, string nom, string prenom, string genre, string fonction, int cagnotte, string affectation, string teintZombie, int decomposition) : base(matricule, nom, prenom, genre, fonction, cagnotte, affectation)
        {
            this.DegreDecomposition = decomposition;
            switch (teintZombie)
            {
                case "bleuatre":
                    teint = CouleurZ.bleuatre;
                    break;
                case "grisatre":
                    teint = CouleurZ.grisatre;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            String pronom = getPronomFromSexe();
            String type = (DegreDecomposition == 10) ? "squelette" : "zombie";
            if (Affectation != null)
            {
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un " + type + " qui possède " + Cagnotte + " points dans sa cagnotte et qui est affecté à " + Affectation.Nom + ". "
                    + pronom + " est " + teint.ToString() + " (Degré " + degreDecomposition + "). " + getStringFromFonction();
            }
            else if (AffectationAutre != null)
            {
                string aff = (AffectationAutre == "parc") ? "est affecté au parc" : "ne peut pas être affecté";
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un " + type + " qui possède " + Cagnotte + " points dans sa cagnotte et qui " + aff + ". "
                    + pronom + " est " + teint.ToString() + " (Degré " + degreDecomposition + "). " + getStringFromFonction();
            }
            else
            {
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un " + type + " qui possède " + Cagnotte + " points dans sa cagnotte et qui n'est affecté à aucune attraction. "
                    + pronom + " est " + teint.ToString() + " (Degré " + degreDecomposition + "). " + getStringFromFonction();
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
            stringBuilder.Append("Zombie")
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
            stringBuilder.Append(teint.ToString())
                         .Append(";")
                         .Append(DegreDecomposition)
                         .Append(";");
            return stringBuilder.ToString();
        }

        #endregion

        #region Tris

        /// <summary>
        /// Tri par défaut - par Teint
        /// </summary>
        /// <param name="other">Zombie à comparer</param>
        /// <returns>Entier qui sert à situer si l'ordre dans la liste doit être modifié</returns>
        public int CompareTo(Zombie other)
        {
            return teint.ToString().CompareTo(other.Teint.ToString());
        }

        /// <summary>
        /// Trie une liste de Zombies par leur Teint
        /// </summary>
        /// <param name="liste">Liste de Zombies à trier</param>
        public static void TrierListeZombies(List<Zombie> liste)
        {
            if(liste != null && liste.Count > 0)
            {
                liste.Sort();
            }
        }

        #endregion
    }
}
