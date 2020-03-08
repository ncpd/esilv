using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD5b
{
    abstract class Vehicule
    {
        protected String immat;
        protected Boolean hasClim;

        public Vehicule(string immat)
        {
            this.immat = immat;
        }

        public string Immat
        {
            get
            {
                return this.immat;
            }
        }
    }
}
