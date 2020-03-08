using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD5b
{
    class Berline : VehiculeFamilial, IAmeliorable
    {
        public Berline(string immatriculation, int nbPlaces, string couleur, typesBoite boite) : base(immatriculation, nbPlaces, couleur, boite)
        {

        }

        public void Ameliorer()
        {
            Console.WriteLine("Améliioration en cours");
        }

        public override string ToString()
        {
            return "Cette berline immatriculée " + immat + " possède " + nbPlaces + ", est de couleur " + couleur + " et possède une boite " + typeBoiteVehicule;
        }

    }
}
