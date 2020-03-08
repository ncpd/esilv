using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium
{
    class Program
    {
        static void Main(string[] args)
        {
            Administration admin = new Administration();
            admin.ReadCSV("Listing.csv");
            admin.drawAscii();
            admin.LaunchDemo();
            Console.ReadKey();
        }
    }
}
