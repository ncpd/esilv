using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Exo1();
            Console.ReadKey();
        }
        static void Exo1()
        {
            string nomDuDocXML = "bdtheque.xml";
            
            //cr�er l'arborescence des chemins XPath du document
            //--------------------------------------------------
            XPathDocument doc = new System.Xml.XPath.XPathDocument(nomDuDocXML);
            XPathNavigator nav = doc.CreateNavigator();

            //cr�er une requete XPath
            //-----------------------
            string maRequeteXPath = "requete XPath ici";
            XPathExpression expr = nav.Compile(maRequeteXPath);

            //ex�cution de la requete
            //-----------------------
            XPathNodeIterator nodes = nav.Select(expr);// ex�cution de la requ�te XPath

            //parcourir le resultat
            //---------------------

            while (nodes.MoveNext()) // pour chaque r�ponses XPath (on est au niveau d'un noeud BD)
            {

			// a compl�ter

            }

            expr = nav.Compile(maRequeteXpath);





        }




    }//fin class programm
}//fin namespace
