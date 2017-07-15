
using System.Collections.Generic;
using System.ComponentModel;
using CalcoloRischioResiduo.RiskAssessment.Exceptions;
using CalcoloRischioResiduo.RiskAssessment.Requirements;

namespace CalcoloRischioResiduo.RiskAssessment.Analysis
{
    public class RPpds
    {
        private double _riskresidualvalue;
 
        public RPpds(double riskresidualvalue)
        {
            _riskresidualvalue = riskresidualvalue;
        }

        public double GetResidualRisk()
        {
           return _riskresidualvalue;
        }
    }
}
