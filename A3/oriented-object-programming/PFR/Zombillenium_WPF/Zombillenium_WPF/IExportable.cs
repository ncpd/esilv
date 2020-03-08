using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombillenium_WPF
{
    interface IExportable
    {
        /// <summary>
        /// Renvoie une chaine de caractère représentant l'objet actuel en format CSV
        /// </summary>
        /// <returns>Chaine de caractère</returns>
        String GetCSVline();
    }
}
