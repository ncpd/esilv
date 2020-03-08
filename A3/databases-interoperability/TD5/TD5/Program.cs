using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TD5
{
    class Program
    {

        static void Exo1_2()
        {
            queryDatabase("SELECT marque FROM voiture;");
            queryDatabase("SELECT pseudo, marque, modele, immat FROM proprietaire p INNER JOIN voiture v on p.codeP = v.codeP;");
        }

        static void Exo3()
        {
            ParserExo3(queryDatabaseToString("SELECT marque, modele, prixJ FROM voiture NATURAL JOIN location WHERE location.codeC = 'C654';"));
        }

        static void Exo4()
        {
            ParserExo4(queryDatabaseToString("SELECT AVG(prixJ) FROM voiture;"));
        }

        static void Exo5()
        {
            ParserExo5(queryDatabaseToString("SELECT MAX(prixJ), MIN(prixJ) FROM voiture;"));
        }

        static void ParserExo5(string result)
        {
            string[] lines = result.Split('\n');
            foreach (string line in lines)
            {
                string[] splitted = line.Split('-');
                for (int i = 0; i < splitted.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (splitted[0] != null && splitted[0] != "")
                            {
                                Console.Write("Prix maximum à la journée : " + splitted[0]);
                            }
                            break;
                        case 1:
                            if (splitted[1] != null && splitted[1] != "")
                            {
                                Console.Write("Prix minimum à la journée : " + splitted[1]);
                            }
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine();
                }
            }
        }

        static void ParserExo4(string result)
        {
            string[] lines = result.Split('\n');
            foreach (string line in lines)
            {
                string[] splitted = line.Split('-');
                for (int i = 0; i < splitted.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if(splitted[0] != null && splitted[0] != "")
                            {
                                Console.Write("Prix moyen à la journée :" + splitted[0]);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        static void ParserExo3(string result)
        {
            string[] lines = result.Split('\n');
            foreach(string line in lines)
            {
                string[] splitted = line.Split('-');
                for(int i = 0; i < splitted.Length; i++)
                {
                    switch(i)
                    {
                        case 0:
                            Console.Write("Marque :" + splitted[0] + " | ");
                            break;
                        case 1:
                            Console.Write("Modèle :" + splitted[1] + " | ");
                            break;
                        case 2:
                            Console.Write("Prix à la journée :" + splitted[2]);
                            break;
                    }
                }
            }
        }

        static void queryDatabase(string query)
        {
            string connectionString = " SERVER = localhost ; PORT =3306; DATABASE = loueur ; UID = root ;PASSWORD = mysql";            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query; // exemple de requete bien - sur !

            MySqlDataReader reader;
            reader = command.ExecuteReader();            while (reader.Read()) // parcours ligne par ligne
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++) // parcours cellule par cellule
                {
                    string valueAsString = reader.GetValue(i).ToString(); // recuperation de la valeur de chaque cellule sous forme d' une string ( voir cependant les differentes methodes disponibles !!)
                    currentRowAsString += valueAsString + ", ";
                }
                Console.WriteLine(currentRowAsString); // affichage de la ligne ( sous forme d'une " grosse " string ) sur la sortie standard
            }
            connection.Close();
        }

        static string queryDatabaseToString(string query)
        {
            string connectionString = " SERVER = localhost ; PORT =3306; DATABASE = loueur ; UID = root ;PASSWORD = mysql";            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query; // exemple de requete bien - sur !

            MySqlDataReader reader;
            reader = command.ExecuteReader();            StringBuilder stringBuilder = new StringBuilder();            while (reader.Read()) // parcours ligne par ligne
            {
                for (int i = 0; i < reader.FieldCount; i++) // parcours cellule par cellule
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    stringBuilder.Append(valueAsString).Append("-");
                }
                stringBuilder.Append("\n");
            }
            connection.Close();
            return stringBuilder.ToString();
        }

        static void Main(string[] args)
        {
            Exo5();
            Console.ReadKey();
        }

    }
}