using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD6
{
    class TelephoneFixe : Telephone, IComparable
    {
        private string bureau;

        public TelephoneFixe(string marque, string numero, string bureau) : base(marque, numero)
        {
            this.bureau = bureau;
        }

        public string Bureau
        {
            get
            {
                return bureau;
            }
        }

        public override string ToString()
        {
            return base.ToString() + " Il est situé dans le bureau " + bureau;
        }

        public int CompareTo(object tel)
        {
            if (tel == null) return 1;

            TelephoneFixe otherTel = tel as TelephoneFixe;
            if (otherTel != null)
                return this.bureau.CompareTo(otherTel.bureau);
            else
                throw new ArgumentException("Object is not a Temperature");
        }

        public int comparerAutre(object tel)
        {
            if (tel == null) return 1;

            TelephoneFixe otherTel = tel as TelephoneFixe;
            if (otherTel != null)
                return this.bureau.CompareTo(otherTel.bureau);
            else
                throw new ArgumentException("Object is not a Temperature");
        }
    }
}
