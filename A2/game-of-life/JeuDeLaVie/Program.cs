using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JeuDeLaVie
{
    class Program
    {
        static void Main(string[] args)
        {
            int niveau;
            int taille = 6; // Données à modifier selon bon vouloir
            int ageinitial = 0;
            int energieinitiale = 1;
            string file = "MatriceConstructeur.txt";
            string messageerreur = "Vous avez mal initialisé la grille !";
            do {
                Console.WriteLine("Choisissez de jouer avec le niveau 1 ou 2 du jeu de la vie :\n\n1/ Niveau 1\n2/ Niveau 2\n3/ Initialisation avec fichier\n\n");
                niveau = int.Parse(Console.ReadLine());
                Console.Clear();
            }
            while (niveau != 1 && niveau != 2 && niveau != 3);
            if (niveau == 1)
            {
                Console.WriteLine("Entrez un pourcentage :");
                int pourcentage = int.Parse(Console.ReadLine());
                if (pourcentage > 0 && pourcentage < 100)
                {
                    Grille torique = new Grille(taille, pourcentage);
                    Console.WriteLine("Entrez un nombre de générations à réaliser :");
                    int nbgenerations = int.Parse(Console.ReadLine());
                    if (nbgenerations > 0 && nbgenerations <= 99)
                    {
                        Console.Clear();
                        for (int i = 0; i < nbgenerations; i++) // Création du nombre voulu de générations
                        {
                            Console.WriteLine("Génération " + (i + 1));
                            torique.Parcours(taille);
                            Console.WriteLine("Appuyez sur une touche pour continuer...");
                            Console.WriteLine();
                            Console.ReadKey();
                        }
                        torique.EcritureGrille("GrilleFinale.txt"); // Ecriture de la dernière génération dans un fichier "GrilleFinale.txt" dans Debug
                    }
                    else Console.WriteLine(messageerreur);
                }
                else Console.WriteLine(messageerreur);
            }
            if(niveau == 2)
            {
                Console.WriteLine("Entrez un pourcentage :");
                int pourcentage = int.Parse(Console.ReadLine());
                if (pourcentage > 0 && pourcentage < 100)
                {
                    Grille toriquelv2 = new Grille(taille, pourcentage, "l", ageinitial, energieinitiale);
                    Console.WriteLine("Entrez un nombre de générations à réaliser :");
                    int nbgenerations = int.Parse(Console.ReadLine());
                    if (nbgenerations > 0 && nbgenerations <= 99)
                    {
                        Console.Clear();
                        for (int i = 0; i < nbgenerations; i++) // Création du nombre voulu de générations
                        {
                            Console.WriteLine("Génération " + (i + 1));
                            toriquelv2.ParcoursLv2(taille);
                            Console.WriteLine("Appuyez sur une touche pour continuer...");
                            Console.WriteLine();
                            Console.ReadKey();
                        }
                        toriquelv2.EcritureGrille("GrilleFinale.txt"); // Ecriture de la dernière génération dans un fichier "GrilleFinale.txt" dans Debug
                    }
                    else Console.WriteLine(messageerreur);
                }
                else Console.WriteLine(messageerreur);
            }
            if(niveau == 3)
            {
                int reponse;
                do
                {
                    Console.WriteLine("Quel niveau voulez vous jouer ?\n1/ Niveau 1\n2/ Niveau 2\n\n");
                    reponse = int.Parse(Console.ReadLine());
                }
                while (reponse != 1 && reponse != 2);
                if(reponse == 1)
                {
                    Console.Clear();
                    Grille fichier = new Grille(file);
                    try {
                        int size = File.ReadAllLines(file).Length;
                        Console.WriteLine("Entrez un nombre de générations à réaliser :");
                        int nbgenerations = int.Parse(Console.ReadLine());
                        if (nbgenerations > 0 && nbgenerations <= 99)
                        {
                            Console.Clear();
                            for (int i = 0; i < nbgenerations; i++) // Création du nombre voulu de générations
                            {
                                Console.WriteLine("Génération " + (i + 1));
                                fichier.Parcours(size);
                                Console.WriteLine("Appuyez sur une touche pour continuer...");
                                Console.WriteLine();
                                Console.ReadKey();
                            }
                            fichier.EcritureGrille("GrilleFinale.txt"); // Ecriture de la dernière génération dans un fichier "GrilleFinale.txt" dans Debug
                        }
                        else Console.WriteLine(messageerreur);
                    }
                    catch(FileNotFoundException f)
                    {
                        Console.WriteLine("Erreur fichier");
                    }
                }
                if(reponse == 2)
                {
                    Console.Clear();
                    Grille fichier = new Grille(file);
                    try
                    {
                        int size = File.ReadAllLines(file).Length;
                        Console.WriteLine("Entrez un nombre de générations à réaliser :");
                        int nbgenerations = int.Parse(Console.ReadLine());
                        if (nbgenerations > 0 && nbgenerations <= 99)
                        {
                            Console.Clear();
                            for (int i = 0; i < nbgenerations; i++) // Création du nombre voulu de générations
                            {
                                Console.WriteLine("Génération " + (i + 1));
                                fichier.ParcoursLv2(size);
                                Console.WriteLine("Appuyez sur une touche pour continuer...");
                                Console.WriteLine();
                                Console.ReadKey();
                            }
                            fichier.EcritureGrille("GrilleFinale.txt"); // Ecriture de la dernière génération dans un fichier "GrilleFinale.txt" dans Debug
                        }
                        else Console.WriteLine(messageerreur);
                    }
                    catch(FileNotFoundException f)
                    {
                        Console.WriteLine("Fichier non trouvé");
                    }
                }
                
            }
            Console.ReadKey();
        }
    }
}
