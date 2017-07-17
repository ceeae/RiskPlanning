using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResidualRisk.RiskAssessment.Exceptions;

namespace ResidualRisk.RiskAssessment.Common
{
    public class Weights : List<Weight>
    {

        public Weights(List<Weight> weights) : base(weights)
        {
            
        }

        public static List<int> operator +(List<int> a, Weights b)
        {
            if (a.Count != b.Count)
            {
                throw new ListsWithDifferentSizesCannotBeAddedException();
            }
            int i;
            List<int> result = Enumerable.Repeat(0, a.Count).ToList();

            for (i = 0; i < a.Count; i++)
            {
                result[i] = a[i] + b[i].Value;
            }
            return result;
        }
    }

}
