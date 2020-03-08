using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD5b
{
    class Program
    {
        static void Main(string[] args)
        {
            Berline b = new Berline("415dcp78", 4, "blanche", VehiculeFamilial.typesBoite.Manuelle);
            Console.WriteLine(b.ToString());
            Console.ReadKey();
        }
    }
}
