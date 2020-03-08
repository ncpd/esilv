using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD5b
{
    abstract class VehiculeFamilial : Vehicule
    {
        protected int nbPlaces;
        protected string couleur;
        protected typesBoite typeBoiteVehicule;

        public enum typesBoite { Manuelle, Automatique };

        public VehiculeFamilial(string immatriculation, int places, string couleur, typesBoite typeBoite) : base(immatriculation)
        {
            nbPlaces = places;
            this.couleur = couleur;
            typeBoiteVehicule = typeBoite;
        }
    }
}
