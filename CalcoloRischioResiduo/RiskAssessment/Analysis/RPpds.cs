
using System.Collections.Generic;
using System.ComponentModel;
using CalcoloRischioResiduo.RiskAssessment.Exceptions;
using CalcoloRischioResiduo.RiskAssessment.Requirements;

namespace CalcoloRischioResiduo.RiskAssessment.Analysis
{
    public class RPpds
    {
        private double _riskresidualvalue;

        private RequirementsSet _set;

        public RPpds(RequirementsSet set)
        {
            if (set == null)
            {
                throw new InvalidNullArgumentException();
            }

            _set = set;
        }

        public RPpds(double riskresidualvalue)
        {
            _riskresidualvalue = riskresidualvalue;
        }

        public Dictionary<long, double[]> GetPotentialRiskDistributionFactors()
        {
            return _set.GetPotentialRiskDistributionFactors();
        }

        public double GetPRBiaTotal()
        {
            return _set.PRbiaTot;
        }

        public double GetPRCOMPLTotal()
        {
            return _set.PRcomplTot;
        }

        public double GetManagedRiskBIAFactor()
        {
            return _set.GetManagedRiskBIAFactor();
        }

        public double GetManagedRiskCOMPLFactor()
        {
            return _set.GetManagedRiskCOMPLFactor();
        }

        public double GetResidualRiskBIAFactor()
        {
            return _set.GetResidualRiskBIAFactor();
        }

        public double GetResidualRiskCOMPLFactor()
        {
            return _set.GetResidualRiskCOMPLFactor();
        }

    }
}
