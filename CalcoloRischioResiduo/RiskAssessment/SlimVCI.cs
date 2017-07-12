
namespace CalcoloRischioResiduo.RiskAssessment
{
    public class SlimVCI
    {
        public static double VCIMAX = 1250;

        private double _vcivalue;
        private double _biavalue;
        private double _compliancevalue;

        public SlimVCI(double vcivalue, double biavalue)
        {
            _vcivalue = vcivalue;
            _biavalue = biavalue;
            _compliancevalue = vcivalue - biavalue;
        }

        public double GetVCIValue()
        {
            return _vcivalue;
        }
        public double GetBIAValue()
        {
            return _biavalue;
        }
        public double GetCOMPLIANCEValue()
        {
            return _compliancevalue;
        }
    }
}
