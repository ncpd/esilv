using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PFR
{
    class Sejour
    {
        private string idSejour;
        private int theme; // ie arrondissement
        private int date; // ie no semaine
        private int annee;

        #region Accesseurs

        public string IdSejour { get => idSejour; set => idSejour = value; }
        public int Theme { get => theme; set => theme = value; }
        public int Date { get => date; set => date = value; }

        #endregion

        #region Constructeur

        public Sejour(int arrondissement, int semaine)
        {
            theme = arrondissement;
            date = semaine;
            annee = DateTime.Now.Year;
            idSejour = GetSejourId(arrondissement, semaine);
        }

        #endregion

        /// <summary>
        /// Récupère l'id d'un séjour si il est existant
        /// </summary>
        /// <param name="arrondissement">Arrondissement concerné</param>
        /// <param name="semaine">Semaine concernée</param>
        /// <returns>Id du séjour si il existe</returns>
        private string GetSejourId(int arrondissement, int semaine)
        {
            MySqlCommand commande = new MySqlCommand();

            commande.Parameters.AddWithValue("@Arrondissement", arrondissement);
            commande.Parameters.AddWithValue("@Semaine", semaine);
            commande.Parameters.AddWithValue("@Annee", annee);
            commande.CommandText = "SELECT id FROM sejour WHERE theme = @Arrondissement AND date = @Semaine AND annee = @Annee;";

            Query query = new Query();
            query.Select(commande);

            string result = query.QueryResult;
            if(result != "")
            {
                query = null;
                String[] splitted = result.Split('-');
                return splitted[0];
            } else
            {
                // On crée le séjour
                query = null;
                string nextId = CreateNextId();
                CreateSejour(nextId, arrondissement, semaine, annee);
                return nextId;
            }
        }

        /// <summary>
        /// Crée un séjour en BDD
        /// </summary>
        /// <param name="id">Identifiant du séjour</param>
        /// <param name="arrondissement">Arrondissement</param>
        /// <param name="semaine">Semaine</param>
        /// <param name="annee">Année</param>
        /// <returns>True si le séjour a bien été crée en BDD</returns>
        private bool CreateSejour(string id, int arrondissement, int semaine, int annee)
        {
            MySqlCommand commande = new MySqlCommand();
            // Empêche l'injection SQL 
            commande.Parameters.AddWithValue("@Id", id);
            commande.Parameters.AddWithValue("@Arrondissement", arrondissement);
            commande.Parameters.AddWithValue("@Semaine", semaine);
            commande.Parameters.AddWithValue("@Annee", annee);
            commande.CommandText = "INSERT INTO sejour VALUES (@Id, @Arrondissement, @Semaine, @Annee);";

            Query query = new Query();
            query.Insert(commande);
            bool success = query.Success;
            query = null;

            return success;
        }

        /// <summary>
        /// Crée l'id de séjour suivant
        /// Exemple : le dernier séjour était S001, alors le nouveau aura pour id S002
        /// </summary>
        /// <returns></returns>
        private string CreateNextId()
        {
            MySqlCommand commande = new MySqlCommand();
            commande.CommandText = "SELECT id FROM sejour ORDER BY id DESC LIMIT 1;";

            Query query = new Query();
            query.Select(commande);
            string result = query.QueryResult;
            int nbId = 0;

            if (result != "")
            {
                String[] fSplitted = result.Split('-');
                String[] fSplitted2 = fSplitted[0].Split('S');
                Int32.TryParse(fSplitted2[1], out nbId);
            }
            result = "S" + (nbId + 1).ToString("000");
            //Console.WriteLine(result);
            query = null;
            return result;
        }
        
    }
}
