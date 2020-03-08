using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetInfo
{
    class Complexe
    {
        private double re; //partie réelle
        private double im; //partie imaginaire

        #region Propriétés

        public double Re
        {
            get
            {
                return re;
            }

            set
            {
                re = value;
            }
        }

        public double Im
        {
            get
            {
                return im;
            }

            set
            {
                im = value;
            }
        }

        #endregion

        #region Constructeur

        public Complexe(double preelle,double pimagin)
        {
            this.re = preelle;
            this.im = pimagin;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Calcule le module d'un complexe
        /// </summary>
        /// <returns></returns>
        public double Module()
        {
            return Math.Sqrt(((this.re * this.re) + (this.im * this.im)));
        }

        #endregion

    }
}
