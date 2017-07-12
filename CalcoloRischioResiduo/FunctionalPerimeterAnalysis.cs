using System.ComponentModel;

namespace CalcoloRischioResiduo
{
    public class FunctionalPerimeterAnalysis
    {

        public const double THRESHOLD = 0.75; // by req a perimeter is "covered by analysis" if 75% elements owns a VCI
        public const double VCIMAX = 1250;

        private FunctionalPerimeters _perimeter;
        private double _avgVCIC3;
        private double _avgVCIAll;
        private double _withVCI;

        public bool isAnalyzed()
        {
            return _withVCI >= THRESHOLD;
        }

        public bool isPerimeter(FunctionalPerimeters perimeter)
        {
            return _perimeter == perimeter;
        }

        public FunctionalPerimeterAnalysis(FunctionalPerimeters perimeter, double avgVCIC3, double avgVCIAll, double withVCI)
        {
            _perimeter = perimeter;
            _avgVCIC3 = avgVCIC3;
            _avgVCIAll = avgVCIAll;
            _withVCI = withVCI;
        }

        public double EstimateResidualRisk(bool isClassifiedElement)
        {
            double correctionFactor = 1 - _withVCI;

            if (isClassifiedElement)
            {
                return isAnalyzed() ? _avgVCIC3*(1 + correctionFactor) : VCIMAX;
            }
            else
            {
                return isAnalyzed() ? _avgVCIAll * (1 + correctionFactor) : VCIMAX;
            }
        }

    }
}