using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msc
{
    public class Star
    {
        public double Ra;
        public double Dec;
        public double Mag;

        public Star()
        {
            Ra = 0.0;
            Dec = 0.0;
            Mag = 0.0;
        }

        public Star(double ra, double dec, double mag)
        {
            Ra = ra;
            Dec = dec;
            Mag = mag;
        }

        public override string ToString()
        {
            return Ra.ToString() + ' ' + Dec.ToString() + ' ' + Mag.ToString();
        }
    }
}
