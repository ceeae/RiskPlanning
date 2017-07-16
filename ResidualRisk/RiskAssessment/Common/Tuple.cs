using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcoloRischioResiduo.RiskAssessment.Common
{
    public class Tuble : List<double>
    {

        public Tuble(int capacity) : base(capacity)
        {
            
        }

        public Tuble(double[] init) : base(init)
        {
        }

        public static Tuble operator /(Tuble dividend, Tuble divisor)
        {
            int i;
            Tuble fraction = new Tuble(dividend.ToArray());

            for (i = 0; i < dividend.Count - 1; i++)
            {
                fraction[i] = (double) dividend[i] / divisor[i];
            }
            return fraction;
        }
    }
}
