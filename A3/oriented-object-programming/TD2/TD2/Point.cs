using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD2
{
    class Point
    {
        private double x;
        private double y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                this.x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                this.y = value;
            }
        }

        public void Translater(double dx, double dy)
        {
            this.x += dx;
            this.y += dy;
        }

        public double DistanceOrigine()
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }
}
