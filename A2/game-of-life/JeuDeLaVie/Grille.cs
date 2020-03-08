using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JeuDeLaVie
{
    class Grille
    {
        private int taille;
        private Cellule[,] grilleAncienne;
        private Cellule[,] grilleCourante;
        private const int ENERGIE = 4;
        private const int AGE_MORT = 5;
        private const int ENERGIE_REPRODUCTION = 10;
        private const int ENERGIE_INITIALE = 1;
        private const int NUL = 0;

        #region Constructeurs
        public Grille(int taille)
        {
            this.taille = taille;
            grilleCourante = new Cellule[taille, taille];
            grilleAncienne = new Cellule[taille, taille];
            bool etat = false;
            for (int i = 0; i < grilleCourante.GetLength(0); i++)
            {
                for (int j = 0; j < grilleCourante.GetLength(1); j++)
                {
                    grilleCourante[i, j] = new Cellule(i, j, etat);
                    grilleAncienne[i, j] = new Cellule(i, j, etat);
                }
            }
            grilleCourante[1, 3].Etat = true;
            grilleCourante[2, 1].Etat = true;
            grilleCourante[2, 3].Etat = true;
            grilleCourante[3, 2].Etat = true;
            grilleCourante[3, 3].Etat = true;
        }

        public Grille(int taille, int pourcentage)
        {
            this.taille = taille;
            grilleCourante = new Cellule[taille, taille];
            grilleAncienne = new Cellule[taille, taille];
            int dim = taille * taille;
            int nbcellulesacreer = (dim * pourcentage) / 100;
            //Console.WriteLine(nbcellulesacreer);
            Random rand = new Random();
            int iAlea;
            int jAlea;
            bool etat = false;
            for (int i = 0; i < grilleCourante.GetLength(0); i++)
            {
                for (int j = 0; j < grilleCourante.GetLength(1); j++)
                {
                    grilleCourante[i, j] = new Cellule(i, j, etat);
                    grilleAncienne[i, j] = new Cellule(i, j, etat);
                }
            }
            for (int p = 0; p <= nbcellulesacreer; p++)
            {
                int max = grilleCourante.GetLength(0) - 1;
                int max2 = grilleCourante.GetLength(1) - 1;
                iAlea = rand.Next(0,max);
                jAlea = rand.Next(0,max2);
                grilleCourante[iAlea, jAlea].Etat = true;
                //Console.WriteLine(iAlea + " " + jAlea);
                grilleAncienne[iAlea, jAlea].Etat = true;
            }
        }

        public Grille(int taille, int pourcentage, string libelle, int age, int energie)
        {
            // Je n'ai pas compris ce qu'il fallait faire du libellé
            this.taille = taille;
            grilleCourante = new Cellule[taille, taille];
            grilleAncienne = new Cellule[taille, taille];
            int dim = taille * taille;
            int nbcellulesacreer = (dim * pourcentage) / 100;
            //Console.WriteLine(nbcellulesacreer);
            Random rand = new Random();
            int iAlea;
            int jAlea;
            bool etat = false;
            for (int i = 0; i < grilleCourante.GetLength(0); i++)
            {
                for (int j = 0; j < grilleCourante.GetLength(1); j++)
                {
                    grilleCourante[i, j] = new Cellule(i, j, etat, age, energie);
                    grilleAncienne[i, j] = new Cellule(i, j, etat, age, energie);
                }
            }
            for (int p = 0; p <= nbcellulesacreer; p++)
            {
                int max = grilleCourante.GetLength(0) - 1;
                int max2 = grilleCourante.GetLength(1) - 1;
                iAlea = rand.Next(0, max);
                jAlea = rand.Next(0, max2);
                grilleCourante[iAlea, jAlea].Etat = true;
                //Console.WriteLine(iAlea + " " + jAlea);
                grilleAncienne[iAlea, jAlea].Etat = true;
            }
        }

        public Grille(string file)
        {
            try
            {
                string[] lines = File.ReadAllLines(file);
                int linescount = File.ReadAllLines(file).Length;
                bool verif = true;
                for (int t = 0; t < linescount; t++)
                {
                    int init = lines[0].Length;
                    if (lines[t].Length != init || lines[t].Length != linescount)
                    {
                        verif = false;
                    }
                }
                if (verif == true)
                {
                    this.taille = linescount;
                    grilleCourante = new Cellule[taille, taille];
                    grilleAncienne = new Cellule[taille, taille];
                    for (int i = 0; i < linescount; i++)
                    {
                        for (int j = 0; j < lines[0].Length; j++)
                        {
                            if (lines[i][j] == '1')
                            {
                                grilleCourante[i, j] = new Cellule(i, j, true);
                                grilleAncienne[i, j] = new Cellule(i, j, true);
                            }
                            if (lines[i][j] == '0')
                            {
                                grilleCourante[i, j] = new Cellule(i, j, false);
                                grilleAncienne[i, j] = new Cellule(i, j, false);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Erreur de taille dans le fichier de la matrice");

                }
            }
            catch (FileNotFoundException f)
            {
                Console.WriteLine("Fichier non trouvé");
            }
            /*
            try {
                string[] lines = File.ReadAllLines(file);
                int linescount = File.ReadAllLines(file).Length;
                bool verif = true;
                for (int t = 0; t < linescount; t++)
                {
                    int init = lines[0].Length;
                    if (lines[t].Length != init || lines[t].Length != linescount)
                    {
                        Console.WriteLine("Erreur de taille dans le fichier de la matrice");
                        verif = false;
                    }
                }
                if (verif == true)
                */
        }
        #endregion

        public int NbVoisins(Cellule macase)
        {
            int nbvoisins = 0;
            if (VoisinNord(macase).Etat == true)
            {
                nbvoisins++;
            }
            if (VoisinNordEst(macase).Etat == true)
            {
                nbvoisins++;
            }
            if (VoisinNordOuest(macase).Etat == true)
            {
                nbvoisins++;
            }
            if (VoisinSud(macase).Etat == true)
            {
                nbvoisins++;
            }
            if (VoisinSudEst(macase).Etat == true)
            {
                nbvoisins++;
            }
            if (VoisinSudOuest(macase).Etat == true)
            {
                nbvoisins++;
            }
            if (VoisinOuest(macase).Etat == true)
            {
                nbvoisins++;
            }
            if (VoisinEst(macase).Etat == true)
            {
                nbvoisins++;
            }
            return nbvoisins;
        } // Calcul du nombre de voisins

        #region Affichage
        public void AfficheGrille(char message, int taille)
        {
            for (int i = 0; i < grilleCourante.GetLength(0); i++)
            {
                for (int j = 0; j < grilleCourante.GetLength(1); j++)
                {
                    grilleCourante[i, j].Affiche(message);
                }
                Console.WriteLine();
            }
        } //Affichage de la Grille Courante

        public void AfficheGrilleA(char message, int taille)
        {
            for (int i = 0; i < grilleAncienne.GetLength(0); i++)
            {
                for (int j = 0; j < grilleAncienne.GetLength(1); j++)
                {
                    grilleAncienne[i, j].Affiche(message);
                }
                Console.WriteLine();
            }
        } //Affichage de la Grille Ancienne (utile pour tests)
        #endregion

        public void EcritureGrille(string nomfichier)
        {
            StreamWriter writer = new StreamWriter(nomfichier);
            for (int i = 0; i < grilleCourante.GetLength(0); i++)
            {
                for (int j = 0; j < grilleCourante.GetLength(1); j++)
                {
                    if(grilleCourante[i,j].Etat)
                    {
                        writer.Write(1);
                    }
                    else
                    {
                        writer.Write(0);
                    }
                }
                writer.WriteLine();
            }
            writer.Close();
        } // Écriture de la dernière génération dans un fichier texte (dans Debug)

        #region Voisinage
        public Cellule VoisinNord(Cellule macase)
        {
            int a = macase.Ligne - 1;
            int b = macase.Colonne;
            if (a < 0)
            {
                a = grilleCourante.GetLength(0) - 1;
            }
            Cellule voisinnord = grilleAncienne[a, b];
            return voisinnord;
        } // Retourne le voisin Nord de macase
        public Cellule VoisinNordEst(Cellule macase)
        {
            int a = macase.Ligne - 1;
            int b = macase.Colonne + 1;
            if (a < 0)
            {
                a = grilleCourante.GetLength(0) - 1;
            }
            if (b >= grilleCourante.GetLength(1))
            {
                b = 0;
            }
            Cellule voisinnordest = grilleAncienne[a, b];
            return voisinnordest;
        }
        public Cellule VoisinNordOuest(Cellule macase)
        {
            int a = macase.Ligne - 1;
            int b = macase.Colonne - 1;
            if (a < 0)
            {
                a = grilleCourante.GetLength(0) - 1;
            }
            if (b < 0)
            {
                b = grilleCourante.GetLength(1) - 1;
            }
            Cellule voisinnordouest = grilleAncienne[a, b];
            return voisinnordouest;
        }
        public Cellule VoisinSud(Cellule macase)
        {
            int a = macase.Ligne + 1;
            int b = macase.Colonne;
            if (a >= grilleCourante.GetLength(0))
            {
                a = 0;
            }
            Cellule voisinsud = grilleAncienne[a, b];
            return voisinsud;
        }
        public Cellule VoisinSudEst(Cellule macase)
        {
            int a = macase.Ligne + 1;
            int b = macase.Colonne + 1;
            if (a >= grilleCourante.GetLength(0))
            {
                a = 0;
            }
            if (b >= grilleCourante.GetLength(1))
            {
                b = 0;
            }
            Cellule voisinsudest = grilleAncienne[a, b];
            return voisinsudest;
        }
        public Cellule VoisinSudOuest(Cellule macase)
        {
            int a = macase.Ligne + 1;
            int b = macase.Colonne - 1;
            if (a >= grilleCourante.GetLength(0))
            {
                a = 0;
            }
            if (b < 0)
            {
                b = grilleCourante.GetLength(1) - 1;
            }
            Cellule voisinsudouest = grilleAncienne[a, b];
            return voisinsudouest;
        }
        public Cellule VoisinOuest(Cellule macase)
        {
            int a = macase.Ligne;
            int b = macase.Colonne - 1;
            if (b < 0)
            {
                b = grilleCourante.GetLength(1) - 1;
            }
            Cellule voisinouest = grilleAncienne[a, b];
            return voisinouest;
        }
        public Cellule VoisinEst(Cellule macase)
        {
            int a = macase.Ligne;
            int b = macase.Colonne + 1;
            if (b >= grilleCourante.GetLength(1))
            {
                b = 0;
            }
            Cellule voisinest = grilleAncienne[a, b];
            return voisinest;
        }
        #endregion

        #region Règles
        public Cellule Jeuniveau1(Cellule macase, int cellulesvoisines)
        {
            if (macase.Etat == true)
            {
                if(cellulesvoisines == 2 || cellulesvoisines == 3)
                {
                    macase.Etat = true;
                }
                if (cellulesvoisines >= 4 || cellulesvoisines <= 1)
                {
                    macase.Etat = false;
                }
            }
            if (macase.Etat == false)
            {
                if (cellulesvoisines == 3)
                {
                    macase.Etat = true;
                }
            }
            return macase;
        } // Règles du Jeu de la Vie Niveau 1
        
        public Cellule Jeuniveau2(Cellule macase, int cellulesvoisines)
        {
            if (macase.Etat == true)
            {
                macase.Energiepropre += ENERGIE;
                macase.Age += 1;
                if (cellulesvoisines == 2 || cellulesvoisines == 3)
                {
                    macase.Etat = true;
                }
                if (cellulesvoisines >= 4 || cellulesvoisines <= 1)
                {
                    macase.Etat = false;
                }
                if(macase.Age == AGE_MORT)
                {
                    macase.Etat = false;
                }
                if(macase.Age < AGE_MORT && macase.Energiepropre == ENERGIE_REPRODUCTION)
                {
                    if(cellulesvoisines == 0)
                    {
                        Reproduction(macase);
                        macase.Energiepropre -= ENERGIE_REPRODUCTION;
                    }
                }
            }
            if (macase.Etat == false)
            {
                if (cellulesvoisines == 3)
                {
                    macase.Etat = true;
                }
            }
            return macase;
        } // Règles du Jeu de la Vie Niveau 2
        #endregion

        public void Clone(Cellule[,] grillec)
        {
            for(int i = 0; i < grilleCourante.GetLength(0); i++)
            {
                for(int j = 0; j < grilleCourante.GetLength(1); j++)
                {
                    grilleAncienne[i, j].Clone(grillec[i,j]);
                }
            }
        } // grillec est clonée dans Grille Ancienne (Grille Tampon)

        public void Reproduction(Cellule macase)
        {
            VoisinNord(macase).Etat = true;
            VoisinNord(macase).Age = NUL;
            VoisinNord(macase).Energiepropre = ENERGIE_INITIALE;
            VoisinNordEst(macase).Etat = true;
            VoisinNordEst(macase).Age = NUL;
            VoisinNordEst(macase).Energiepropre = ENERGIE_INITIALE;
            VoisinNordOuest(macase).Etat = true;
            VoisinNordOuest(macase).Age = NUL;
            VoisinNordOuest(macase).Energiepropre = ENERGIE_INITIALE;
            VoisinSud(macase).Etat = true;
            VoisinSud(macase).Age = NUL;
            VoisinSud(macase).Energiepropre = ENERGIE_INITIALE;
            VoisinSudEst(macase).Etat = true;
            VoisinSudEst(macase).Age = NUL;
            VoisinSudEst(macase).Energiepropre = ENERGIE_INITIALE;
            VoisinSudOuest(macase).Etat = true;
            VoisinSudOuest(macase).Age = NUL;
            VoisinSudOuest(macase).Energiepropre = ENERGIE_INITIALE;
            VoisinOuest(macase).Etat = true;
            VoisinOuest(macase).Age = NUL;
            VoisinOuest(macase).Energiepropre = ENERGIE_INITIALE;
            VoisinEst(macase).Etat = true;
            VoisinEst(macase).Age = NUL;
            VoisinEst(macase).Energiepropre = ENERGIE_INITIALE;
        } // Reproduction (Règle du Niveau 2)

        #region Parcours
        public void Parcours(int taille)
        {
            Clone(grilleCourante);
            Cellule cgrillea;
            Cellule courante;
            int nbvoisins;
            //AfficheGrille('0', taille);
            for(int i = 0; i < grilleAncienne.GetLength(0); i++)
            {
                for(int j = 0; j < grilleAncienne.GetLength(1); j++)
                {
                    cgrillea = grilleAncienne[i, j];
                    courante = grilleCourante[i, j];
                    nbvoisins = NbVoisins(cgrillea);
                    Jeuniveau1(courante, nbvoisins);
                }
            }
            AfficheGrille('O', taille);
            Console.WriteLine();
        } // Génération pour le Niveau 1

        public void ParcoursLv2(int taille)
        {
            Clone(grilleCourante);
            Cellule couranteGA;
            Cellule couranteGC;
            int nbvoisins;
            //AfficheGrille('0', taille);
            for (int i = 0; i < grilleAncienne.GetLength(0); i++)
            {
                for (int j = 0; j < grilleAncienne.GetLength(1); j++)
                {
                    couranteGA = grilleAncienne[i, j];
                    couranteGC = grilleCourante[i, j];
                    nbvoisins = NbVoisins(couranteGA);
                    Jeuniveau2(couranteGC, nbvoisins);
                }
            }
            AfficheGrille('O', taille);
            Console.WriteLine();
        } // Génération pour le Niveau 2

        #endregion
    }
}
