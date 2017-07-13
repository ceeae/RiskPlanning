using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcoloRischioResiduo.RiskAssessment.Requirements
{

    public class COMPLIANCEValues
    {
        public int[] values { get; }

        public COMPLIANCEValues(int[] complvalue)
        {
            values = (int []) complvalue.Clone();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            COMPLIANCEValues tot = (COMPLIANCEValues) obj;
            return values.SequenceEqual(tot.values);
        }


    }
}
