using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD6
{
    class Telephone : IComparable<Telephone>
    {
        protected string marque;
        protected string numero;

        public Telephone(string marque, string num)
        {
            this.marque = marque;
            numero = num;
        }

        public string Marque
        {
            get
            {
                return marque;
            }
        }

        public string Numero
        {
            get
            {
                return numero;
            }
        }

        public int CompareTo(Telephone other)
        {
            if (numero.CompareTo(other.numero) > 0)
                return 1;
            if (numero.CompareTo(other.numero) < 0)
                return -1;
            else
                return 0;
        }

        public override string ToString()
        {
            return "Ce téléphone est de marque " + marque + " et son numéro est " + numero + ".";
        }
    }
}
