using System.Collections.Generic;
using System.Linq;
using CalcoloRischioResiduo.RiskAssessment.Common;

namespace CalcoloRischioResiduo.RiskAssessment.Requirements
{
    public class RequirementsSet : List<Requirement>
    {

        public List<int> CalculateTotals()
        {
            List<int> result = Enumerable.Repeat(0, Requirement.WEIGHTS_NUM).ToList();

            foreach (var req in this)
            {
                result += req.ReqWeights;
            }

            return result;
        }

        public void AddRequirement(int id, double pas, double alpha, int[] values)
        {
            Requirement requirement = new Requirement(id, new FractionWeight(pas), new CorrectionFactor(alpha), values);
            Add(requirement);
        }

        public Dictionary<long, double> GetPotentialRiskDistributionBIA()
        {
            return new Dictionary<long, double>
            {
                { 101, 0.5 },
                { 102, 0.5 },
                { 103, 0.5 },
                { 104, 0.5 },
                { 105, 0.5 },

            };
        }

    }
}
