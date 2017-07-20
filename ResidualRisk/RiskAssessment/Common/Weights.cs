using System.Collections.Generic;
using System.Linq;
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
                throw new ListsWithDifferentSizesCannotBeManagedException();
            }
            int i;
            List<int> result = Enumerable.Repeat(0, a.Count).ToList();

            for (i = 0; i < a.Count; i++)
            {
                result[i] = a[i] + b[i].Value;
            }
            return result;
        }

        public static List<double> operator /(Weights a, List<int> b)
        {
            if (a.Count != b.Count)
            {
                throw new ListsWithDifferentSizesCannotBeManagedException();
            }
            int i;
            List<double> result = Enumerable.Repeat((double) 0, a.Count).ToList();

            for (i = 0; i < a.Count; i++)
            {
                result[i] = (double) a[i].Value / b[i];
            }
            return result;
        }

    }

}
