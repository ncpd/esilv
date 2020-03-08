using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace TD7
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "bdtheque.xml";
            //Exo1Xpath(fileName);
            //Exo1Correction(fileName);
            //Exo2XML();
            //Exo3XML();
            //Exo4();
            //Exo5();
            //Exo6();
            Exo7();
            Console.ReadKey();
        }

        static void Exo1Xpath(string fileName)
        {
            XPathDocument doc = new XPathDocument(fileName);
            XPathNavigator navi = doc.CreateNavigator();

            XPathNodeIterator xPathIterator = navi.Select("/bdtheque/BD");
            foreach (XPathNavigator bd in xPathIterator)
            {
                string isbn = bd.GetAttribute("isbn", "");
                XPathNavigator nav = bd.SelectSingleNode("titre");
                string titre = nav == null ? string.Empty : nav.Value;
                nav = bd.SelectSingleNode("auteur");
                string authorFirstName = nav == null ? string.Empty : nav.Value;
                nav = bd.SelectSingleNode("nombre_pages");
                string pages = nav == null ? string.Empty : nav.Value;
                string nb_pages = "";
                if(pages != "" && pages != null)
                {
                    nb_pages = " (" + pages + " pages)";
                }
                Console.WriteLine(titre + nb_pages + ", écrit par " + authorFirstName + ", numéro ISBN :" + isbn);
            }
        }

        static void Exo1Correction(string fileName)
        {
            XPathDocument doc = new XPathDocument(fileName);
            XPathNavigator nav = doc.CreateNavigator();
            XPathExpression expr = nav.Compile("bdtheque/BD");

            XPathNodeIterator nodes = nav.Select(expr);// exécution de la requête XPath
            while (nodes.MoveNext()) // pour chaque réponses XPath (on est au niveau d'un noeud BD)
            {
                string isbn = nodes.Current.GetAttribute("isbn", "");
                nodes.Current.MoveToFirstChild();
                string titre = nodes.Current.Value;
                nodes.Current.MoveToNext();
                string auteur = nodes.Current.Value;
                bool nbPagesExiste = nodes.Current.MoveToNext();
                string nbPages = "";
                if(nbPagesExiste)
                {
                    nbPages = " (" + nodes.Current.Value + " pages)";
                }
                Console.WriteLine(titre + nbPages + ", écrit par " + auteur + ", numéro ISBN :" + isbn);
            }
        }

        static void Exo2XML()
        {
            XmlDocument docXml = new XmlDocument();

            // création de l'en-tête XML (no <=> pas de DTD associée)
            docXml.CreateXmlDeclaration("1.0", "UTF-8", "no");

            XmlElement racine = docXml.CreateElement("BD");
            racine.SetAttribute("isbn", "978-2203001169");
            docXml.AppendChild(racine);

            XmlElement titre = docXml.CreateElement("titre");
            titre.InnerText = "On a marché sur la lune";
            racine.AppendChild(titre);

            XmlElement auteur = docXml.CreateElement("auteur");
            auteur.InnerText = "Hergé";
            racine.AppendChild(auteur);

            XmlElement nb_pages = docXml.CreateElement("nombre_pages");
            nb_pages.InnerText = "62";
            racine.AppendChild(nb_pages);

            // enregistrement du document XML   ==> à retrouver dans le dossier bin\Debug de Visual Studio
            docXml.Save("bd1.xml");
        }

        static void Exo3XML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("bd1.xml");
            int i = 0;
            StringBuilder tostr = new StringBuilder();
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                string text = node.InnerText; //or loop through its children as well
                switch (i)
                {
                    case 0:
                        tostr.Append(text);
                        break;
                    case 1:
                        tostr.Append(", écrit par " + text);
                        break;
                    case 2:
                        if(text != "")
                        {
                            tostr.Append(" (" + text + " pages)");
                        }
                        break;
                }
                i++;
            }
            tostr.Append(", ISBN : " + doc.DocumentElement.GetAttribute("isbn").ToString());
            Console.WriteLine(tostr);
        }

        static void Exo4()
        {
            BD bd11 = new BD("978-2203001169", "On a marché sur la Lune", 62);
            Console.WriteLine(bd11);  // affichage pour débug

            // Code pour sérialiser l'objet bd11 en XML dans un fichier "bd11.xml"
            XmlSerializer xs = new XmlSerializer(typeof(BD));  // l'outil de sérialisation
            StreamWriter wr = new StreamWriter("bd11.xml");  // accès en écriture à un fichier (texte)
            xs.Serialize(wr, bd11); // action de sérialiser en XML l'objet bd11 
                                    // et d'écrire le résultat dans le fichier manipulé par wr
            wr.Close();

            // vérifier le contenu du fichier "bd11.xml" dans le dossier bin\Debug de Visual Studio.
        }

        public static void Exo5()
        {
            BD bd11 = null;

            // Désérialisation...
            XmlSerializer xs = new XmlSerializer(typeof(BD));
            StreamReader rd = new StreamReader("bd11.xml");
            bd11 = xs.Deserialize(rd) as BD;
            rd.Close();

            // Bilan :
            Console.WriteLine(bd11); // affichage pour contrôler le contenu de l'objet bd11
        }

        public static void Exo6()
        {
            Artiste auteur = new Artiste("Remi", "Georges", "Hergé");
            Console.WriteLine(auteur);  // affichage pour débug

            BandeDessinee bd12 = new BandeDessinee("978-2203001169", "On a marché sur la Lune", auteur, 62);
            Console.WriteLine(bd12);  // affichage pour débug

            // Sérialisation...
            XmlSerializer xs = new XmlSerializer(typeof(BandeDessinee));
            StreamWriter wr = new StreamWriter("bd12.xml");
            xs.Serialize(wr, bd12);
            wr.Close();

            // vérifier le contenu du fichier "bd12.xml" dans le dossier bin\Debug de Visual Studio.
        }

        public static void Exo7()
        {
            Artiste herge = new Artiste("Remi", "Georges", "Hergé");
            BandeDessinee bd1 = new BandeDessinee("978-2203001169", "On a marché sur la Lune", herge, 62);

            BDtheque bdtheque = new BDtheque();
            bdtheque.Ajouter(bd1);
            bdtheque.Ajouter(new BandeDessinee("978-2203001039", "Les Cigares du pharaon", herge));
            bdtheque.Ajouter(new BandeDessinee("978-2012101371", "Le tour de Gaule d'Astérix", new Artiste("Goscinny", "René"), 48));
            Console.WriteLine(bdtheque);    // affichage pour débug

            // Sérialisation...
            XmlSerializer xs = new XmlSerializer(typeof(BDtheque));
            StreamWriter wr = new StreamWriter("ma_bdtheque.xml");
            xs.Serialize(wr, bdtheque);
            wr.Close();
        }
    }
}
