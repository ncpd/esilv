using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace TD7
{
    public class BD
    {
        private string isbn;
        private string titre;
        private int nombrePages; // valeur -1 par défaut (cf. les constructeurs où information non renseignée en paramètre)

        public BD(string isbn, string titre, int nombrePages)
        {
            this.isbn = isbn;
            this.titre = titre;
            this.nombrePages = nombrePages;
        }

        public BD(string isbn, string titre)
            : this(isbn, titre, -1)
        {
        }


        #region Constructeur "vide" + Propriétés accessibles publiquement en get et en set pour une (dé)sérialisation via XmlSerializer

        // Attention : pour qu'un objet puisse être (dé)sérialisable via XmlSerializer, 
        //             la classe DOIT avoir un constructeur public "vide" (i.e sans paramètre).
        // ... même si cela n'est pas terrible ici d'un point de vue purement POO, car la création d"une BD sans identifiant et sans titre n'a pas de sens :-(
        public BD()
            : this("N/C", "N/C")  // N/C : Non Communiqué
        {
        }

        // Lors de la (dé)sérialisation de l'objet
        // seuls les champs totalement publics (ici les propriétés publiques en get ET AUSSI en set !) 
        // vont être utilisés pour la sérialisation / désérialisation      
        // ... même si cela n'est pas terrible ici d'un point de vue purement POO de mettre tout accessible en get ET en set :-( 
        public string ISBN
        {
            get { return isbn; }
            set { isbn = value; }  // Attention : accès en écriture nécessaire (via XmlSerializer), même si seulement pour sérialisation :-(
        }
        public string Titre
        {
            get { return titre; }
            set { titre = value; }
        }
        public int NombrePages
        {
            get { return nombrePages; }
            set { nombrePages = value; }
        }

        #endregion


        override public string ToString()
        {
            string nbPages = "";
            if (nombrePages > 0)
            {
                nbPages = " (" + Convert.ToString(nombrePages) + " pages)";
            }

            return titre + nbPages + ", ISBN : " + isbn;
        }
    }
}
