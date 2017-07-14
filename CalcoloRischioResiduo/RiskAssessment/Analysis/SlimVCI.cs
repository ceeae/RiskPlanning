
using System.Security.Claims;

namespace CalcoloRischioResiduo.RiskAssessment.Analysis
{
    public class SlimVCI
    {
        public static double VCIMAX = 1250;

        private double _vcivalue;
        private double _biavalue;
        private double _compliancevalue;

        public SlimVCI(double biavalue, double compliancevalue)
        {
            _biavalue = biavalue;
            _compliancevalue = compliancevalue;
            _vcivalue = biavalue + compliancevalue;
        }

        public double GetPotentialRiskValue()
        {
            return _vcivalue;
        }

        public double GetPotentialRiskBIAValue()
        {
            return _biavalue;
        }

        public double GetPotentialRiskCOMPLIANCEValue()
        {
            return _compliancevalue;
        }
    }
}
