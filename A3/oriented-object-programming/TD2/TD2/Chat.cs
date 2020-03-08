using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD2
{
    class Chat
    {
        private String nom;
        private double poids;
        private Boolean doitFaireUnRegime;
        protected Random rndNumber = new Random();

        public Chat(String nom)
        {
            this.nom = nom;
            this.poids = RandomNumberBetween(3.0, 9.0);
            this.doitFaireUnRegime = (poids > 6.0) ? true : false;
        }

        public String Nom
        {
            get
            {
                return nom;
            }
        }

        public double Poids
        {
            get
            {
                return poids;
            }
            set
            {
                this.poids = value;
            }
        }

        public Boolean DoitFaireUnRegime
        {
            get
            {
                return doitFaireUnRegime;
            }
        }

        private double RandomNumberBetween(double min, double max)
        {
            double next = rndNumber.NextDouble();
            return min + (next * (max - min));
        }

        public override string ToString()
        {
            String regime = (doitFaireUnRegime) ? "est au régime" : "n'est pas au régime";
            return "Le chat " + nom + " pèse " + poids.ToString("N2") + "kg et " + regime + ". Il a besoin de " + Croquettes() + " gr de croquettes par jour.";
        }

        public double Croquettes()
        {
            if (doitFaireUnRegime)
            {
                return Math.Round((poids * 20) * 0.85);
            } else
            {
                return Math.Round(poids * 20);
            }
        }
    }
}
