
namespace CalcoloRischioResiduo.RiskAssessment.Analysis
{
    public class SlimPDS
    {
        private double _riskresidualvalue;
 
        public SlimPDS(double riskresidualvalue)
        {
            _riskresidualvalue = riskresidualvalue;
        }

        public double GetResidualRiskValue()
        {
            return _riskresidualvalue;
        }
    }
}
