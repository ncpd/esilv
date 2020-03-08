using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD3
{
    class Program
    {
        static void AfficherListe(List<Document> l)
        {
            Console.WriteLine("Bibliothèque :");
            foreach(Document d in l)
            {
                Console.Write(d.Titre + "\n");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Document d = new Document("Docu", 1);
            Dictionnaire dico = new Dictionnaire("Larousse", 2, "français");
            Livre l = new Livre("Jean BON", 156, "Les misérables", 3);
            Manuel m = new Manuel("Jean EUDE", 162, "Zadar", 4, 3);
            Revue r = new Revue("Sciences et Vie Jr.", 4, "Avril", 2002);
            Roman rom = new Roman("Jean TROIE", 140, "La vie en rose", 5, "Fields");
            //Console.WriteLine(o.ToString());
            //Console.WriteLine(o2.ToString());
            //Console.WriteLine(r.ToString());

            Etagere e = new Etagere();
            e.ajouterOuvrage(d);
            e.ajouterOuvrage(dico);
            e.ajouterOuvrage(l);
            e.ajouterOuvrage(m);
            e.ajouterOuvrage(r);
            e.ajouterOuvrage(rom);

            e.listerOuvrages();

            Document ouv = e.chercherOuvrageParTitre("Larousse");
            if(ouv != null)
            {
                Console.WriteLine(ouv.ToString());
            } else
            {
                Console.WriteLine("L'ouvrage n'a pas été trouvé");
            }

            Document df = e.chercherOuvrageParNo(3);
            if (df != null)
            {
                Console.WriteLine(df.ToString());
            }
            else
            {
                Console.WriteLine("L'ouvrage n'a pas été trouvé");
            }

            Console.ReadKey();
        }
    }
}
