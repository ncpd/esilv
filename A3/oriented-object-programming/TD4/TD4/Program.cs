using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TD4
{
    class Program
    {
        const String fichier = "C:\\Users\\Nicolas\\Documents\\ESILV\\3A\\Semestre 6\\POOI\\TD4\\exo1.txt";
        const String annuaire = "C:\\Users\\Nicolas\\Documents\\ESILV\\3A\\Semestre 6\\POOI\\TD4\\exo7.txt";

        #region Exos Obligatoires

        static void exo1Lecture()
        {
            StreamReader sr = new StreamReader(fichier, true);
            while(sr.Peek() > 0)
            {
                Console.WriteLine(sr.ReadLine());
            }
            sr.Close();
        }

        static void exo1Ecriture()
        {
            if (File.Exists(fichier))
            {
                File.Delete(fichier);
            }

            StreamWriter sw = File.AppendText(fichier);
            sw.WriteLine("Blablablabla");
            sw.WriteLine("Hohohohohohoh");
            sw.Close();
        }

        static void exo1ReadUntilFin()
        {
            if (File.Exists(fichier))
            {
                File.Delete(fichier);
            }
            Console.WriteLine("Ecrivez :");
            string input = Console.ReadLine();
            StreamWriter sw = File.AppendText(fichier);
            while(input != "fin")
            {
                sw.WriteLine(input);
                Console.WriteLine("Ecrivez :");
                input = Console.ReadLine();
            }
            sw.Close();

        }

        static void exo2()
        {
            String[] tab = new String[100];
            exo1ReadUntilFin();
            StreamReader sr = new StreamReader(fichier, true);
            int i = 0;
            String srd = sr.ReadLine();
            while(srd != ".")
            {
                tab[i] = srd;
                srd = sr.ReadLine();
                i++;
            }
            sr.Close();
            int j = 0;
            while(tab[j] != null)
            {
                Console.Write(tab[j] + " ");
                j++;
            }

        }

        static void exo3Stack()
        {
            Stack s = new Stack();
            exo1ReadUntilFin();
            StreamReader sr = new StreamReader(fichier, true);
            String srd = sr.ReadLine();
            while (srd != ".")
            {
                s.Push(srd);
                srd = sr.ReadLine();
            }
            sr.Close();
            Stack reverse = new Stack();
            int k = 0;
            string spop;
            int sizeTemp = s.Count;
            while(k < sizeTemp)
            {
                spop = (string) s.Pop();
                reverse.Push(spop);
                k++;
            }
            int j = 0;
            while (j < sizeTemp)
            {
                Console.Write((string)reverse.Pop() + " ");
                j++;
            }
        }

        static void exo3Queue()
        {
            Queue q = new Queue();
            exo1ReadUntilFin();
            StreamReader sr = new StreamReader(fichier, true);
            String srd = sr.ReadLine();
            while (srd != ".")
            {
                q.Enqueue(srd);
                srd = sr.ReadLine();
            }
            sr.Close();
            int j = 0;
            int sizeTemp = q.Count;
            while (j < sizeTemp)
            {
                Console.Write((String) q.Dequeue() + " ");
                j++;
            }
        }

        static void exo5()
        {
            Console.WriteLine("Entrez une phrase :");
            String input = Console.ReadLine();
            String[] phrase = input.Split(' ');
            for(int i = 0; i < phrase.Length; i++)
            {
                Console.WriteLine(phrase[i]);
            }
        }

        static char EncodeCaractere(char caractere, char encodeur)
        {
            int asciiCaractere = (int)caractere;
            int asciiEncodeur = (int)encodeur;
            int asciiCaractereCode = ((asciiCaractere - 97) + (asciiEncodeur - 97)) % 26;
            char caractereCode = (char)(asciiCaractereCode + 97);

            return caractereCode;
        }

        static char DecodeCaractere(char caractere, char encodeur)
        {
            int asciiCaractere = (int)caractere;
            int asciiEncodeur = (int)encodeur;
            int asciiCaractereCode = ((asciiCaractere - 97) - (asciiEncodeur - 97)) % 26;
            char caractereCode = (char)(asciiCaractereCode + 97);

            return caractereCode;
        }

        static String EncodeMot(String mot, Char encodeur)
        {
            char[] retour = new char[mot.Length];
            for(int i = 0; i < retour.Length; i++)
            {
                retour[i] = EncodeCaractere(mot[i], encodeur);
            }
            string str = new string(retour);
            return str;
        }

        static String DecodeMot(String mot, Char encodeur)
        {
            char[] retour = new char[mot.Length];
            for (int i = 0; i < retour.Length; i++)
            {
                retour[i] = DecodeCaractere(mot[i], encodeur);
            }
            string str = new string(retour);
            return str;
        }

        static String EncodePhrase(String phrase, Char encodeur)
        {
            String[] ph = phrase.Split(' ');
            String[] retour = ph;
            for (int i = 0; i < ph.Length; i++)
            {
                retour[i] = EncodeMot(ph[i], encodeur);
            }
            StringBuilder builder = new StringBuilder();
            foreach(string str in retour)
            {
                builder.Append(str);
                builder.Append(" ");
            }
            return builder.ToString();
        }

        static String DecodePhrase(String phrase, Char encodeur)
        {
            String[] ph = phrase.Split(' ');
            String[] retour = ph;
            for (int i = 0; i < ph.Length; i++)
            {
                retour[i] = DecodeMot(ph[i], encodeur);
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in retour)
            {
                builder.Append(str);
                builder.Append(" ");
            }
            return builder.ToString();
        }

        static void CesarEncode()
        {
            String userInput = Console.ReadLine();
            String crypt = EncodePhrase(userInput, 'c');
            Console.WriteLine("La phrase cryptée est la suivante :");
            Console.WriteLine(crypt);
        }

        static void CesarDecode()
        {
            StreamReader sr = new StreamReader(fichier, true);
            StringBuilder builder = new StringBuilder();
            while (sr.Peek() > 0)
            {
                builder.Append(sr.ReadLine());
                builder.Append(" ");
            }
            sr.Close();
            String code = builder.ToString();
            String dcode = DecodePhrase(code, 'c');
            Console.WriteLine("La phrase décryptée est la suivante :");
            Console.WriteLine(dcode);

        }

        #endregion

        #region Exo7
           
        static SortedList<string, string> init()
        {
            StreamReader sr = new StreamReader(annuaire, true);
            SortedList<string, string> individus = new SortedList<string, string>();
            while (sr.Peek() > 0)
            {
                String infos = sr.ReadLine();
                String[] infosSeparees = infos.Split(' ');
                
                individus.Add(infosSeparees[0], infosSeparees[1]);
            }
            sr.Close();
            return individus;
        }

        static void ajouterIndividu(SortedList<string, string> sl)
        {
            Console.WriteLine("Entrez un numéro :");
            String num = Console.ReadLine();
            Console.WriteLine("Entrez un nom :");
            String name = Console.ReadLine();
            sl.Add(num, name);
        }

        static void chercherIndividu(SortedList<string, string> sl)
        {
            Console.WriteLine("Entrez un numéro :");
            String num = Console.ReadLine();
            if (sl.ContainsKey(num))
            {
                int index = sl.IndexOfKey(num);
                Console.WriteLine(sl.ElementAt(index).Value);
            } else
            {
                Console.WriteLine("Personne n'est enregistré à ce numéro.");
            }
        }

        static void saveToTxt(SortedList<string, string> sl)
        {
            if (File.Exists(annuaire))
            {
                File.Delete(annuaire);
            }

            StreamWriter sw = File.AppendText(annuaire);
            for(int i = 0; i < sl.Count; i++)
            {
                sw.WriteLine(sl.ElementAt(i).Key + " " + sl.ElementAt(i).Value);
            }
            sw.Close();
        }

        static void supprimerIndividu(SortedList<string, string> sl)
        {
            Console.WriteLine("Entrez un numéro :");
            String num = Console.ReadLine();
            if (sl.ContainsKey(num))
            {
                int index = sl.IndexOfKey(num);
                String name = sl.ElementAt(index).Value;
                sl.RemoveAt(index);
                Console.WriteLine(name + " a bien été supprimé de l'annuaire.");
            }
            else
            {
                Console.WriteLine("Personne n'est enregistré à ce numéro.");
            }
        }

        #endregion
        static void Main(string[] args)
        {
            //exo1Ecriture();
            //exo1Lecture();
            //exo1ReadUntilFin();
            //exo1Lecture();
            //exo2();
            //exo3Stack();
            //exo3Queue();
            //exo5();
            //CesarEncode();
            //CesarDecode();

            SortedList<string, string> sl = init();
            ajouterIndividu(sl);
            chercherIndividu(sl);
            saveToTxt(sl);
            supprimerIndividu(sl);
            saveToTxt(sl);
            Console.ReadKey();
        }
    }
}
