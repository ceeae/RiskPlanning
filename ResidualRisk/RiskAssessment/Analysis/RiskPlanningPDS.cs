
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

            requirements.CalculateRisk();

            _requirements = requirements;

        }

        public void SetVEF(double vef)
        {
            _requirements.VEF = vef;

            _requirements.CalculateRisk(); // Changing VEF requires new calculation of risk
        }

        public Dictionary<long, double[]> GetPotentialRiskDistributionFactors()
        {
            return _requirements.Distribution;
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
            return _requirements.ManagedRiskBIA;
        }

        public double GetManagedRiskCOMPLFactor()
        {
            return _requirements.ManagedRiskCOMPL;
        }

        public double GetResidualRiskBIAFactor()
        {
            return _requirements.ResidualRiskBIA;
        }

        public double GetResidualRiskCOMPLFactor()
        {
            return _requirements.ResidualRiskCOMPL;
        }

    }
}
