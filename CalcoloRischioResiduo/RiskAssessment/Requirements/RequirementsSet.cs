using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcoloRischioResiduo.RiskAssessment.Common;

namespace CalcoloRischioResiduo.RiskAssessment.Requirements
{
    public class RequirementsSet : List<Requirement>
    {

        public List<int> Totals()
        {
            List<int> result = Enumerable.Repeat(0, Requirement.WEIGHTS_NUM).ToList();

            foreach (var req in this)
            {
                result += req.weights;
            }

            return result;
        }

        public void Append(int id, double pas, double alpha, int[] values)
        {
            Requirement requirement = new Requirement(id, new FractionWeight(pas), new CorrectionFactor(alpha));
            requirement.InitializeWeightsWithIntArray(values);
            Add(requirement);
        }
    }
}
