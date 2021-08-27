using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msc
{
    public class Dso
    {
        public double RA;
        public double Dec;
        public double Mag;
        public string Name;
        public string Id;
        public string Type;
        public string Const;
        public double R1;
        public double R2;
        public double Angle;

        public Dso()
        {
            RA = 0.0;
            Dec = 0.0;
            Mag = 0.0;
            Name = "";
            Id = "";
            Type = "";
            Const = "";
            R1 = 0.0;
            R2 = 0.0;
            Angle = 0.0;
        }

        public Dso(double ra, double dec, double mag, string name, string id, string type, string constellation, double r1, double r2, double angle)
        {
            RA = ra;
            Dec = dec;
            Mag = mag;
            Name = name;
            Id = id;
            Type = type;
            Const = constellation;
            R1 = r1;
            R2 = r2;
            Angle = angle;
        }

        public override string ToString()
        {
            return Id + ' ' + Name + ' ' + RA + ' ' + Dec + ' ' + Mag + ' ' + Type + ' ' + Const + ' ' + R1 + ' ' + R2 + ' ' + Angle;
        }
    }
}
