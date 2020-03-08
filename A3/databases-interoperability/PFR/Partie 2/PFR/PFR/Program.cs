using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Xml;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using JsonPath;
using System.Xml.Linq;

namespace PFR
{
    class Program
    {
        static void Main(string[] args)
        {
            Escapade escapade = new Escapade();
            System.Diagnostics.Process.Start("https://escapade-dashboard.herokuapp.com/");
            /* ETAPE 1 : RESERVATION D'UN SEJOUR */

            Etape1Ascii();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========== La demande de séjour (M1 => m1.xml) est envoyée à ce moment ==========");
            Console.ForegroundColor = ConsoleColor.White;
            AfficheContenuXML("m1.xml");
            escapade.DemandeSejour("m1.xml");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();

            /* ETAPE 2 : CHECKOUT CLIENT */

            Console.Clear();
            Etape2Ascii();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Entrez votre numéro de location :");
            Console.ForegroundColor = ConsoleColor.White;
            string noLoc = Console.ReadLine();


            // COMMENTAIRE, NOTE, ID ET PLACE DU PARKING INDIQUES PAR LE CLIENT
            escapade.CheckOut(noLoc, "P22", "A5", 4, "Génial le déploiement sur Heroku !");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();
        }

        /// <summary>
        /// Réalise un affichage header pour l'étape 2
        /// </summary>
        static void Etape2Ascii()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\t******************************************************");
            Console.WriteLine("\t*                                                    *");
            Console.WriteLine("\t*                 ETAPE 2 : CHECKOUT                 *");
            Console.WriteLine("\t*                                                    *");
            Console.WriteLine("\t******************************************************\n\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Affiche le contenu XML d'un fichier
        /// </summary>
        /// <param name="fileName">Fichier XML</param>
        public static void AfficheContenuXML(string fileName)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Contenu du fichier " + fileName + ":\n");
            Console.ForegroundColor = ConsoleColor.White;

            
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            StringWriter string_writer = new StringWriter();
            XmlTextWriter xml_text_writer = new XmlTextWriter(string_writer);
            xml_text_writer.Formatting = System.Xml.Formatting.Indented;
            doc.WriteTo(xml_text_writer);

            Console.WriteLine(string_writer.ToString());
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Réalise un affichage header pour l'étape 1
        /// </summary>
        static void Etape1Ascii()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\t******************************************************");
            Console.WriteLine("\t*                                                    *");
            Console.WriteLine("\t*                 ETAPE 1 : RESERVATION              *");
            Console.WriteLine("\t*                                                    *");
            Console.WriteLine("\t******************************************************\n\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
