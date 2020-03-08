using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium
{
    class Fantome : Monstre, IExportable
    {
        #region Constructeurs

        public Fantome(int matricule, string nom, string prenom, string genre, string fonction, int cagnotte, Attraction affectation) : base(matricule, nom, prenom, genre, fonction, cagnotte, affectation)
        {

        }

        public Fantome(int matricule, string nom, string prenom, string genre, string fonction, int cagnotte, string affectation) : base(matricule, nom, prenom, genre, fonction, cagnotte, affectation)
        {

        }

        #endregion

        #region ToString

        public override string ToString()
        {
            String pronom = getPronomFromSexe();
            if (Affectation != null)
            {
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un fantôme qui possède " + Cagnotte + " points dans sa cagnotte et qui est affecté à " + Affectation.Nom + ". "
                    + getStringFromFonction();
            }
            else if (AffectationAutre != null)
            {
                string aff = (AffectationAutre == "parc") ? "est affecté au parc" : "ne peut pas être affecté";
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un fantôme qui possède " + Cagnotte + " points dans sa cagnotte et qui " + aff + ". "
                    + getStringFromFonction();
            }
            else
            {
                return Prenom + " " + Nom + " (n°" + Matricule + ") est un fantôme qui possède " + Cagnotte + " points dans sa cagnotte et qui n'est affecté à aucune attraction. "
                    + getStringFromFonction();
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
            stringBuilder.Append("Fantome")
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
    }
}
