using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD1
{
    class Program
    {
        static void Main(string[] args)
        {
            Universite esilv = new Universite();
            Initialisation(esilv);
            IHM(esilv);
        }

        static void IHM(Universite universite)
        {
            int choix = 0;
            while (choix != 9)
            {
                Console.WriteLine("choisir une opération");
                Console.WriteLine("---------------------\n");
                Console.WriteLine("Pour un étudiant\n");
                Console.WriteLine("1: Obtenir sa note à un cours");
                Console.WriteLine("2: Valider un diplome");
                Console.WriteLine("---------------------\n");
                Console.WriteLine("Pour un enseignant\n");
                Console.WriteLine("3: Afficher la liste de ses cours");
                Console.WriteLine("4: Consulter la moyenne de ses cours");
                Console.WriteLine("");
                Console.WriteLine("9: fin");
                Console.WriteLine("---------------------");
                Console.Write("quel est votre choix > ");
                choix = Int32.Parse(Console.ReadLine());
                Console.WriteLine("");
                switch (choix)
                {
                    case 1: // Obtenir sa note à un cours
                        ObtenirNote(universite);
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 2: // Valider un diplome
                        ValiderDiplome(universite);
                        break;

                    case 3: //  Afficher la liste de ses cours
                        ListeCours(universite);
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 4: // Consulter la moyenne d'un de ses cours
                        MoyenneCours(universite);
                        break;

                    case 5: //
                        break;

                    case 9: //fin
                        Environment.Exit(1);
                        break;
                }
            }
        }
        static void ObtenirNote(Universite universite)
        {
            Console.Clear();
            // obtenir la liste des étudiants (depuis l'universite)
            Etudiant[] listeElevesUni = universite.ListeEtudiants;
            Etudiant e = null;
            Cours d = null;
            int note = -1;
            // choisir l'étudiant par son identifiant (depuis la console)
            Console.WriteLine("Entrez votre matricule (n° étudiant) ");
            int id = Convert.ToInt32(Console.ReadLine());
            if(universite.studentIsInUni(id) > -1)
            {
                e = listeElevesUni[universite.studentIsInUni(id)];
                // obtenir la liste ses cours (depuis l'étudiant concerné)
                Cours[] listeCours = e.ListeCours;
                // choisir un cours (depuis la console)
                if(listeCours != null)
                {
                    // demander au cours concerné la note de cet étudiant
                    Console.WriteLine("Entrez le cours dans lequel vous souhaitez connaitre votre note :");
                    string cours = Console.ReadLine();
                    if (e.estInscrit(cours) > -1)
                    {
                        d = e.ListeCours[e.estInscrit(cours)]; // On accède au cours demandé
                        if(d != null)
                        {
                            note = d.getNote(e);
                            // afficher la note
                            if (note != -1)
                            {
                                Console.WriteLine("Vous avez obtenu " + note + "/20");
                            } else
                            {
                                Console.WriteLine("N/A");
                            }
                        }
                    } else
                    {
                        Console.WriteLine("Vous n'êtes pas inscrit à ce cours ou il n'existe pas.");
                    }
                } else
                {
                    Console.WriteLine("Vous n'êtes pas encore inscrit à un cours.");
                }
            }
            else
            {
                Console.WriteLine("Le numéro de matricule est erroné.");
            }
        }

        static void ValiderDiplome(Universite universite)
        {
            // à compléter par vos soins:
            // --------------------------
            //
            //
        }

        static void ListeCours(Universite universite)
        {
            Console.Clear();
            // obtenir la liste des profs (depuis l'universite)
            Professeur[] listeProfsUni = universite.ListeProfs;
            Professeur p = null;
            // choisir le professeur par son identifiant (depuis la console)
            Console.WriteLine("Entrez votre matricule (n° professeur) ");
            int id = Convert.ToInt32(Console.ReadLine());
            if (universite.profIsInUni(id) > -1)
            {
                p = listeProfsUni[universite.profIsInUni(id)];
                // obtenir la liste des cours (depuis le professeur concerné)
                Cours[] listeCours = p.ListeCours;
                if(listeCours != null)
                {
                    Console.WriteLine("Vos cours sont les suivants :");
                    for (int i = 0; i < listeCours.Length; i++)
                    {
                        if(i == listeCours.Length - 1)
                        {
                            Console.Write(listeCours[i].Matiere);
                        } else
                        {
                            Console.Write(listeCours[i].Matiere + " | ");
                        }
                    }
                } else
                {
                    Console.WriteLine("Vous n'êtes pas encore assigné à un cours.");
                }
            } else
            {
                Console.WriteLine("Le numéro de matricule est erroné.");
            }
        }

        static void MoyenneCours(Universite universite)
        {
            // à compléter par vos soins:
            // --------------------------
            //
            //
        }

        static void Initialisation(Universite universite)
        {
            universite.Nom = "ESILV Paris";
            
            Etudiant martin = new Etudiant("PINTIAU", "Martin", 1, null, null);
            Etudiant charles = new Etudiant("RONTEIX", "Charles", 2, null, null);
            Etudiant nicolas = new Etudiant("PICARD", "Nicolas", 3, null, null);
            Etudiant anton = new Etudiant("PINAUD", "Anton", 4, null, null);
            Etudiant florian = new Etudiant("PRIGENT", "Florian", 5, null, null);
            universite.ListeEtudiants = new Etudiant[] { martin, charles, nicolas, anton, florian };
            int[] listeEtu = new int[] { martin.Id, charles.Id, nicolas.Id, anton.Id, florian.Id };
            int[] listeNotesMeca = new int[] { 12, 11, 14, 12, 13 };
            int[] listeNotesMaths = new int[] { 6, 1, 4, 2, 3 };


            Professeur no1 = new Professeur("BON", "Jean", 1, null);
            Professeur no2 = new Professeur("COT", "Harry", 2, null);
            universite.ListeProfs = new Professeur[] { no1, no2 };

            Cours meca = new Cours("Mécanique", no1, listeEtu, listeNotesMeca, "12:00", "01/02/2018", "L102", null);
            Cours maths = new Cours("Maths", no2, listeEtu, listeNotesMaths, "16:00", "01/02/2018", "L108", null);

            nicolas.ListeCours = new Cours[] { maths, meca };
            martin.ListeCours = new Cours[] { maths, meca };
            charles.ListeCours = new Cours[] { maths, meca };
            anton.ListeCours = new Cours[] { maths, meca };
            florian.ListeCours = new Cours[] { maths, meca };

            no1.ListeCours = new Cours[] { meca };
            no2.ListeCours = new Cours[] { maths };


            universite.ListeSalles = new string[] { "L102", "E118" };
        }

    }
}
