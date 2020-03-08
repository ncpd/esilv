using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PFR
{
    class User
    {
        private string nom;
        private string prenom;
        private string id;

        #region Constructeur

        public User(string nom, string prenom, string id)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.id = id;
        }

        public User(string nom, string prenom)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.id = CreateNextId();
        }

        public User(string nom)
        {
            this.nom = nom;
            this.prenom = "";
            this.id = CreateNextId();
        }

        #endregion

        #region Accesseurs

        public string Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }

        #endregion

        /// <summary>
        /// Ajoute l'utilisateur actuel à la BDD
        /// </summary>
        /// <returns>True si l'utilisateur a bien été ajouté</returns>
        public bool AddToDataBase()
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Parameters.AddWithValue("@CodeClient", id);
            commande.Parameters.AddWithValue("@NomClient", nom);
            commande.CommandText = "INSERT INTO client VALUES (@CodeClient, @NomClient, NULL, NULL, NULL, NULL);";

            Query query = new Query();
            query.Insert(commande);
            bool success = query.Success;
            query = null;

            return success;
        }

        /// <summary>
        /// Crée l'id de client suivant
        /// Exemple : le dernier client était C001, alors le nouveau aura pour code client C002
        /// </summary>
        /// <returns>Nouveau code client</returns>
        private string CreateNextId()
        {
            MySqlCommand commande = new MySqlCommand();
            commande.CommandText = "SELECT codeC FROM client ORDER BY codeC DESC LIMIT 1;";

            Query query = new Query();
            query.Select(commande);
            string result = query.QueryResult;
            int nbId = 0;

            if (result != "")
            {
                String[] fSplitted = result.Split('-');
                String[] fSplitted2 = fSplitted[0].Split('C');
                Int32.TryParse(fSplitted2[1], out nbId);
            }
            result = "C" + (nbId + 1).ToString("000");
            //Console.WriteLine(result);
            query = null;
            return result;
        }

        public override string ToString()
        {
            return id + " : " + nom + " " + prenom; 
        }
    }
}
