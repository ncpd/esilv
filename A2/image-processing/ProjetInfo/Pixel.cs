using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetInfo
{
    class Pixel
    {
        private int rouge;
        private int vert;
        private int bleu;

        #region Propriétés

        public int Rouge
        {
            get
            {
                return rouge;
            }

            set
            {
                rouge = value;
            }
        }

        public int Vert
        {
            get
            {
                return vert;
            }

            set
            {
                vert = value;
            }
        }

        public int Bleu
        {
            get
            {
                return bleu;
            }

            set
            {
                bleu = value;
            }
        }

        #endregion

        #region Constructeur

        public Pixel(int blue, int green, int red)
        {
            this.rouge = red;
            this.vert = green;
            this.bleu = blue;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Opère une moyenne des composantes du pixel pour obtenir un niveau de gris et l'applique à chaque composante du pixel
        /// </summary>
        public void NiveauDeGris()
        {
            int gris = (this.rouge + this.vert + this.bleu) / 3;
            this.rouge = gris;
            this.vert = gris;
            this.bleu = gris;
        }

        /// <summary>
        /// Opère une moyenne des composantes du pixel et selon la valeur de la nuance de gris applique 255 à chaque composante (blanc)
        /// ou applique 0 à chaque composante (noir)
        /// </summary>
        public void NoirEtBlanc()
        {
            int couleur = (this.rouge + this.vert + this.bleu) / 3;
            if(couleur > 128)
            {
                this.rouge = 255;
                this.vert = 255;
                this.bleu = 255;
            }
            else
            {
                this.rouge = 0;
                this.vert = 0;
                this.bleu = 0;
            }
        }

        /// <summary>
        /// La méthode Negatif récupère l'inverse des composante du pixel (le reste) et l'applique au pixel
        /// </summary>
        public void Negatif()
        {
            int redNeg = 255 - this.rouge;
            this.rouge = redNeg;
            int vertNeg = 255 - this.vert;
            this.vert = vertNeg;
            int bleuNeg = 255 - this.bleu;
            this.bleu = bleuNeg;
        }

        /// <summary>
        /// Récupère la nuance sépia de chaque composante d'un pixel, les met à 255 en cas de dépassement
       ///  puis les applique à chaque composante
        /// </summary>
        public void Sepia()
        {
            int rsep = (int)((0.393 * this.rouge) + (0.769 * this.vert) + (0.189 * this.bleu));
            int vsep = (int)((0.349 * this.rouge) + (0.686 * this.vert) + (0.168 * this.bleu));
            int bsep = (int)((0.272 * this.rouge) + (0.534 * this.vert) + (0.131 * this.bleu));
            if (rsep > 255)
            {
                rsep = 255;
            }
            if(vsep > 255)
            {
                vsep = 255;
            }
            if(bsep > 255)
            {
                bsep = 255;
            }
            this.rouge = rsep;
            this.vert = vsep;
            this.bleu = bsep;

        }

        /// <summary>
        /// Applique 255 à la composante rouge d'un pixel et 0 au bleu et au vert
        /// </summary>
        public void DessinerRouge()
        {
            this.rouge = 255;
            this.vert = 0;
            this.bleu = 0;
        }

        /// <summary>
        /// Applique 255 à la composante verte d'un pixel et 0 au bleu et au rouge 
        /// </summary>
        public void DessinerVert()
        {
            this.rouge = 0;
            this.vert = 255;
            this.bleu = 0;
        }

        /// <summary>
        /// Applique 255 à la composante bleue d'un pixel et 0 au rouge et au vert
        /// </summary>
        public void DessinerBleu()
        {
            this.rouge = 0;
            this.vert = 0;
            this.bleu = 255;
        }

        /// <summary>
        /// Applique 0 aux 3 composantes d'un pixel
        /// </summary>
        public void DessinerNoir()
        {
            this.rouge = 0;
            this.vert = 0;
            this.bleu = 0;
        }

        /// <summary>
        /// Applique 255 aux 3 composantes d'un pixel
        /// </summary>
        public void DessinerBlanc()
        {
            this.rouge = 255;
            this.vert = 255;
            this.bleu = 255;
        }

        /// <summary>
        /// Copie les composantes du pixel A dans le pixel actuel
        /// </summary>
        /// <param name="A"> Pixel à copier </param>
        public void CopyPixel(Pixel A)
        {
            this.rouge = A.rouge;
            this.vert = A.vert;
            this.bleu = A.bleu;
        }

        /// <summary>
        /// Applique une valeur à la composante rouge d'un pixel
        /// </summary>
        /// <param name="value"> valeur du rouge à appliquer </param>
        public void SetRedValue(int value)
        {
            if (value > 255)
            {
                this.rouge = 255;
            }
            if(value < 0)
            {
                this.rouge = 0;
            }
            else
            {
                this.rouge = value;
            }
        }

        /// <summary>
        /// Applique une valeur à la composante verte d'un pixel
        /// </summary>
        /// <param name="value"> valeur du vert à appliquer </param>
        public void SetGreenValue(int value)
        {
            if (value > 255)
            {
                this.vert = 255;
            }
            if (value < 0)
            {
                this.vert = 0;
            }
            else
            {
                this.vert = value;
            }
        }

        /// <summary>
        /// Applique une valeur à la composante bleue d'un pixel
        /// </summary>
        /// <param name="value"> valeur du bleu à appliquer </param>
        public void SetBlueValue(int value)
        {
            if (value > 255)
            {
                this.bleu = 255;
            }
            if (value < 0)
            {
                this.bleu = 0;
            }
            else
            {
                this.bleu = value;
            }
        }
        #endregion

    }
}
