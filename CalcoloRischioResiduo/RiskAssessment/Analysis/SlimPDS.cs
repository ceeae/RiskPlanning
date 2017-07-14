
using System.Collections.Generic;
using System.ComponentModel;
using CalcoloRischioResiduo.RiskAssessment.Exceptions;
using CalcoloRischioResiduo.RiskAssessment.Requirements;

namespace CalcoloRischioResiduo.RiskAssessment.Analysis
{
    public class SlimPDS
    {
        private ExtentedRequirements _requirements;

        private double _riskresidualvalue;
 
        public SlimPDS(double riskresidualvalue)
        {
            _riskresidualvalue = riskresidualvalue;
        }

        public SlimPDS(ExtentedRequirements requirements)
        {
            _requirements = requirements;
        }

        public double GetResidualRiskValue()
        {
            if (_requirements == null)
            {
                throw new MissingRequirementsException();
            }

            return _riskresidualvalue;
        }
    }
}
