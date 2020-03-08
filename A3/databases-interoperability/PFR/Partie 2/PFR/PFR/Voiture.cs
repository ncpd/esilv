using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PFR
{
    class Voiture
    {
        private string immatriculation;
        private string marque;
        private string modele;
        private string categorie;
        private int nbPlaces;
        private string nomParking;
        private string placeParking;
        private bool doitEtreNettoyee;
        private string idControleur;
        private int prixJ;

        #region Accesseurs

        public string Immatriculation { get => immatriculation; set => immatriculation = value; }
        public string NomParking { get => nomParking; set => nomParking = value; }
        public string PlaceParking { get => placeParking; set => placeParking = value; }
        public bool DoitEtreNettoyee { get => doitEtreNettoyee; set => doitEtreNettoyee = value; }
        public int PrixJ { get => prixJ; set => prixJ = value; }

        #endregion

        #region Constructeurs

        public Voiture(string immatriculation, string marque, string modele, string idControleur, string categorie, int nbPlaces, string nomParking, string placeParking, int prixJ)
        {
            this.immatriculation = immatriculation;
            this.marque = marque;
            this.modele = modele;
            this.categorie = categorie;
            this.nbPlaces = nbPlaces;
            this.nomParking = nomParking;
            this.placeParking = placeParking;
            this.idControleur = idControleur;
            this.prixJ = prixJ;
        }

        public Voiture(string immatriculation, string idControleur, bool doitEtreNettoyee)
        {
            this.immatriculation = immatriculation;
            this.idControleur = idControleur;
            this.doitEtreNettoyee = doitEtreNettoyee;
        }

        #endregion

        public override string ToString()
        {
            return marque + " " + modele + ", " + categorie + " " + nbPlaces + " places, immattriculée " + immatriculation + " (" + prixJ + " euros par jour)";
        }

        /// <summary>
        /// Contrôle une voiture
        /// </summary>
        /// <returns>True si la voiture a été controlée</returns>
        public bool Controler()
        {
            if(doitEtreNettoyee)
            {
                // Passage indispo + nettoyage
                MettreIndisponible("Nettoyage");
                Intervention("Nettoyage intérieur");
                Console.WriteLine("Nettoyage en cours ...");
                MettreDisponible();
                return true;
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// Crée une intervention en BDD avec le motif indiqué
        /// </summary>
        /// <param name="typeIntervention">Motif de l'intervention</param>
        private void Intervention(string typeIntervention)
        {
            doitEtreNettoyee = false;
            bool success = false;
            MySqlCommand commande = new MySqlCommand();
            commande.Parameters.AddWithValue("@InterId", GetNextInterventionId());
            commande.Parameters.AddWithValue("@Immat", immatriculation);
            commande.Parameters.AddWithValue("@IdControleur", idControleur);
            commande.Parameters.AddWithValue("@TypeInter", typeIntervention);
            commande.Parameters.AddWithValue("@Date", DateTime.Now.ToString("yyyy-MM-dd"));

            commande.CommandText = "INSERT INTO intervention VALUES (@InterId, @Immat, @IdControleur, @TypeInter, @Date);";

            Query query = new Query();
            query.Update(commande);
            success = (query.Success);
            query = null;
        }

        /// <summary>
        /// Crée l'id d'intervention suivant
        /// Exemple : la dernière intervention était I001, alors la nouvelle aura pour id I002
        /// </summary>
        /// <returns>Id de la nouvelle intervention</returns>
        private string GetNextInterventionId()
        {
            MySqlCommand commande = new MySqlCommand();
            commande.CommandText = "SELECT id FROM intervention ORDER BY id DESC LIMIT 1;";

            Query query = new Query();
            query.Select(commande);
            string result = query.QueryResult;
            int nbId = 0;

            if (result != "")
            {
                String[] fSplitted = result.Split('-');
                String[] fSplitted2 = fSplitted[0].Split('I');
                Int32.TryParse(fSplitted2[1], out nbId);
            }
            result = "I" + (nbId + 1).ToString("000");
            //Console.WriteLine(result);
            query = null;
            return result;
        }

        /// <summary>
        /// Mise indisponible de la voiture en BDD avec le motif indiqué
        /// </summary>
        /// <param name="motif">Motif d'indisponibilité</param>
        private void MettreIndisponible(string motif)
        {
            bool success = false;
            MySqlCommand commande = new MySqlCommand();
            commande.Parameters.AddWithValue("@Motif", motif);
            commande.Parameters.AddWithValue("@Immat", this.immatriculation);
            commande.CommandText = "UPDATE voiture SET disponibilite = 0, motif_indisponibilite = @Motif WHERE immat = @Immat;";

            Query query = new Query();
            query.Update(commande);
            success = (query.Success);
            query = null;
        }

        /// <summary>
        /// Mise disponible de la voiture en BDD
        /// </summary>
        private void MettreDisponible()
        {
            bool success = false;
            MySqlCommand commande = new MySqlCommand();
            commande.Parameters.AddWithValue("@Immat", this.immatriculation);
            commande.CommandText = "UPDATE voiture SET disponibilite = 1, motif_indisponibilite = NULL WHERE immat = @Immat;";

            Query query = new Query();
            query.Update(commande);
            success = (query.Success);
            query = null;
        }
    }
}
