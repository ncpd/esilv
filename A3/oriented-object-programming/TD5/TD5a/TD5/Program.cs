using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD5
{
    class Program
    {
        static void Main(string[] args)
        {
            Livre l = new Livre("Jean", 120, "Bondour", 1);
            Manuel m = new Manuel("Karine", 45, "Salut les maths", 2, 1);
            Roman r = new Roman("Yoko", 65, "Thriller", 3, "Nobel");
            Etagere e = new Etagere();
            e.ajouterOuvrage(m);
            e.ajouterOuvrage(l);
            e.ajouterOuvrage(r);
            e.Imprimer(1);
            e.Imprimer(2);
            e.Imprimer(3);
            e.Imprimer(8);
            Console.ReadKey();
        }
    }
}
