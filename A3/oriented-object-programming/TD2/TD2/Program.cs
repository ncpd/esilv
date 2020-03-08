using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD2
{
    class Program
    {

        static void exoPersonne()
        {
            Personne p1;
            p1 = new Personne("PICARD", "Nicolas", true, 1996, "en couple");
            Console.WriteLine(p1.Message());
            Personne p2 = new Personne("BON", "Jean", true, 1978);
            Console.WriteLine(p2.Message());
            Console.WriteLine(p1.ToString());
            PersonnePositionnee p3 = new PersonnePositionnee("PINTIAU", "Martin", true, 1997, "célibataire", 2.265, 5.659);
            Console.WriteLine(p3.ToString());
            Console.WriteLine("Distance depuis origine : " + p3.Position.DistanceOrigine());
            p3.Position.Translater(2, 2);
            Console.WriteLine(p3.ToString());
            Console.WriteLine("Distance depuis origine : " + p3.Position.DistanceOrigine());
        }

        static void exoChat()
        {
            Chat kira = new Chat("Kira");
            Console.WriteLine(kira.ToString());
        }
        static void Main(string[] args)
        {
            exoChat();
            Console.ReadKey();
        }
    }
}
