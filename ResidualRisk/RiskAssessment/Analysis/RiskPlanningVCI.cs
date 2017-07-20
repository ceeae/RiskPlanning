
namespace ResidualRisk.RiskAssessment.Analysis
{

    public class RiskPlanningVCI
    {

        public static double VCI_MAX = 1250;

        private readonly double _vciValue;

        private readonly double _biaValue;

        private readonly double _complianceValue;

        public RiskPlanningVCI(double biaValue, double complianceValue)
        {
            _biaValue = biaValue;
            _complianceValue = complianceValue;
            _vciValue = biaValue + complianceValue;
        }

        public double GetPotentialRisk()
        {
            return _vciValue;
        }

        public double GetPotentialRiskBIA()
        {
            return _biaValue;
        }

        public double GetPotentialRiskCOMPLIANCE()
        {
            return _complianceValue;
        }
    }
}
