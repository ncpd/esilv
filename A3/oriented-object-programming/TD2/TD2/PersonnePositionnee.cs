using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD2
{
    class PersonnePositionnee : Personne
    {

        private Point position;
        public PersonnePositionnee(string nom, string prenom, bool sexe, int anneeNaiss, string statutFamilial, double x, double y) : base(nom, prenom, sexe, anneeNaiss, statutFamilial)
        {
            this.position = new Point(x, y);
        }

        public Point Position
        {
            get
            {
                return position;
            }
            set
            {
                this.position = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() + ", et se situe en ( " + position.X + " ; " + position.Y + " ).";
        }
    }
}
