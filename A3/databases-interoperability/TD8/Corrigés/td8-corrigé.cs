using System;
using System.IO;
using Newtonsoft.Json;

namespace td8
{
    class Programm
    {
        static void Main(string[] args)
        {
            string monFichier = "chiens.json";

            LectureJson(monFichier);
            Console.WriteLine("------------------\nfin de <lectureJson>\n------------------\nappuyez sur une touche");
            Console.ReadKey();
            Console.Clear();
           
            AfficherPrettyJson(monFichier);
            Console.WriteLine("------------------\nfin de <AfficherPrettyJson>\n------------------\nappuyez sur une touche");
            Console.ReadKey();
            Console.Clear();

            EcritureFichierJson();
            Console.WriteLine("------------------\nfin de <EcritureFichierJson>\n------------------\nappuyez sur une touche");
            Console.ReadKey();
            Console.Clear(); Console.ReadKey();

            Console.WriteLine("------------------\nfin d'exécution\n------------------");
            Console.ReadKey();
            Console.Clear();
           
        }

        /// <summary>
        /// exercice 5 :
        /// lecture d'un fichier JSON
        /// </summary>
        /// <param name="nomFichier"></param>
        static void LectureJson(string nomFichier)
        {
            StreamReader reader = new StreamReader(nomFichier);
            JsonTextReader jreader = new JsonTextReader(reader);

            int compteurToken = 1;
            while (jreader.Read())
            {
                Console.Write("Token n°" + (compteurToken++) + " => ");
                //il y a deux sortes de token : avec une valeur associée ou non
                if (jreader.Value != null)
                {
                    Console.WriteLine(jreader.TokenType.ToString() + " : " + jreader.Value);
                }
                else
                {
                    Console.WriteLine(jreader.TokenType.ToString() + " : ");
                }
            }
            jreader.Close();
            reader.Close();
        }

        /// <summary>
        /// exercice 6 : 
        /// lecture du fichier JSON (Pretty affichage)
        /// </summary>
        /// <param name="nomFichier"></param>
        static void AfficherPrettyJson(string nomFichier)
        {
            StreamReader reader = new StreamReader(nomFichier);
            JsonTextReader jreader = new JsonTextReader(reader);
            while (jreader.Read())
            {
                if (jreader.Value != null)
                {
                    if (jreader.TokenType.ToString() == "PropertyName")
                    {
                        Console.Write(jreader.Value + " : ");
                    }
                    else
                    {
                        Console.WriteLine(jreader.Value);
                    }
                }
                else
                {
                    if (jreader.TokenType.ToString() == "StartObject") Console.WriteLine("Nouvel objet\n--------------");
                    if (jreader.TokenType.ToString() == "EndObject") Console.WriteLine("-------------\n");
                    if (jreader.TokenType.ToString() == "StartArray") Console.WriteLine("Liste\n");
                }
            }
            jreader.Close();
            reader.Close();
        }
        /// <summary>
        /// exercice 7: 
        /// ecriture de fichier Json
        /// </summary>
        static void EcritureFichierJson()
        {
            Console.WriteLine("ecriture de chats.json\n-----------------------");
            string monFichier = "chats.json";

            //informations sur les chats
            string[] nom = { "Bambou", "Taz", "Leloo" };
            string[] race = { "europeen", "europeen", "siamois" };
            string[] sexe = { "femelle", "male", "femelle" };
            string[] proprietaire = { "Jules", "Alain", "Luc" };

            //instanciation des "writer"
            StreamWriter writer = new StreamWriter(monFichier);
            JsonTextWriter jwriter = new JsonTextWriter(writer);

            //debut du fichier Json
            jwriter.WriteStartObject();

            //debut du tableau Json
            jwriter.WritePropertyName("chats");
            jwriter.WriteStartArray();
            for (int index = 0; index <= nom.Length - 1; index++)
            {
                Console.Write("traitement du chat n° " + index + "  :  ");
                Console.WriteLine(nom[index] + "  " + race[index] + "  " + sexe[index] + "  " + proprietaire[index]);
                jwriter.WriteStartObject();
                jwriter.WritePropertyName("nom");
                jwriter.WriteValue(nom[index]);
                jwriter.WritePropertyName("race");
                jwriter.WriteValue(race[index]);
                jwriter.WritePropertyName("sexe");
                jwriter.WriteValue(sexe[index]);
                jwriter.WritePropertyName("proprietaire");
                jwriter.WriteValue(proprietaire[index]);
                jwriter.WriteEndObject();
            }
            jwriter.WriteEndArray();
            jwriter.WriteEndObject();

            //femeture de "writer"
            jwriter.Close();
            writer.Close();

            //relecture du fichier créé
            //-----------------------------
            Console.WriteLine("\nlecture des informations de chats.json");
            Console.WriteLine("--------------------------------------\n");
            AfficherPrettyJson("chats.json");
        }

    }
}
