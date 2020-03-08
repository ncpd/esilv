using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjetInfo
{
    class Image
    {
        private const int HEADER = 14; //Taille header
        private int TAILLE_HEADER_INFO = 0; //Provisoire, défini plus tard
        private int OCTETS_HEADER_INFO = 4; //Octets header info
        private int TAILLE_FICHIER = 0; //Provisoire, défini plus tard
        private bool fichiertrouve; //Vérification si le fichier est trouvé
        private int largeur;
        private int hauteur;
        Pixel[,] picture; // Image
        Pixel[,] photomat; // Photomaton
        Pixel[,] convo; // Image avec filtre de convolution
        Pixel[,] rota90; // Image où la rotation de 90° a été appliquée
        Pixel[,] rota180; // Image où la rotation de 180° a été appliquée
        Pixel[,] rota270; // Image où la rotation de 270° a été appliquée
        private int[,] filtre = new int[3,3]; // Filtre de convolution défini dans le menu

        #region Propriétés

        public int Largeur
        {
            get
            {
                return largeur;
            }

            set
            {
                largeur = value;
            }
        }

        public int Hauteur
        {
            get
            {
                return hauteur;
            }

            set
            {
                hauteur = value;
            }
        }

        public bool Fichiertrouve
        {
            get
            {
                return fichiertrouve;
            }

            set
            {
                fichiertrouve = value;
            }
        }

        public int[,] Filtre
        {
            get
            {
                return filtre;
            }

            set
            {
                filtre = value;
            }
        }

        #endregion

        #region Constructeurs
        /// <summary>
        /// Ce constructeur est utilisé lorsqu'on réalise un traitement d'image à partir d'un fichier. Il initialise la matrice de pixels pour obtenir l'image.
        /// </summary>
        /// <param name="filename"> nom du fichier à traiter </param>
        public Image(string filename)
        {
            byte[] image = LectureFichier(filename);
            if (fichiertrouve == true)
            {
                TAILLE_HEADER_INFO = ConvertirEndianToInt(image, 14, 18); // OCTETS DE 14 A 18 : TAILLE HEADER INFO
                this.largeur = ConvertirEndianToInt(image, 18, 22); // OCTETS DE 18 A 22 : LARGEUR EN PIXELS DE L'IMAGE
                this.hauteur = ConvertirEndianToInt(image, 22, 26); // OCTETS DE 22 A 26 : HAUTEUR EN PIXELS DE L'IMAGE
                picture = new Pixel[hauteur, largeur];
                photomat = new Pixel[hauteur, largeur];
                convo = new Pixel[hauteur, largeur];
                rota90 = new Pixel[largeur, hauteur];
                rota180 = new Pixel[hauteur, largeur];
                rota270 = new Pixel[largeur, hauteur];
                int indexBinaire = TAILLE_HEADER_INFO + HEADER;
                for (int j = hauteur - 1; j >= 0; j--)
                {
                    for (int k = 0; k < largeur; k++)
                    {
                        int blue = image[indexBinaire];
                        indexBinaire++;
                        int green = image[indexBinaire];
                        indexBinaire++;
                        int red = image[indexBinaire];
                        indexBinaire++;
                        picture[j, k] = new Pixel(blue, green, red);
                        photomat[j, k] = new Pixel(blue, green, red);
                        convo[j, k] = new Pixel(blue, green, red);
                        rota90[k, j] = new Pixel(blue, green, red);
                        rota180[j, k] = new Pixel(blue, green, red);
                        rota270[k, j] = new Pixel(blue, green, red);
                    }
                }
            }
        }
        /// <summary>
        /// Ce constructeur est utilisé lors de la création d'une image
        /// </summary>
        /// <param name="width"> largeur de l'image à créer </param>
        /// <param name="height"> hauteur de l'image à créer </param>
        public Image(int width, int height)
        {
            this.largeur = width;
            this.hauteur = height;
            TAILLE_HEADER_INFO = 40;
            picture = new Pixel[hauteur, largeur];
            for (int j = hauteur - 1; j >= 0; j--)
            {
                for (int k = 0; k < largeur; k++)
                {
                    int blue = 0;
                    int green = 0;
                    int red = 0;
                    picture[j, k] = new Pixel(blue, green, red);
                }
            }
        }

        #endregion

        #region Méthodes

        #region Lecture / Debug / Utilitaires

        /// <summary>
        /// Affiche toutes les valeurs des bytes du fichier bitmap. Il affiche les valeurs de chaque octet du header, du header info ainsi que
        /// les valeurs de chacun des pixels de l'image
        /// </summary>
        /// <param name="nomfichier"> Nom de fichier qui sera lu par la méthode LectureFichier </param>
        public void Debug(string nomfichier)
        {
            byte[] myfile = LectureFichier(nomfichier);
            if (fichiertrouve == true)
            {
                Console.WriteLine("HEADER\n");
                for (int i = 0; i < HEADER; i++)
                {
                    Console.Write(myfile[i] + " ");
                }
                Console.WriteLine("\n");
                TAILLE_FICHIER = ConvertirEndianToInt(myfile, 2, 6);
                Console.WriteLine("TAILLE FICHIER = " + TAILLE_FICHIER + " OCTETS");
                TAILLE_HEADER_INFO = ConvertirEndianToInt(myfile, 14, 18);
                Console.WriteLine("TAILLE HEADER INFO = " + TAILLE_HEADER_INFO + "\n");
                Console.WriteLine("INFOS\n");
                for (int i = HEADER; i < TAILLE_HEADER_INFO + HEADER; i++)
                {
                    Console.Write(myfile[i] + " ");
                }
                Console.WriteLine("\n");
                Console.WriteLine("IMAGE\n");
                for (int i = TAILLE_HEADER_INFO + HEADER; i < myfile.Length; i++) //LECTURE A PARTIR DU PREMIER PIXEL
                {
                    Console.Write(myfile[i] + "\t");
                }
            }
            else
            {
                Console.WriteLine("Fichier non trouvé !");
            }
        }
        /// <summary>
        /// Lit un fichier et modifie l'état de la variable fichiertrouve si celui-ci n'est pas trouvé
        /// </summary>
        /// <param name="nomfichier"> Nom du fichier à aller chercher </param>
        /// <returns></returns>
        private byte[] LectureFichier(string nomfichier)
        {
            byte[] image = null;
            try
            {
                image = File.ReadAllBytes(nomfichier);
                fichiertrouve = true;
            }
            catch(FileNotFoundException)
            {
                fichiertrouve = false;
            }
            return image;
        }

        /// <summary>
        /// Convertit un tableau d'octets en un entier en multipliant chaque case par une puissance de 256
        /// </summary>
        /// <param name="tab"> Tableau d'octets qui est le fichier </param>
        /// <param name="debut"> Index du tableau auquel commencer l'opération </param>
        /// <param name="fin"> Index du tableau auquel finir l'opération </param>
        /// <returns></returns>
        private int ConvertirEndianToInt(byte[] tab,int debut,int fin)
        {
            int somme = 0;
            double pow = 0.0;
            int multiple;
            for (int i = debut; i < fin; i++)
            {
                multiple = Convert.ToInt32(Math.Pow(256.0, pow));
                somme = somme + (tab[i] * multiple);
                pow++;
            }
            return somme;
        }

        #endregion

        #region Transformations Colorimétriques

        /// <summary>
        /// Applique à chaque pixel de l'image la méthode NiveauDeGris
        /// </summary>
        public void TransformationNvxDeGris()
        {
            for(int i = 0; i < hauteur; i++)
            {
                for(int j = 0; j < largeur; j++)
                {
                    picture[i, j].NiveauDeGris();
                }
            }
        }
        /// <summary>
        /// Applique à chaque pixel de l'image la méthode NoirEtBlanc
        /// </summary>
        public void TransformationNoirEtBlanc()
        {
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    picture[i, j].NoirEtBlanc();
                }
            }
        }
        /// <summary>
        /// Applique à chaque pixel de l'image la méthode Negatif
        /// </summary>
        public void TransformationNegatif()
        {
            for(int i = 0; i < hauteur; i++)
            {
                for(int j = 0; j < largeur; j++)
                {
                    picture[i, j].Negatif();
                }
            }
        }
        /// <summary>
        /// Applique à chaque pixel de l'image la méthode Sepia
        /// </summary>
        public void TransformationSepia()
        {
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    picture[i, j].Sepia();
                }
            }
        }

        #endregion

        #region Convolution / Forme géométrique

        /// <summary>
        /// Calcule premièrement le diviseur du filtre. Calcule ensuite la valeur (à diviser) de la nouvelle composante en multipliant 
        /// chaque composante des pixels voisins par les valeurs du filtre. Divise ensuite cette valeur par le diviseur pour obtenir la valeur de la 
        /// nouvelle composante. Si la valeur est négative on la rend positive car la valeur d'un pixel ne peut être négative.
        /// </summary>
        public void AppliquerFiltre()
        {
            int diviseur = 0; // CALCUL DIVISEUR
            for (int k = 0; k < filtre.GetLength(0); k++)
            {
                for (int l = 0; l < filtre.GetLength(1); l++)
                {
                    diviseur += filtre[k, l];
                }
            }
            if (diviseur == 0)
            {
                diviseur = 1; // PAS DE DIVISION NULLE
            }
            // APPLICATION DU FILTRE
            for (int i = 1; i < picture.GetLength(0) - 2; i++)
            {
                for (int j = 1; j < picture.GetLength(1) - 2; j++)
                {
                    int valBleu = (picture[i - 1, j - 1].Bleu * filtre[0, 0]) + (picture[i - 1, j].Bleu * filtre[0, 1]) + (picture[i - 1, j + 1].Bleu * filtre[0, 2])
                        + (picture[i, j - 1].Bleu * filtre[1, 0]) + (picture[i, j].Bleu * filtre[1, 1]) + (picture[i, j + 1].Bleu * filtre[1, 2])
                        + (picture[i + 1, j - 1].Bleu * filtre[2, 0]) + (picture[i + 1, j].Bleu * filtre[2, 1]) + (picture[i + 1, j + 1].Bleu * filtre[2, 2]);
                    if (valBleu < 0) // PAS DE VALEURS NEGATIVES POUR LES PIXELS
                    {
                        valBleu = -valBleu;
                    }
                    convo[i, j].SetBlueValue(valBleu / diviseur);
                    int valVert = (picture[i - 1, j - 1].Vert * filtre[0, 0]) + (picture[i - 1, j].Vert * filtre[0, 1]) + (picture[i - 1, j + 1].Vert * filtre[0, 2])
                        + (picture[i, j - 1].Vert * filtre[1, 0]) + (picture[i, j].Vert * filtre[1, 1]) + (picture[i, j + 1].Vert * filtre[1, 2])
                        + (picture[i + 1, j - 1].Vert * filtre[2, 0]) + (picture[i + 1, j].Vert * filtre[2, 1]) + (picture[i + 1, j + 1].Vert * filtre[2, 2]);
                    if (valVert < 0)
                    {
                        valVert = -valVert;
                    }
                    convo[i, j].SetGreenValue(valVert / diviseur);
                    int valRouge = (picture[i - 1, j - 1].Rouge * filtre[0, 0]) + (picture[i - 1, j].Rouge * filtre[0, 1]) + (picture[i - 1, j + 1].Rouge * filtre[0, 2])
                        + (picture[i, j - 1].Rouge * filtre[1, 0]) + (picture[i, j].Rouge * filtre[1, 1]) + (picture[i, j + 1].Rouge * filtre[1, 2])
                        + (picture[i + 1, j - 1].Rouge * filtre[2, 0]) + (picture[i + 1, j].Rouge * filtre[2, 1]) + (picture[i + 1, j + 1].Rouge * filtre[2, 2]);
                    if (valRouge < 0)
                    {
                        valRouge = -valRouge;
                    }
                    convo[i, j].SetRedValue(valRouge / diviseur);
                }
            }
        }
        /// <summary>
        /// Créé un sablier dans l'image en mettant les pixels compris dedans en bleu et le reste des pixels en noir
        /// </summary>
        public void FormeGeo()
        {
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    if ((i <= j && i <= (largeur - 1 - j)) || (i >= j && i >= (largeur - 1 - j)))
                    {
                        picture[i, j].DessinerBleu();
                    }
                    else
                    {
                        picture[i, j].DessinerNoir();
                    }
                }
            }
        }
        /// <summary>
        /// Créé le logo de Deadmau5 (artiste de musique électronique)
        /// </summary>
        public void Deadmau5()
        {
            int lignecercle1 = hauteur / 4;
            int colonnecercle1 = largeur / 4;
            int lignecercle2 = 3 * (hauteur / 4);
            int colonnecercle2 = largeur / 4;
            int centrex = largeur / 2;
            int centrey = hauteur / 2;
            int oeil1x = (int)(1.75 * largeur / 5);
            int oeil1y = (int)(2.4 * hauteur / 6);
            int oeil2x = (int)(3.25 * largeur / 5);
            int oeil2y = (int)(2.4 * hauteur / 6);
            for(int i = 0; i < hauteur; i++)
            {
                for(int j = 0; j < largeur; j++)
                {
                    if ((i - centrey) * (i - centrey) + (j - centrex) * (j - centrex) <= 40 * largeur) // EQUATION DE LA TETE (plus gros cercle)
                    {
                        picture[i, j].DessinerNoir();
                        if (j >= (centrex - 30 * largeur) && j <= (centrex + 30 * largeur) && i >= centrey && (i - centrey) * (i - centrey) + (j - centrex) * (j - centrex) <= 30 * largeur)
                        {                                       // EQUATION DE LA BOUCHE
                            picture[i, j].SetBlueValue(0);
                            picture[i, j].SetGreenValue(0);
                            picture[i, j].SetRedValue((i * 185) / hauteur); //dégradé de rouge
                        }
                        if((i - oeil1y) * (i - oeil1y) + (j - oeil1x) * (j - oeil1x) <= 3 * largeur) // EQUATION DE L'OEIL GAUCHE
                        {
                            picture[i, j].SetBlueValue(0);
                            picture[i, j].SetGreenValue(0);
                            picture[i, j].SetRedValue((i * 185) / hauteur); //dégradé de rouge
                        }
                        if ((i - oeil2y) * (i - oeil2y) + (j - oeil2x) * (j - oeil2x) <= 3 * largeur) // EQUATION DE L'OEIL DROIT
                        {
                            picture[i, j].SetBlueValue(0);
                            picture[i, j].SetGreenValue(0);
                            picture[i, j].SetRedValue((i * 185) / hauteur); //dégradé de rouge
                        }
                    }
                    else if ((i - colonnecercle1) * (i - colonnecercle1) + (j - lignecercle1) * (j - lignecercle1) <= 15 * largeur) //EQUATION OREILLE GAUCHE
                    {
                        picture[i, j].DessinerNoir();
                    }
                    else if ((i - colonnecercle2) * (i - colonnecercle2) + (j - lignecercle2) * (j - lignecercle2) <= 15 * largeur) //EQUATION OREILLE DROITE
                    {
                        picture[i, j].DessinerNoir();
                    }
                    else //FOND
                    {
                        picture[i, j].SetBlueValue(0);
                        picture[i, j].SetGreenValue(0);
                        picture[i, j].SetRedValue((int)(0.7 * 255)); //rouge pas trop clair
                    }
                }
            }
        }

        #endregion
        /// <summary>
        /// Applique la transformation du photomaton à une image (de largeur et hauteur paires obligatoirement !) avec :
        /// 2k -> k   ET   2k + 1 -> ((hauteur ou largeur selon le cas) / 2) + k
        /// </summary>
        public void Photomaton()
        {
            for(int i = 0; i < picture.GetLength(0); i++)
            {
                for(int j = 0; j < picture.GetLength(1); j++)
                {
                    if(i % 2 == 0 && j % 2 == 0) //Cas avec index de ligne pair et index de colonne pair
                    {
                        photomat[i / 2, j / 2].CopyPixel(picture[i, j]); //Copie le pixel actuel en (i / 2, j / 2)
                    }
                    if (i % 2 == 0 && j % 2 == 1) //Cas avec index de ligne pair et index de colonne impair
                    {
                        photomat[i / 2, ((j - 1) / 2) + (largeur / 2)].CopyPixel(picture[i, j]); //Copie le pixel actuel en (i / 2, ((j-1) / 2) + (largeur / 2))
                    }
                    if(i % 2 == 1 && j % 2 == 0) //Cas avec index de ligne impair et index de colonne pair
                    {
                        photomat[((i - 1) / 2) + (hauteur / 2), j / 2].CopyPixel(picture[i, j]); //Copie le pixel actuel en (((i-1) / 2) + (hauteur / 2), j / 2)
                    }
                    if (i % 2 == 1 && j % 2 == 1) //Cas avec index de ligne impair et index de colonne impair
                    {
                        photomat[((i - 1) / 2) + (hauteur / 2), ((j - 1) / 2) + (largeur / 2)].CopyPixel(picture[i, j]);
                        //Copie le pixel actuel en (((i-1) / 2) + (hauteur / 2), ((j-1) / 2) + (largeur / 2))
                    }
                }
            }
        }

        #region Fractales

        /// <summary>
        /// Dessine la fractale de Mandelbrot
        /// </summary>
        /// <param name="iterations"></param>
        public void FractaleMandelBrot(int iterations)
        {
            double x1 = -2.1;
            double x2 = 0.6;
            double y1 = -1.2;
            double y2 = 1.2;
            int nbiterations = iterations;
            double zoomwidth = largeur / (y2 - y1);
            double zoomheight = hauteur / (x2 - x1);
            for(int x = 0; x < hauteur; x++)
            {
                for(int y = 0; y < largeur; y++)
                {
                    Complexe c = new Complexe(x / zoomheight + x1, y / zoomwidth + y1);
                    Complexe Z = new Complexe(0, 0);
                    int i = 0;
                    do
                    {
                        double temp = Z.Re;
                        Z.Re = Z.Re * Z.Re - Z.Im * Z.Im + c.Re; // Suite de Mandelbrot : Z(n+1) = (Zn)^2 + c
                        Z.Im = 2 * Z.Im * temp + c.Im;
                        i++;
                    }
                    while (i < nbiterations && Z.Module() < 2); // Si le module est < 2 il fait partie de la fractale
                    if (i == nbiterations)
                    {
                        picture[x, y].DessinerNoir();
                    }
                    else
                    {
                        picture[x, y].SetBlueValue((i * 255) / nbiterations); //dégradé de cyan
                        picture[x, y].SetGreenValue((i * 255) / nbiterations);
                        picture[x, y].SetRedValue(0);
                    }
                }
            }
        }

        /// <summary>
        /// Dessine les ensembles de Julia
        /// </summary>
        /// <param name="iterations"></param>
        public void FractaleJulia(int iterations)
        {
            double x1 = -1.0;
            double x2 = 1.0;
            double y1 = -1.2;
            double y2 = 1.2;
            int nbiterations = iterations;
            double zoomwidth = largeur / (y2 - y1);
            double zoomheight = hauteur / (x2 - x1);
            for (int x = 0; x < hauteur; x++)
            {
                for (int y = 0; y < largeur; y++)
                {
                    Complexe c = new Complexe(0.285,0.01);
                    Complexe Z = new Complexe(x / zoomheight + x1, y / zoomwidth + y1);
                    int i = 0;
                    do
                    {
                        double temp = Z.Re;
                        Z.Re = Z.Re * Z.Re - Z.Im * Z.Im + c.Re; // Suite de Julia est la même que Mandelbrot : Z(n+1) = (Zn)^2 + c
                        Z.Im = 2 * Z.Im * temp + c.Im;
                        i++;
                    }
                    while (i < nbiterations && Z.Module() < 2); // Si le module est < 2 il fait partie de la fractale
                    if (i == nbiterations)
                    {
                        picture[x, y].DessinerBlanc();
                    }
                    else
                    {
                        picture[x, y].SetBlueValue(0);
                        picture[x, y].SetGreenValue((i * 255) / nbiterations); // dégradé de jaune
                        picture[x, y].SetRedValue((i * 255) / nbiterations);
                    }
                }
            }
        }

        #endregion

        #region Rotations
        /// <summary>
        /// La méthode Rotation90 prend chaque Pixel de l'image originale et les place dans une nouvelle image (matrice de pixels) rota90 tout en tenant compte des indices.
        /// </summary>
        public void Rotation90()
        {
            int indexl = rota90.GetLength(0) - 1;
            for(int i = 0; i < picture.GetLength(0); i++)
            {
                for(int j = 0; j < picture.GetLength(1); j++)
                {
                    rota90[indexl, i].CopyPixel(picture[i, j]);
                    indexl--;
                }
                indexl = rota90.GetLength(0) - 1;
            }
        }
        /// <summary>
        ///  La méthode Rotation180 prend chaque Pixel de l'image originale et les place dans une nouvelle image (matrice de pixels) rota180 tout en tenant compte des indices.
        /// </summary>
        public void Rotation180()
        {
            int indexl = rota180.GetLength(0) - 1;
            int indexc = rota180.GetLength(1) - 1;
            for (int i = 0; i < picture.GetLength(0); i++)
            {
                for (int j = 0; j < picture.GetLength(1); j++)
                {
                    rota180[indexl, indexc].CopyPixel(picture[i, j]);
                    indexc--;
                }
                indexl--;
                indexc = rota180.GetLength(1) - 1;
            }
        }
        /// <summary>
        ///  La méthode Rotation270 prend chaque Pixel de l'image originale et les place dans une nouvelle image (matrice de pixels) rota270 tout en tenant compte des indices.
        /// </summary>
        public void Rotation270()
        {
            int indexc = rota270.GetLength(1) - 1;
            for (int i = 0; i < picture.GetLength(0); i++)
            {
                for (int j = 0; j < picture.GetLength(1); j++)
                {
                    rota270[j, indexc].CopyPixel(picture[i, j]);
                }
                indexc--;
            }
        }

        #endregion

        #region Enregistrement
        /// <summary>
        /// Créé le header d'une image Bitmap
        /// </summary>
        /// <returns>Elle retourne un tableau de 14 octets qui correspond au header de l'image à enregistrer</returns>
        private byte[] CreerHeader()
        {
            byte[] header = new byte[14];
            header[0] = (byte)'B';
            header[1] = (byte)'M';
            int val = HEADER + OCTETS_HEADER_INFO + (largeur * hauteur * 3);
            byte[] converti = ConvertIntToEndian(val);
            for (int i = 2; i < 6; i++)
            {
                header[i] = converti[i - 2];
            }
            for (int i = 6; i < 10; i++)
            {
                header[i] = 0;
            }
            header[10] = (byte)(HEADER + OCTETS_HEADER_INFO);
            header[11] = 0;
            header[12] = 0;
            header[13] = 0;
            return header;
        }
        /// <summary>
        /// Créé la partie des informations du header pour y entrer les dimensions, etc..
        /// </summary>
        /// <returns>Elle retourne un tableau de 40 octets (dû au format bmp) qui correspond aux informations du header</returns>
        private byte[] CreerHeaderInfo()
        {
            byte[] headerinfo = new byte[TAILLE_HEADER_INFO];
            headerinfo[0] = (byte)TAILLE_HEADER_INFO;
            for (int i = 1; i < 3; i++)
            {
                headerinfo[i] = 0;
            }
            byte[] width = ConvertIntToEndian(largeur);
            for (int i = 4; i < 8; i++)
            {
                headerinfo[i] = width[i - 4];
            }
            byte[] height = ConvertIntToEndian(hauteur);
            for (int i = 8; i < 12; i++)
            {
                headerinfo[i] = height[i - 8];
            }
            headerinfo[12] = 1;
            headerinfo[13] = 0;
            headerinfo[14] = 24;
            headerinfo[15] = 0;
            for (int i = 16; i < 40; i++)
            {
                headerinfo[i] = 0;
            }
            return headerinfo;
        }
        /// <summary>
        /// Créé la partie des informations du header pour y entrer les dimensions, etc..mais on y inverse largeur et hauteur (pour les images pivotées à 90 et 270°)
        /// </summary>
        /// <returns>Elle retourne un tableau de 40 octets (dû au format bmp) qui correspond aux informations du header</returns>
        private byte[] CreerHeaderInfoRotation()
        {
            byte[] headerinfo = new byte[TAILLE_HEADER_INFO];
            headerinfo[0] = (byte)TAILLE_HEADER_INFO;
            for (int i = 1; i < 3; i++)
            {
                headerinfo[i] = 0;
            }
            byte[] height = ConvertIntToEndian(hauteur);
            for (int i = 4; i < 8; i++)
            {
                headerinfo[i] = height[i - 4];
            }
            byte[] width = ConvertIntToEndian(largeur);
            for (int i = 8; i < 12; i++)
            {
                headerinfo[i] = width[i - 8];
            }
            headerinfo[12] = 1;
            headerinfo[13] = 0;
            headerinfo[14] = 24;
            headerinfo[15] = 0;
            for (int i = 16; i < 40; i++)
            {
                headerinfo[i] = 0;
            }
            return headerinfo;
        }
        /// <summary>
        /// Convertit un entier en un tableau de 4 octets en little endian
        /// </summary>
        /// <param name="val"> Valeur à convertir </param>
        /// <returns>Retourne un tableau de 4 octets en little endian (utilisé pour les tailles de fichier et les dimensions)</returns>
        private byte[] ConvertIntToEndian(int val)
        {
            byte[] retour = new byte[4];
            if(val < Math.Pow(256,1))
            {
                retour[0] = (byte)val;
                for(int i = 1; i < 4; i++)
                {
                    retour[i] = (byte)0;
                }

            }
            else if(val >= Math.Pow(256,1) && val < Math.Pow(256, 2))
            {
                retour[0] = (byte)(val % Math.Pow(256, 1));
                retour[1] = (byte)((val - retour[0])/ Math.Pow(256, 1));
                retour[2] = (byte)0;
                retour[3] = (byte)0;
            }
            else if(val >= Math.Pow(256,2) && val < Math.Pow(256,3))
            {
                int reste = (int)(val % Math.Pow(256, 2));
                if(reste < Math.Pow(256,1))
                {
                    retour[0] = (byte)reste;
                    retour[1] = (byte)0;
                    retour[2] = (byte)((val - reste) / Math.Pow(256, 2));
                    retour[3] = (byte)0;
                }
                else
                {
                    retour[0] = (byte)(reste % Math.Pow(256, 1));
                    retour[1] = (byte)(reste / Math.Pow(256, 1));
                    retour[2] = (byte)((val - reste) / Math.Pow(256, 2));
                    retour[3] = (byte)0;
                }
            }
            return retour;
        }
        /// <summary>
        /// La méthode EnregistrerImage crée un nouveau tableau d'octets (la nouvelle image bmp), lui crée un header et un header info, puis
        /// enregistre chaque composante de chaque pixel de l'image dans ce nouveau tableau, et enfin l'écrit dans un fichier.
        /// </summary>
        /// <param name="fichierfinal">Prend en paramètre un nom de fichier pour l'enregistrement</param>
        public void EnregistrerImage(string fichierfinal)
        {
            byte[] header = CreerHeader();
            byte[] headerinfo = CreerHeaderInfo();
            byte[] imagefinale = new byte[header.Length + headerinfo.Length + (hauteur * largeur * 3)];
            for(int k = 0; k < HEADER; k++)
            {
                imagefinale[k] = header[k];
            }
            int indexheaderinfo = 0;
            for (int l = HEADER; l < TAILLE_HEADER_INFO + HEADER; l++)
            {
                imagefinale[l] = headerinfo[indexheaderinfo];
                indexheaderinfo++;
            }
            int indexBinaire = TAILLE_HEADER_INFO + HEADER;
            for (int i = hauteur - 1; i >= 0; i--)
            {
                for (int j = 0; j < largeur; j++)
                {
                    byte bleu = (byte)picture[i, j].Bleu;
                    byte vert = (byte)picture[i, j].Vert;
                    byte rouge = (byte)picture[i, j].Rouge;
                    imagefinale[indexBinaire] = bleu;
                    indexBinaire++;
                    imagefinale[indexBinaire] = vert;
                    indexBinaire++;
                    imagefinale[indexBinaire] = rouge;
                    indexBinaire++;
                }
            }
            File.WriteAllBytes(fichierfinal, imagefinale);
        }
        /// <summary>
        /// La méthode est la même que celle ci-dessus mais enregistre l'image à partir de l'image où le filtre de convolution a été appliqué
        /// </summary>
        /// <param name="fichierfinal"></param>
        public void EnregistrerConvo(string fichierfinal)
        {
            byte[] header = CreerHeader();
            byte[] headerinfo = CreerHeaderInfo();
            byte[] imagefinale = new byte[header.Length + headerinfo.Length + (hauteur * largeur * 3)];
            for (int k = 0; k < HEADER; k++)
            {
                imagefinale[k] = header[k];
            }
            int indexheaderinfo = 0;
            for (int l = HEADER; l < TAILLE_HEADER_INFO + HEADER; l++)
            {
                imagefinale[l] = headerinfo[indexheaderinfo];
                indexheaderinfo++;
            }
            int indexBinaire = TAILLE_HEADER_INFO + HEADER;
            for (int i = hauteur - 1; i >= 0; i--)
            {
                for (int j = 0; j < largeur; j++)
                {
                    byte bleu = (byte)convo[i, j].Bleu;
                    byte vert = (byte)convo[i, j].Vert;
                    byte rouge = (byte)convo[i, j].Rouge;
                    imagefinale[indexBinaire] = bleu;
                    indexBinaire++;
                    imagefinale[indexBinaire] = vert;
                    indexBinaire++;
                    imagefinale[indexBinaire] = rouge;
                    indexBinaire++;
                }
            }
            File.WriteAllBytes(fichierfinal, imagefinale);
        }
        /// <summary>
        /// Créé un nouveau tableau d'octets (la nouvelle image bmp), lui crée un header et un header info, puis
        /// enregistre chaque composante de chaque pixel de l'image où la transformation du photomaton a été appliquée dans ce nouveau tableau, et enfin l'écrit dans un fichier.
        /// </summary>
        /// <param name="fichierfinal"></param>
        public void EnregistrerPhotomaton(string fichierfinal)
        {
            byte[] header = CreerHeader();
            byte[] headerinfo = CreerHeaderInfo();
            byte[] imagefinale = new byte[header.Length + headerinfo.Length + (hauteur * largeur * 3)];
            for (int k = 0; k < HEADER; k++)
            {
                imagefinale[k] = header[k];
            }
            int indexheaderinfo = 0;
            for (int l = HEADER; l < TAILLE_HEADER_INFO + HEADER; l++)
            {
                imagefinale[l] = headerinfo[indexheaderinfo];
                indexheaderinfo++;
            }
            int indexBinaire = TAILLE_HEADER_INFO + HEADER;
            for (int i = hauteur - 1; i >= 0; i--)
            {
                for (int j = 0; j < largeur; j++)
                {
                    byte bleu = (byte)photomat[i, j].Bleu;
                    byte vert = (byte)photomat[i, j].Vert;
                    byte rouge = (byte)photomat[i, j].Rouge;
                    imagefinale[indexBinaire] = bleu;
                    indexBinaire++;
                    imagefinale[indexBinaire] = vert;
                    indexBinaire++;
                    imagefinale[indexBinaire] = rouge;
                    indexBinaire++;
                }
            }
            File.WriteAllBytes(fichierfinal, imagefinale);
        }
        /// <summary>
        /// Créé un nouveau tableau d'octets (la nouvelle image bmp), lui crée un header et un header info, puis
        /// enregistre chaque composante de chaque pixel de l'image où la rotation a 90° a été appliquée dans ce nouveau tableau, et enfin l'écrit dans un fichier.
        /// </summary>
        /// <param name="fichierfinal"></param>
        public void EnregistrerRotation90(string fichierfinal)
        {
            byte[] header = CreerHeader();
            byte[] headerinfo = CreerHeaderInfoRotation();
            byte[] imagefinale = new byte[header.Length + headerinfo.Length + (hauteur * largeur * 3)];
            for (int k = 0; k < HEADER; k++)
            {
                imagefinale[k] = header[k];
            }
            int indexheaderinfo = 0;
            for (int l = HEADER; l < TAILLE_HEADER_INFO + HEADER; l++)
            {
                imagefinale[l] = headerinfo[indexheaderinfo];
                indexheaderinfo++;
            }
            int indexBinaire = TAILLE_HEADER_INFO + HEADER;
            for (int i = largeur - 1; i >= 0; i--)
            {
                for (int j = 0; j < hauteur; j++)
                {
                    byte bleu = (byte)rota90[i, j].Bleu;
                    byte vert = (byte)rota90[i, j].Vert;
                    byte rouge = (byte)rota90[i, j].Rouge;
                    imagefinale[indexBinaire] = bleu;
                    indexBinaire++;
                    imagefinale[indexBinaire] = vert;
                    indexBinaire++;
                    imagefinale[indexBinaire] = rouge;
                    indexBinaire++;
                }
            }
            File.WriteAllBytes(fichierfinal, imagefinale);
        }
        /// <summary>
        /// Créé un nouveau tableau d'octets (la nouvelle image bmp), lui crée un header et un header info, puis
        /// enregistre chaque composante de chaque pixel de l'image où la rotation a 180° a été appliquée dans ce nouveau tableau, et enfin l'écrit dans un fichier.
        /// </summary>
        /// <param name="fichierfinal"></param>
        public void EnregistrerRotation180(string fichierfinal)
        {
            byte[] header = CreerHeader();
            byte[] headerinfo = CreerHeaderInfo();
            byte[] imagefinale = new byte[header.Length + headerinfo.Length + (hauteur * largeur * 3)];
            for (int k = 0; k < HEADER; k++)
            {
                imagefinale[k] = header[k];
            }
            int indexheaderinfo = 0;
            for (int l = HEADER; l < TAILLE_HEADER_INFO + HEADER; l++)
            {
                imagefinale[l] = headerinfo[indexheaderinfo];
                indexheaderinfo++;
            }
            int indexBinaire = TAILLE_HEADER_INFO + HEADER;
            for (int i = hauteur - 1; i >= 0; i--)
            {
                for (int j = 0; j < largeur; j++)
                {
                    byte bleu = (byte)rota180[i, j].Bleu;
                    byte vert = (byte)rota180[i, j].Vert;
                    byte rouge = (byte)rota180[i, j].Rouge;
                    imagefinale[indexBinaire] = bleu;
                    indexBinaire++;
                    imagefinale[indexBinaire] = vert;
                    indexBinaire++;
                    imagefinale[indexBinaire] = rouge;
                    indexBinaire++;
                }
            }
            File.WriteAllBytes(fichierfinal, imagefinale);
        }
        /// <summary>
        /// Créé un nouveau tableau d'octets (la nouvelle image bmp), lui crée un header et un header info, puis
        /// enregistre chaque composante de chaque pixel de l'image où la rotation a 270° a été appliquée dans ce nouveau tableau, et enfin l'écrit dans un fichier.
        /// </summary>
        /// <param name="fichierfinal"></param>
        public void EnregistrerRotation270(string fichierfinal)
        {
            byte[] header = CreerHeader();
            byte[] headerinfo = CreerHeaderInfoRotation();
            byte[] imagefinale = new byte[header.Length + headerinfo.Length + (hauteur * largeur * 3)];
            for (int k = 0; k < HEADER; k++)
            {
                imagefinale[k] = header[k];
            }
            int indexheaderinfo = 0;
            for (int l = HEADER; l < TAILLE_HEADER_INFO + HEADER; l++)
            {
                imagefinale[l] = headerinfo[indexheaderinfo];
                indexheaderinfo++;
            }
            int indexBinaire = TAILLE_HEADER_INFO + HEADER;
            for (int i = largeur - 1; i >= 0; i--)
            {
                for (int j = 0; j < hauteur; j++)
                {
                    byte bleu = (byte)rota270[i, j].Bleu;
                    byte vert = (byte)rota270[i, j].Vert;
                    byte rouge = (byte)rota270[i, j].Rouge;
                    imagefinale[indexBinaire] = bleu;
                    indexBinaire++;
                    imagefinale[indexBinaire] = vert;
                    indexBinaire++;
                    imagefinale[indexBinaire] = rouge;
                    indexBinaire++;
                }
            }
            File.WriteAllBytes(fichierfinal, imagefinale);
        }

        #endregion

        #endregion
    }
}
