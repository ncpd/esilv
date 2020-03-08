using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD6
{
    class TelephonePortable : Telephone, IComparable<TelephonePortable>
    {
        private string nomProprietaire;

        public TelephonePortable(string marque, string numero, string nomProprio) : base(marque, numero)
        {
            nomProprietaire = nomProprio;
        }

        public string NomProprietaire
        {
            get
            {
                return nomProprietaire;
            }
        }

        public int CompareTo(TelephonePortable other)
        {
            if (nomProprietaire.CompareTo(other.nomProprietaire) > 0)
                return 1;
            if (nomProprietaire.CompareTo(other.nomProprietaire) < 0)
                return -1;
            else
                return 0;
        }

        public override string ToString()
        {
            return base.ToString() + " Il appartient à " + nomProprietaire;
        }

        
    }
}
