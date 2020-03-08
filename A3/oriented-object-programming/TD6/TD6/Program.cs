using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TD6
{
    class Program
    {
        private const string PATH_LISTE1 = "C:\\Users\\Nicolas\\Documents\\ESILV\\3A\\Semestre 6\\POOI\\TD6\\liste1.txt";
        private const string PATH_LISTE2 = "C:\\Users\\Nicolas\\Documents\\ESILV\\3A\\Semestre 6\\POOI\\TD6\\liste2.txt";

        static void LectureFichier(string path)
        {
            StreamReader monStreamReader = new StreamReader(path);
            string ligne = monStreamReader.ReadLine();

            while (ligne != null)
            {
                string[] temp = ligne.Split(',');
                if (temp[0] == "Fixe")
                {
                    Console.WriteLine("TelephoneFixe ");
                    foreach (string val in temp)
                    {
                        Console.WriteLine("     " + val);
                    }
                }
                else
                {
                    Console.WriteLine("TelephonePortable ");
                    Console.WriteLine("     " + temp[0]);
                    Console.WriteLine("     " + temp[1]);
                    Console.WriteLine("     " + temp[2]);
                    Console.WriteLine("     " + temp[3]);
                }
                ligne = monStreamReader.ReadLine();
            }
            monStreamReader.Close();
        }

        static List<Telephone> CreateList(string path)
        {
            List<Telephone> tels = new List<Telephone>();
            StreamReader monStreamReader = new StreamReader(path);
            string ligne = monStreamReader.ReadLine();
            int lineCounter = 1;

            while (ligne != null)
            {
                string[] temp = ligne.Split(',');
                try
                {
                    if (temp[0] == "Fixe")
                    {
                        TelephoneFixe fixe = new TelephoneFixe(temp[1], temp[2], temp[3]);
                        tels.Add(fixe);
                    }
                    else
                    {
                        TelephonePortable portable = new TelephonePortable(temp[1], temp[2], temp[3]);
                        tels.Add(portable);
                    }
                    ligne = monStreamReader.ReadLine();
                    lineCounter++;
                }
                catch(IndexOutOfRangeException e)
                {
                    Console.WriteLine("Une erreur est survenue. Merci de vérifier que le fichier est bien formaté à la ligne " + lineCounter);
                    ligne = monStreamReader.ReadLine();
                }
            }
            monStreamReader.Close();
            return tels;
        }

        static void deleteWrongNumbers(List<Telephone> tels)
        {
            for(int i = 0; i < tels.Count; i++)
            {
                if(tels.ElementAt(i).Numero.Length != 10)
                {
                    tels.RemoveAt(i);
                }
            }
        }

        static void sortByNumber(List<Telephone> tels)
        {
            if(tels != null)
            {
                tels.Sort();
            }
        }

        static void sortByBrand(List<Telephone> tels)
        {
            if(tels != null)
            {
                tels.Sort(delegate (Telephone p1, Telephone p2)
                {
                    return p1.Marque.CompareTo(p2.Marque);
                });
            }
        }

        static void sortByProprietaire(List<Telephone> tels)
        {
            
        }

        static void Main(string[] args)
        {
            List<Telephone> liste1 = CreateList(PATH_LISTE1);
            List<Telephone> liste2 = CreateList(PATH_LISTE2);
            
            List<Telephone> merged = liste1.Union(liste2).ToList();

            merged.Sort();
            merged.Sort()
            /*
            deleteWrongNumbers(merged);

            Dictionary<String, String> annuaire = new Dictionary<string, string>();
            foreach(Telephone t in merged)
            {
                if(t is TelephoneFixe)
                {
                    TelephoneFixe fixe = (TelephoneFixe)t;
                    annuaire.Add(fixe.Bureau, fixe.Numero);
                } else
                {
                    TelephonePortable fixe = (TelephonePortable)t;
                    annuaire.Add(fixe.NomProprietaire, fixe.Numero);
                }
            }

            sortByBrand(merged);

            if (merged != null)
            {
                foreach (Telephone tel in merged)
                {
                    Console.WriteLine(tel.ToString());
                }
            }

            foreach(KeyValuePair<string, string> kvp in annuaire) {
                Console.WriteLine("<" + kvp.Key + "," + kvp.Value + ">");
            }*/

            Console.ReadKey();
        }
    }
}
