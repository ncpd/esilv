using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PFR
{
    class Query
    {
        private MySqlConnection connection;
        private string connectionString = "SERVER = fboisson.ddns.net ; PORT =3306; DATABASE = pica_nico ; UID = S6-PICA-NICO ;PASSWORD = 8430";
        private string queryResult;
        private bool success;

        #region Accesseurs

        public string QueryResult { get => queryResult; set => queryResult = value; }
        public bool Success { get => success; set => success = value; }

        #endregion

        #region Constructeur

        public Query()
        {
            connection = new MySqlConnection(connectionString);
            queryResult = "";
            success = false;
        }

        #endregion

        /// <summary>
        /// Méthode qui réalise un SELECT depuis une MySQLCommand
        /// </summary>
        /// <param name="cmd">Commande à éxécuter</param>
        public void Select(MySqlCommand cmd)
        {
            try
            {
                connection.Open();

                if (cmd.CommandText != null && cmd.CommandText != "")
                {

                    cmd.Connection = connection;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    StringBuilder stringBuilder = new StringBuilder();
                    while (reader.Read()) // parcours ligne par ligne
                    {
                        for (int i = 0; i < reader.FieldCount; i++) // parcours cellule par cellule
                        {
                            string valueAsString = reader.GetValue(i).ToString();
                            stringBuilder.Append(valueAsString).Append("-");
                        }
                        stringBuilder.Append("\n");
                    }
                    QueryResult = stringBuilder.ToString();

                }

                connection.Close();

                success = true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message.ToString());
                success = false;
            }
        }

        /// <summary>
        /// Méthode qui réalise un INSERT depuis une MySQLCommand
        /// </summary>
        /// <param name="cmd">Commande à éxécuter</param>
        public void Insert(MySqlCommand cmd)
        {
            try
            {
                connection.Open();

                bool successful = false;

                if (cmd.CommandText != null && cmd.CommandText != "")
                {

                    cmd.Connection = connection;

                    int rows = cmd.ExecuteNonQuery();

                    successful = (rows > 0); // Réussite de la requête si rows > 0
                }

                connection.Close();

                success = successful;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message.ToString());
                success = false;
            }
        }

        /// <summary>
        /// Méthode qui réalise un UPDATE depuis une MySQLCommand
        /// </summary>
        /// <param name="cmd">Commande à éxécuter</param>
        public void Update(MySqlCommand cmd)
        {
            try
            {
                connection.Open();

                bool successful = false;

                if (cmd.CommandText != null && cmd.CommandText != "")
                {

                    cmd.Connection = connection;

                    int rows = cmd.ExecuteNonQuery();

                    successful = (rows > 0); // Réussite de la requête si rows > 0
                }

                connection.Close();

                success = successful;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
                success = false;
            }
        }
    }
}
