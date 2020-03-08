using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjetInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            string FichierInitial = "lena.bmp";
            string FichierFinal = "";
            Console.WriteLine("Bonjour ! Bienvenue dans mon application de traitement d'image Bitmap !\nRéalisé par Nicolas PICARD - TD I\n\nL'image sélectionnée est la suivante : " + FichierInitial + "\n");
            int answer;
            int answer2;
            int answer3;
            int reponse;
            int mode;
            int mat;
            string choice = " ";
            do
            {
                do
                {
                    Console.WriteLine("Que voulez-vous réaliser ?");
                    Console.WriteLine("1) Transformation colorimétrique ou rotation\n2) Création géométrique\n3) Application d'une matrice de convolution\n4) Innovation\n5) Debug");
                    try
                    {
                        reponse = int.Parse(Console.ReadLine());
                    }
                    catch(FormatException)
                    {
                        Console.WriteLine("Vous n'avez pas entré un bon format de réponse !\nAppuyez sur une touche pour continuer...");
                        Console.ReadKey();
                        reponse = 0;
                    }
                    Console.Clear();
                }
                while (reponse != 1 && reponse != 2 && reponse != 3 && reponse != 4 && reponse != 5);
                switch (reponse)
                {
                    case 1:
                        do
                        {
                            Console.WriteLine("Choisissez un traitement d'image à effectuer :\n\n1) Transformation en niveaux de gris\n2) Transformation en noir et blanc"
                            + "\n3) Transformation en négatif\n4) Transformation en sépia\n5) Rotation à 90°\n6) Rotation à 180°\n7) Rotation à 270°");
                            try
                            {
                                answer = int.Parse(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Vous n'avez pas entré un bon format de réponse !\nAppuyez sur une touche pour continuer...");
                                Console.ReadKey();
                                answer = 0;
                            }
                            Console.Clear();
                        }
                        while (answer != 1 && answer != 2 && answer != 3 && answer != 4 && answer != 5 && answer != 6 && answer != 7);
                        switch (answer)
                        {
                            case 1:
                                Image greyshades = new Image(FichierInitial);
                                if (greyshades.Fichiertrouve)
                                {
                                    greyshades.TransformationNvxDeGris();
                                    Console.WriteLine("Entrez le nom du fichier à enregistrer :");
                                    FichierFinal = Console.ReadLine() + ".bmp";
                                    Console.Clear();
                                    Console.WriteLine("Le fichier se trouve dans votre bin/Debug et se nomme " + FichierFinal);
                                    greyshades.EnregistrerImage(FichierFinal);
                                    do
                                    {
                                        Console.WriteLine("\nSouhaitez vous refaire un traitement d'image ? (O/N)");
                                        choice = Console.ReadLine();
                                    }
                                    while (choice != "O" && choice != "N");
                                }
                                else
                                {
                                    Console.WriteLine("Fichier non trouvé : " + FichierInitial);
                                    Console.ReadKey();
                                }
                                break;
                            case 2:
                                Image bandw = new Image(FichierInitial);
                                if (bandw.Fichiertrouve)
                                {
                                    bandw.TransformationNoirEtBlanc();
                                    Console.WriteLine("Entrez le nom du fichier à enregistrer :");
                                    FichierFinal = Console.ReadLine() + ".bmp";
                                    Console.Clear();
                                    Console.WriteLine("Le fichier se trouve dans votre bin/Debug et se nomme " + FichierFinal);
                                    bandw.EnregistrerImage(FichierFinal);
                                    do
                                    {
                                        Console.WriteLine("\nSouhaitez vous refaire un traitement d'image ? (O/N)");
                                        choice = Console.ReadLine();
                                    }
                                    while (choice != "O" && choice != "N");
                                }
                                else
                                {
                                    Console.WriteLine("Fichier non trouvé : " + FichierInitial);
                                    Console.ReadKey();
                                }
                                break;
                            case 3:
                                Image neg = new Image(FichierInitial);
                                if (neg.Fichiertrouve)
                                {
                                    neg.TransformationNegatif();
                                    Console.WriteLine("Entrez le nom du fichier à enregistrer :");
                                    FichierFinal = Console.ReadLine() + ".bmp";
                                    Console.Clear();
                                    Console.WriteLine("Le fichier se trouve dans votre bin/Debug et se nomme " + FichierFinal);
                                    neg.EnregistrerImage(FichierFinal);
                                    do
                                    {
                                        Console.WriteLine("\nSouhaitez vous refaire un traitement d'image ? (O/N)");
                                        choice = Console.ReadLine();
                                    }
                                    while (choice != "O" && choice != "N");
                                }
                                else
                                {
                                    Console.WriteLine("Fichier non trouvé : " + FichierInitial);
                                    Console.ReadKey();
                                }
                                break;
                            case 4:
                                Image sep = new Image(FichierInitial);
                                if(sep.Fichiertrouve)
                                {
                                    sep.TransformationSepia();
                                    Console.WriteLine("Entrez le nom du fichier à enregistrer :");
                                    FichierFinal = Console.ReadLine() + ".bmp";
                                    Console.Clear();
                                    Console.WriteLine("Le fichier se trouve dans votre bin/Debug et se nomme " + FichierFinal);
                                    sep.EnregistrerImage(FichierFinal);
                                    do
                                    {
                                        Console.WriteLine("\nSouhaitez vous refaire un traitement d'image ? (O/N)");
                                        choice = Console.ReadLine();
                                    }
                                    while (choice != "O" && choice != "N");
                                }
                                else
                                {
                                    Console.WriteLine("Fichier non trouvé : " + FichierInitial);
                                    Console.ReadKey();
                                }
                                break;
                            case 5:
                                Image rota = new Image(FichierInitial);
                                if(rota.Fichiertrouve)
                                {
                                    rota.Rotation90();
                                    Console.WriteLine("Entrez le nom du fichier à enregistrer :");
                                    FichierFinal = Console.ReadLine() + ".bmp";
                                    Console.Clear();
                                    Console.WriteLine("Le fichier se trouve dans votre bin/Debug et se nomme " + FichierFinal);
                                    rota.EnregistrerRotation90(FichierFinal);
                                    do
                                    {
                                        Console.WriteLine("\nSouhaitez vous refaire un traitement d'image ? (O/N)");
                                        choice = Console.ReadLine();
                                    }
                                    while (choice != "O" && choice != "N");
                                }
                                else
                                {
                                    Console.WriteLine("Fichier non trouvé : " + FichierInitial);
                                    Console.ReadKey();
                                }
                                break;
                            case 6:
                                Image rotat180 = new Image(FichierInitial);
                                if(rotat180.Fichiertrouve)
                                {
                                    rotat180.Rotation180();
                                    Console.WriteLine("Entrez le nom du fichier à enregistrer :");
                                    FichierFinal = Console.ReadLine() + ".bmp";
                                    Console.Clear();
                                    rotat180.EnregistrerRotation180(FichierFinal);
                                    Console.WriteLine("Le fichier se trouve dans votre bin/Debug et se nomme " + FichierFinal);
                                    do
                                    {
                                        Console.WriteLine("\nSouhaitez vous refaire un traitement d'image ? (O/N)");
                                        choice = Console.ReadLine();
                                    }
                                    while (choice != "O" && choice != "N");
                                }
                                else
                                {
                                    Console.WriteLine("Fichier non trouvé : " + FichierInitial);
                                    Console.ReadKey();
                                }
                                break;
                            case 7:
                                Image rotat270 = new Image(FichierInitial);
                                if (rotat270.Fichiertrouve)
                                {
                                    rotat270.Rotation270();
                                    Console.WriteLine("Entrez le nom du fichier à enregistrer :");
                                    FichierFinal = Console.ReadLine() + ".bmp";
                                    Console.Clear();
                                    rotat270.EnregistrerRotation270(FichierFinal);
                                    Console.WriteLine("Le fichier se trouve dans votre bin/Debug et se nomme " + FichierFinal);
                                    do
                                    {
                                        Console.WriteLine("\nSouhaitez vous refaire un traitement d'image ? (O/N)");
                                        choice = Console.ReadLine();
                                    }
                                    while (choice != "O" && choice != "N");
                                }
                                else
                                {
                                    Console.WriteLine("Fichier non trouvé : " + FichierInitial);
                                    Console.ReadKey();
                                }
                                break;
                        }
                        break;
                    case 2:
                        do
                        {
                            Console.WriteLine("Choisissez une forme d'image à effectuer :\n\n1) Création d'un sablier\n2) Création du logo Deadmau5");
                            try
                            {
                                answer2 = int.Parse(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Vous n'avez pas entré un bon format de réponse !\nAppuyez sur une touche pour continuer...");
                                Console.ReadKey();
                                answer2 = 0;
                            }
                            Console.Clear();
                        }
                        while (answer2 != 1 && answer2 != 2);
                        switch (answer2)
                        {
                            case 1:
                                Image creation = new Image(500, 500);
                                creation.FormeGeo();
                                Console.WriteLine("Saisissez un nom d'image :");
                                string nom = Console.ReadLine();
                                creation.EnregistrerImage(nom + ".bmp");
                                Console.WriteLine("Le fichier se trouve dans votre bin/Debug et se nomme " + nom + ".bmp");
                                do
                                {
                                    Console.WriteLine("\nSouhaitez vous refaire un traitement d'image ? (O/N)");
                                    choice = Console.ReadLine();
                                }
                                while (choice != "O" && choice != "N");
                                break;
                            case 2:
                                Image deadmau = new Image(500, 500);
                                deadmau.Deadmau5();
                                Console.WriteLine("Saisissez un nom d'image :");
                                string name = Console.ReadLine();
                                deadmau.EnregistrerImage(name + ".bmp");
                                Console.WriteLine("Le fichier se trouve dans votre bin/Debug et se nomme " + name + ".bmp");
                                do
                                {
                                    Console.WriteLine("\nSouhaitez vous refaire un traitement d'image ? (O/N)");
                                    choice = Console.ReadLine();
                                }
                                while (choice != "O" && choice != "N");
                                break;
                        }
                        break;
                    case 3:
                        Image convolution = new Image(FichierInitial);
                        if (convolution.Fichiertrouve)
                        {
                            do
                            {
                                Console.WriteLine("Choisissez le mode de remplissage du filtre de convolution :\n");
                                Console.WriteLine("1) Manuel\n2) Filtres prédéfinis");
                                try
                                {
                                    mode = int.Parse(Console.ReadLine());
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Vous n'avez pas entré un bon format de réponse !\nAppuyez sur une touche pour continuer...");
                                    Console.ReadKey();
                                    mode = 0;
                                }
                                Console.Clear();
                            }
                            while (mode != 1 && mode != 2);
                            switch (mode)
                            {
                                case 1:
                                    Console.WriteLine("Saisissez une par une la valeur de chaque indice de votre matrice (3x3)");
                                    int index = 9;
                                    for (int i = 0; i < convolution.Filtre.GetLength(0); i++)
                                    {
                                        for (int j = 0; j < convolution.Filtre.GetLength(1); j++)
                                        {
                                            Console.WriteLine("Ligne : " + i + " Colonne : " + j);
                                            try
                                            {
                                                convolution.Filtre[i, j] = int.Parse(Console.ReadLine());
                                            }
                                            catch(FormatException)
                                            {
                                                convolution.Filtre[i, j] = 0;
                                                Console.WriteLine("\nMauvais format : vous n'avez pas entré un entier ! un 0 a été ajouté\n");
                                            }
                                            index--;
                                            Console.WriteLine("Il reste " + index + " valeurs à compléter");
                                        }
                                    }
                                    convolution.AppliquerFiltre();
                                    Console.Clear();
                                    Console.WriteLine("Entrez le nom du fichier à enregistrer :");
                                    FichierFinal = Console.ReadLine() + ".bmp";
                                    Console.Clear();
                                    Console.WriteLine("Le fichier se trouve dans votre bin/Debug et se nomme " + FichierFinal);
                                    convolution.EnregistrerConvo(FichierFinal);
                                    do
                                    {
                                        Console.WriteLine("\nSouhaitez vous refaire un traitement d'image ? (O/N)");
                                        choice = Console.ReadLine();
                                    }
                                    while (choice != "O" && choice != "N");
                                    break;
                                case 2:
                                    do
                                    {
                                        Console.WriteLine("Choisissez le filtre de convolution :\n");
                                        Console.WriteLine("1) Augmentation du contraste\n2) Flou\n3) Renforcement des bords\n4) Détection des bords\n5) Repoussage");
                                        try
                                        {
                                            mat = int.Parse(Console.ReadLine());
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Vous n'avez pas entré un bon format de réponse !\nAppuyez sur une touche pour continuer...");
                                            Console.ReadKey();
                                            mat = 0;
                                        }
                                        Console.Clear();
                                    }
                                    while (mat != 1 && mat != 2 && mat != 3 && mat != 4 && mat != 5);
                                    switch (mat)
                                    {
                                        case 1:
                                            convolution.Filtre = new int[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
                                            break;
                                        case 2:
                                            convolution.Filtre = new int[3, 3] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
                                            break;
                                        case 3:
                                            convolution.Filtre = new int[3, 3] { { 0, 0, 0 }, { -1, 1, 0 }, { 0, 0, 0 } };
                                            break;
                                        case 4:
                                            convolution.Filtre = new int[3, 3] { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
                                            break;
                                        case 5:
                                            convolution.Filtre = new int[3, 3] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
                                            break;
                                    }
                                    convolution.AppliquerFiltre();
                                    Console.WriteLine("Entrez le nom du fichier à enregistrer :");
                                    FichierFinal = Console.ReadLine() + ".bmp";
                                    Console.Clear();
                                    Console.WriteLine("Le fichier se trouve dans votre bin/Debug et se nomme " + FichierFinal);
                                    convolution.EnregistrerConvo(FichierFinal);
                                    do
                                    {
                                        Console.WriteLine("\nSouhaitez vous refaire un traitement d'image ? (O/N)");
                                        choice = Console.ReadLine();
                                    }
                                    while (choice != "O" && choice != "N");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Fichier non trouvé : " + FichierInitial);
                            Console.ReadKey();
                        }
                        break;
                    case 4:
                        do
                        {
                            Console.WriteLine("Choisissez une innovation à effectuer :\n\n1) Photomaton\n2) Fractale de Mandelbrot\n3) Fractale de Julia");
                            try
                            {
                                answer3 = int.Parse(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Vous n'avez pas entré un bon format de réponse !\nAppuyez sur une touche pour continuer...");
                                Console.ReadKey();
                                answer3 = 0;
                            }
                            Console.Clear();
                        }
                        while (answer3 != 1 && answer3 != 2 && answer3 != 3);
                        switch (answer3)
                        {
                            case 1:
                                Image carree = new Image(FichierInitial);
                                if (carree.Largeur % 2 == 0 && carree.Hauteur % 2 == 0 && carree.Fichiertrouve == true)
                                {
                                    carree.Photomaton();
                                    Console.WriteLine("Entrez le nom du fichier à enregistrer :");
                                    FichierFinal = Console.ReadLine() + ".bmp";
                                    Console.Clear();
                                    carree.EnregistrerPhotomaton(FichierFinal);
                                    Console.WriteLine("Le fichier se trouve dans votre bin/Debug et se nomme " + FichierFinal);
                                }
                                else
                                {
                                    if (carree.Largeur % 2 != 0 || carree.Hauteur % 2 != 0)
                                    {
                                        Console.WriteLine("Erreur : fichier de largeur ou hauteur non paires");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Fichier non trouvé : " + FichierInitial);
                                        Console.ReadKey();
                                    }
                                }
                                do
                                {
                                    Console.WriteLine("\nSouhaitez vous refaire un traitement d'image ? (O/N)");
                                    choice = Console.ReadLine();
                                }
                                while (choice != "O" && choice != "N");
                                break;
                            case 2:
                                Image fractale = new Image(1000, 1000);
                                int iterations;
                                do
                                {
                                    Console.WriteLine("Combien d'itérations voulez vous effectuer (inférieur à 250) ?");
                                    iterations = int.Parse(Console.ReadLine());
                                }
                                while (iterations > 250 || iterations < 0);
                                Console.WriteLine("Veuillez patienter...(Le traitement peut être long pour beaucoup d'itérations !)");
                                fractale.FractaleMandelBrot(iterations);
                                Console.Clear();
                                Console.WriteLine("Entrez le nom du fichier à enregistrer :");
                                FichierFinal = Console.ReadLine() + ".bmp";
                                Console.Clear();
                                fractale.EnregistrerImage(FichierFinal);
                                Console.WriteLine("Le fichier se trouve dans votre bin/Debug et se nomme " + FichierFinal);
                                do
                                {
                                    Console.WriteLine("\nSouhaitez vous refaire un traitement d'image ? (O/N)");
                                    choice = Console.ReadLine();
                                }
                                while (choice != "O" && choice != "N");
                                break;
                            case 3:
                                Image julia = new Image(1000, 1000);
                                int ite;
                                do
                                {
                                    Console.WriteLine("Combien d'itérations voulez vous effectuer (inférieur à 250) ?");
                                    ite = int.Parse(Console.ReadLine());
                                }
                                while (ite > 250 || ite < 0);
                                Console.WriteLine("Veuillez patienter...(Le traitement peut être long pour beaucoup d'itérations !)");
                                julia.FractaleJulia(ite);
                                Console.Clear();
                                Console.WriteLine("Entrez le nom du fichier à enregistrer :");
                                FichierFinal = Console.ReadLine() + ".bmp";
                                Console.Clear();
                                julia.EnregistrerImage(FichierFinal);
                                Console.WriteLine("Le fichier se trouve dans votre bin/Debug et se nomme " + FichierFinal);
                                do
                                {
                                    Console.WriteLine("\nSouhaitez vous refaire un traitement d'image ? (O/N)");
                                    choice = Console.ReadLine();
                                }
                                while (choice != "O" && choice != "N");
                                break;
                        }
                        break;
                    case 5:
                        {
                            string rep;
                            do
                            {
                                Console.WriteLine("----------------------- Attention -----------------------\n" +
                                "Pour des images de taille importante, la console affichera \nbeaucoup d'informations et vous risquez de ne pas tout voir !\n\n");
                                Console.WriteLine("Voulez-vous continuer ?\nO : Oui\nN : Non");
                                rep = Console.ReadLine();
                                Console.Clear();
                            }
                            while (rep != "O" && rep != "N");
                            if (rep == "O")
                            {
                                Image utilitaire = new Image(FichierInitial);
                                utilitaire.Debug(FichierInitial);
                                Console.WriteLine("\n\nAppuyez sur une touche pour continuer...");
                                Console.ReadKey();
                                Console.Clear();
                                do
                                {
                                    Console.WriteLine("Souhaitez vous refaire un traitement d'image ? (O/N)");
                                    choice = Console.ReadLine();
                                }
                                while (choice != "O" && choice != "N");
                            }
                            break;
                        }
                }
                Console.Clear();
            }
            while (choice == "O");
            Console.Clear();
            Console.WriteLine("Merci et à bientôt !");
            Console.ReadKey();
        }
    }
}
