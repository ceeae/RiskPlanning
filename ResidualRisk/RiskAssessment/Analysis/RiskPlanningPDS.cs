
using System.Collections.Generic;
using ResidualRisk.RiskAssessment.Exceptions;
using ResidualRisk.RiskAssessment.Requirements;

namespace ResidualRisk.RiskAssessment.Analysis
{
    public class RiskPlanningPDS
    {

        private readonly RequirementsSet _requirements;

        public RiskPlanningPDS(RequirementsSet requirements)
        {
            if (requirements == null)
            {
                throw new InvalidNullArgumentException();
            }

            _requirements = requirements;
        }

        public void SetVEF(double vef)
        {
            _requirements.VEF = vef;
        }

        public Dictionary<long, double[]> GetPotentialRiskDistributionFactors()
        {
            return _requirements.GetPotentialRiskDistribution();
        }

        public double GetPotentialRiskBIATotal()
        {
            return _requirements.TotalPotentialRiskBIA;
        }

        public double GetPotentialRiskCOMPLTotal()
        {
            return _requirements.TotalPotentialRiskCOMPL;
        }

        public double GetManagedRiskBIAFactor()
        {
            return _requirements.GetManagedRiskBIA();
        }

        public double GetManagedRiskCOMPLFactor()
        {
            return _requirements.GetManagedRiskCOMPL();
        }

        public double GetResidualRiskBIAFactor()
        {
            return _requirements.GetResidualRiskBIA();
        }

        public double GetResidualRiskCOMPLFactor()
        {
            return _requirements.GetResidualRiskCOMPL();
        }

    }
}
